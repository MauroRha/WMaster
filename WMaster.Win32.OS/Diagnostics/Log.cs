namespace WMaster.Win32.Diagnostics
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Classe de gestion des logs
    /// </summary>
    public sealed class Log : WMaster.Diagnostics.ILog
    {
        #region Static part
        /// <summary>
        /// Singleton of <see cref="Log"/> instance.
        /// </summary>
        private static Log _instance = null;

        /// <summary>
        /// Get application log instance.
        /// </summary>
        public static Log LogInstance
        {
            get
            {
                if (Log._instance == null)
                { Log._instance = new Log(); }
                return Log._instance;
            }
        }

        // TODO : REFACTORING - need to move to configuration setting
        //public const string LogFilename = "gamelog.txt";
        public static string _logFilename = null;

        public static string LogFilename
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Log._logFilename))
                {
                    if (System.Reflection.Assembly.GetEntryAssembly() != null)
                    { Log._logFilename = System.Reflection.Assembly.GetEntryAssembly().GetName().Name + ".log"; }
                    else
                    { Log._logFilename = "Game.Log"; }
                }
                return Log._logFilename;
            }
        }

        /// <summary>
        /// Store the severity level of log to trace
        /// </summary>
        private static WMLog.LogSwitch _logSwitch = WMLog.LogSwitch.NONE;

        /// <summary>
        /// Indicate if initialisation has been done.
        /// </summary>
        private static bool _setup = false;
        #endregion

        #region Properties
        /// <summary>
        /// Minimal severity level to log.
        /// <remarks><para>TraceLog.DEBUG and TraceLog.BENCHMARK wasn't affected with this switch.</para></remarks>
        /// </summary>
        public WMLog.LogSwitch Switch
        {
            get { return Log._logSwitch; }
            set { Log._logSwitch = value; }
        }
        #endregion

        #region CTor / Initialisation
        /// <summary>
        /// Constructor, private to respect Singleton template.
        /// </summary>
        private Log()
        {
            if (!Log._setup)
            {
                this.InitLogFile();
            }
        }

        /// <summary>
        /// Initialisation of trace listener
        /// </summary>
        /// <param name="logSwitch"></param>
        private void InitLogFile()
        {
            Console.Error.WriteLine(typeof(WMaster.Win32.Diagnostics.Log).FullName + " Initialisation");
            try
            {
                    // Remove the original default trace listener.
                    System.Diagnostics.Trace.Listeners.RemoveAt(0);

                    // Create and add a new default trace listener.
                    System.Diagnostics.DefaultTraceListener defaultListener = new System.Diagnostics.DefaultTraceListener();
                    defaultListener.LogFileName = Log._logFilename;

                    System.Diagnostics.Trace.Listeners.Add(defaultListener);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return;
            }
            finally
            { Log._setup = true; }
        }
        #endregion

        #region Events
        /// <summary>
        /// Log message event handler.
        /// </summary>
        public event WMaster.Diagnostics.LoggedDataEventHandler LoggedData;

        /// <summary>
        /// Send log event.
        /// </summary>
        /// <param name="message">Message logged.</param>
        /// <param name="typeTrace"><see cref="WMLog.TraceLog"/> sevirity level.</param>
        private void OnLoggedData(string message, WMLog.TraceLog typeTrace)
        {
            if (LoggedData != null)
            { LoggedData(message, typeTrace); }
        }
        #endregion

        /// <summary>
        /// Trace a message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        /// <param name="typeTrace"><see cref="WMLog.TraceLog"/> severity level trace. Define the log category.</param>
        public void Trace(string message, WMLog.TraceLog typeTrace)
        {
            System.Diagnostics.Trace.WriteLine(message, typeTrace.ToString());
        }

        /// <summary>
        /// Trace an information message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        public void TraceInformation(string message)
        {
            System.Diagnostics.Trace.TraceInformation(message);
        }

        /// <summary>
        /// Trace a warning message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        public void TraceWarning(string message)
        {
            System.Diagnostics.Trace.TraceWarning(message);
        }

        /// <summary>
        /// Trace an error message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        public void TraceError(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }

        /// <summary>
        /// Trace an faillure message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        public void TraceFail(string message)
        {
            System.Diagnostics.Trace.Fail(message);
        }

        /// <summary>
        /// Trace a debug message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        public void TraceDebug(string message)
        {
            this.Trace(message, WMLog.TraceLog.DEBUG);
        }

        /// <summary>
        /// Trace an benckmark message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        public void TraceBenchmark(string message)
        {
            this.Trace(message, WMLog.TraceLog.BENCHMARK);
        }
    }
}
