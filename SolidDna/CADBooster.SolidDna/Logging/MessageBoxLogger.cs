using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A logger implementation that displays messages in SOLIDWORKS message boxes
    /// </summary>
    /// <remarks>
    /// Messages are shown either immediately or grouped by scope when using <see cref="BeginScope{TState}"/>.
    /// </remarks>
    public class MessageBoxLogger : ILogger
    {
        /// <summary>
        /// Represents a scope for grouping log messages
        /// </summary>
        /// <remarks>
        /// Messages are buffered until the scope is disposed, then shown in a single message box
        /// </remarks>
        private class MessageLogScope : IDisposable
        {
            private readonly StringBuilder _string = new StringBuilder();
            private readonly Stack<MessageLogScope> _scopes;
            private readonly LogLevel _logLevel;
            private readonly HashSet<LogLevel> _levels = new HashSet<LogLevel>();

            /// <summary>
            /// Creates a new log scope and pushes it onto the scope stack
            /// </summary>
            /// <param name="scopes">The parent scope stack</param>
            /// <param name="logLevel">The minimum log level for this scope</param>
            public MessageLogScope(Stack<MessageLogScope> scopes, LogLevel logLevel)
            {
                _scopes = scopes;
                _logLevel = logLevel;
                _scopes.Push(this);
            }

            /// <summary>
            /// Logs a message to this scope
            /// </summary>
            /// <param name="logLevel">The severity level of the message</param>
            /// <param name="message">The message text</param>
            public void Log(LogLevel logLevel, string message)
            {
                if (logLevel < _logLevel)
                    return;

                _ = _levels.Add(logLevel);
                if (string.IsNullOrWhiteSpace(message) == false)
                    _ = _string.AppendLine(message);
            }

            /// <summary>
            /// Disposes the scope and shows all buffered messages
            /// </summary>
            /// <exception cref="SolidDnaException">
            /// Thrown if the dispose sequence is incorrect
            /// </exception>
            public void Dispose()
            {
                var scope = _scopes.Peek();
                if (scope != this)
                    throw new SolidDnaException(new SolidDnaError(), new InvalidOperationException("Incorrect dispose sequence"));

                _ = _scopes.Pop();

                var message = scope._string.ToString();
                var maxLevel = _levels.Count == 0 ? LogLevel.Trace : _levels.Max();

                if (maxLevel < _logLevel)
                    return;

                _ = SolidWorksEnvironment.Application.ShowMessageBox(
                    message,
                    ConvertLogLevelToIcon(maxLevel),
                    SolidWorksMessageBoxButtons.Ok
                );
            }
        }

        /// <summary>
        /// Gets or sets the minimum log level that will be displayed
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="LogLevel.Critical"/> to only show critical errors by default
        /// </remarks>
        public LogLevel LogLevel { get; set; } = LogLevel.Critical;

        private readonly Stack<MessageLogScope> _scopes = new Stack<MessageLogScope>();

        /// <summary>
        /// Begins a logical operation scope
        /// </summary>
        /// <typeparam name="TState">Not used</typeparam>
        /// <param name="state">The identifier for the scope</param>
        /// <returns>An <see cref="IDisposable"/> that ends the scope when disposed</returns>
        /// <remarks>
        /// Messages logged within the scope will be grouped together and shown when the scope is disposed
        /// </remarks>
        public IDisposable BeginScope<TState>(TState state)
            => new MessageLogScope(_scopes, LogLevel);

        /// <summary>
        /// Checks if the given log level is enabled
        /// </summary>
        /// <param name="logLevel">Level to be checked</param>
        /// <returns>True if enabled</returns>
        public bool IsEnabled(LogLevel logLevel) => LogLevel <= logLevel;

        /// <summary>
        /// Logs a message to a SOLIDWORKS message box
        /// </summary>
        /// <typeparam name="TState">The type of the state object</typeparam>
        /// <param name="logLevel">The severity level of the message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="state">The message state</param>
        /// <param name="exception">The exception to log</param>
        /// <param name="formatter">Function to format the message</param>
        /// <remarks>
        /// Shows messages immediately when not in a scope, or buffers them when in a scope
        /// </remarks>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel < LogLevel)
                return;

            if (_scopes.Count == 0)
                _ = SolidWorksEnvironment.Application.ShowMessageBox(
                    formatter.Invoke(state, exception),
                    ConvertLogLevelToIcon(logLevel),
                    SolidWorksMessageBoxButtons.Ok
                );
            else
                _scopes.Peek().Log(logLevel, formatter.Invoke(state, exception));
        }

        /// <summary>
        /// Converts a <see cref="LogLevel"/> to a SOLIDWORKS message box icon
        /// </summary>
        /// <param name="logLevel">The log level to convert</param>
        /// <returns>The appropriate message box icon</returns>
        /// <exception cref="ArgumentException">Thrown for unsupported log levels</exception>
        private static SolidWorksMessageBoxIcon ConvertLogLevelToIcon(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return SolidWorksMessageBoxIcon.Information;
                case LogLevel.Debug:
                    return SolidWorksMessageBoxIcon.Information;
                case LogLevel.Information:
                    return SolidWorksMessageBoxIcon.Information;
                case LogLevel.Warning:
                    return SolidWorksMessageBoxIcon.Warning;
                case LogLevel.Error:
                    return SolidWorksMessageBoxIcon.Stop;
                case LogLevel.Critical:
                    return SolidWorksMessageBoxIcon.Stop;
                default:
                    throw new ArgumentException("Unsupported enum value", nameof(logLevel));
            }
        }
    }
}
