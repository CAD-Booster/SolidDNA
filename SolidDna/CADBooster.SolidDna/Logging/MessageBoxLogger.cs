using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADBooster.SolidDna
{
    internal class MessageBoxLogger : ILogger
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Critical;
        private readonly Stack<MessageLogScope> _scopes = new Stack<MessageLogScope>();

        public IDisposable BeginScope<TState>(TState state)
            => new MessageLogScope(_scopes, LogLevel);

        public bool IsEnabled(LogLevel logLevel) => LogLevel <= logLevel;

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

        private class MessageLogScope : IDisposable
        {
            private readonly StringBuilder _string = new StringBuilder();
            private readonly Stack<MessageLogScope> _scopes;
            private readonly LogLevel _logLevel;
            private readonly HashSet<LogLevel> _levels = new HashSet<LogLevel>();

            public MessageLogScope(Stack<MessageLogScope> scopes, LogLevel logLevel)
            {
                _scopes = scopes;
                _logLevel = logLevel;
                _scopes.Push(this);
            }

            public void Log(LogLevel logLevel, string message)
            {
                if (logLevel < _logLevel)
                    return;

                _ = _levels.Add(logLevel);
                if (string.IsNullOrWhiteSpace(message) == false)
                    _ = _string.AppendLine(message + Environment.NewLine);
            }

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
    }
}
