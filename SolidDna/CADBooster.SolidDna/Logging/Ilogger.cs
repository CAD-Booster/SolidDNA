using System;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a type used to perform logging. Copied from Microsoft.Extensions.Logging.Remarks: Aggregates most logging patterns to a single method.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    /// <typeparam name="TState">The identifier for the scope.</typeparam>
    /// <param name="state"></param>
    /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
    IDisposable BeginScope<TState>(TState state);

    /// <summary>
    /// Checks if the given <paramref name="logLevel"/> is enabled.
    /// </summary>
    /// <param name="logLevel">Level to be checked.</param>
    /// <returns>true if enabled.</returns>
    bool IsEnabled(LogLevel logLevel);

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeparam name="TState">The type of the state object.</typeparam>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">ID of the event.</param>
    /// <param name="state">The entry to be written. Can be also an object.</param>
    /// <param name="exception">The exception related to this entry.</param>
    /// <param name="formatter">Function to create a string message of the state and exception.</param>
    void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
}