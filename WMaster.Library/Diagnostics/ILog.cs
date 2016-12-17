namespace WMaster.Diagnostics
{
    using System;
    using System.IO;
    using System.Text;

    public delegate void LoggedDataEventHandler(string message, WMLog.TraceLog typeTrace);

    /// <summary>
    /// Log manager interface for os dependant log.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Raised when log message was sent.
        /// </summary>
        event LoggedDataEventHandler LoggedData;

        /// <summary>
        /// Minimal severity level to log.
        /// <remarks><para>TraceLog.DEBUG and TraceLog.BENCHMARK wasn't affected with this switch.</para></remarks>
        /// </summary>
        WMLog.LogSwitch Switch { get; set; }

        /// <summary>
        /// Trace a message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        /// <param name="typeTrace"><see cref="WMLog.TraceLog"/> severity level trace. <see cref="WMLog.TraceLog.ERROR"/> par default.</param>
        void Trace(string message, WMLog.TraceLog typeTrace);

        /// <summary>
        /// Trace an information message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        void TraceInformation(string message);

        /// <summary>
        /// Trace a warning message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        void TraceWarning(string message);

        /// <summary>
        /// Trace an error message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        void TraceError(string message);

        /// <summary>
        /// Trace an faillure message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        void TraceFail(string message);

        /// <summary>
        /// Trace a debug message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        void TraceDebug(string message);

        /// <summary>
        /// Trace an benckmark message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        void TraceBenchmark(string message);
    }
}
