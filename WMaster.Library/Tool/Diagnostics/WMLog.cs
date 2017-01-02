/*
 * Original source code in C++ from :
 * Copyright 2009, 2010, The Pink Petal Development Team.
 * The Pink Petal Devloment Team are defined as the game's coders 
 * who meet on http://pinkpetal.org     // old site: http://pinkpetal .co.cc
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#define BENCHMARK

// Root namespace to provid global access
namespace WMaster
{
    using System;
    using System.Text;
    using WMaster.Tool.Diagnostics;

    /// <summary>
    /// static class exposing log functionality from concret os dependant log instance.
    /// </summary>
    public static class WMLog
    {
        /// <summary>
        /// Store old exception to ensure it don't be treate multiple time
        /// </summary>
        private static Exception _oldException;

        /// <summary>
        /// Level of log severity to log. 
        /// </summary>
        public enum LogSwitch : short
        {
            /// <summary>
            /// Log all severity levels.
            /// </summary>
            INFORMATION = TraceLog.INFORMATION,
            /// <summary>
            /// Log all severity levels except TraceLog.INFORMATION
            /// </summary>
            WARNING = TraceLog.WARNING,
            /// <summary>
            /// Log all severity levels greater or equal to TraceLog.ERROR
            /// </summary>
            ERROR = TraceLog.ERROR,
            /// <summary>
            /// Log only TraceLog.CRITICAL severity level.
            /// </summary>
            CRITICAL = TraceLog.CRITICAL,
            /// <summary>
            /// No log are saved
            /// </summary>
            NONE = short.MaxValue
        }

        /// <summary>
        /// Severity level of log trace.
        /// <remarks>
        ///     <para>Added DEBUG and BENCHMARK for developpement help. They are stored only if #define DEBUG / #define BENCHMARK</para>
        ///     <para>Set Flags attribut for futur evolutions.</para>
        /// </remarks>
        /// </summary>
        [Flags]
        public enum TraceLog : short
        {
            /// <summary>
            /// Information to log. Not shown if LogSwitch is set to Warning or higher
            /// </summary>
            INFORMATION = 1,
            /// <summary>
            /// Log a Warning message. Need attention but dont stop execution.
            /// </summary>
            WARNING = 2,
            /// <summary>
            /// Log an Error. Need attention and throw excetpion. Stop application if error wasn't catch.
            /// </summary>
            ERROR = 4,
            /// <summary>
            /// Log a Critical error. Need attention and throw excetpion. Stop application immediately {Environment.Exit(-1)}
            /// </summary>
            CRITICAL = 16,
            /// <summary>
            /// Debug information to log. Need #define DEBUG to log it.
            /// </summary>
            DEBUG = 32,
            /// <summary>
            /// Benchmark log (ie performance test, execution time). Need #define BENCHMARK to log it.
            /// </summary>
            BENCHMARK = 64
        }

        /// <summary>
        /// String buffer for logging information.
        /// <remarks><para>Need to call Send() to write & clear this buffer.</para></remarks>
        /// </summary>
        private static StringBuilder _logString = new StringBuilder();

        // rename ss() to LogMessage()
        /// <summary>
        /// Get the log buffer.
        /// <remarks><para>Need to call Sent() to write log into Output</para></remarks>
        /// </summary>
        public static StringBuilder LogMessage
        {
            get
            {
                return WMLog._logString;
            }
        }

        /// <summary>
        /// Internal concret os dependant log instance
        /// </summary>
        private static ILog _log;

        /// <summary>
        /// Initialise the log manager with <see cref="ILog"/> derived instnce.
        /// </summary>
        /// <param name="log"></param>
        internal static void InitialiseLog(ILog log)
        {
            WMLog._log = log;
        }

        /// <summary>
        /// Trace an exception. 
        /// </summary>
        /// <param name="ex">Exception to trace. Only if différent from old excetion traced.</param>
        /// <param name="typeTrace"><see cref="WMLog.TraceLog"/> severity level trace. <see cref="WMLog.TraceLog.ERROR"/> par default.</param>
        public static void Trace(Exception ex, WMLog.TraceLog typeTrace = WMLog.TraceLog.ERROR)
        {
            if (WMLog._oldException != ex)
            {
                System.Reflection.MethodBase callingMethod = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();

                WMLog.Trace(
                    string.Format("{0}:{1}::{2} ==> {3}",
                        callingMethod.ReflectedType.Name,
                        callingMethod.Name.Substring(4),
                        ex.Message,
                        ex.StackTrace),
                    typeTrace);

                WMLog._oldException = ex;
            }
        }

        /// <summary>
        /// Trace le contenu du buffer (StringBuilder)
        /// </summary>
        /// <param name="typeTrace"><see cref="WMLog.TraceLog"/> severity level trace. <see cref="WMLog.TraceLog.ERROR"/> par default.</param>
        public static void TraceBuffer(WMLog.TraceLog typeTrace = WMLog.TraceLog.INFORMATION)
        {
            if (WMLog._logString.Length > 0)
            {
                WMLog.Trace(WMLog._logString.ToString(), typeTrace);
                WMLog._logString.Clear();
            }
        }

        /// <summary>
        /// Trace a message.
        /// </summary>
        /// <param name="message">Message to trace.</param>
        /// <param name="typeTrace"><see cref="WMLog.TraceLog"/> severity level trace. <see cref="WMLog.TraceLog.ERROR"/> par default.</param>
        public static void Trace(string message, WMLog.TraceLog typeTrace = WMLog.TraceLog.INFORMATION)
        {
            // Game wasn't initialized -> Log may not be initialised
            if (!Game.IsInitialized && (WMLog._log == null))
            { return; }

            WMLog.LogSwitch lSwitch = WMLog._log.Switch;
            if (lSwitch == WMLog.LogSwitch.NONE)
            { return; }

            switch (typeTrace)
            {
                case WMLog.TraceLog.INFORMATION:
                    if (lSwitch <= WMLog.LogSwitch.INFORMATION)
                    { WMLog._log.TraceInformation(message); }
                    break;

                case WMLog.TraceLog.WARNING:
                    if (lSwitch <= WMLog.LogSwitch.WARNING)
                    { WMLog._log.TraceWarning(message); }
                    break;

                case WMLog.TraceLog.ERROR:
                    if (lSwitch <= WMLog.LogSwitch.ERROR)
                    { WMLog._log.TraceError(message); }
                    break;

                case WMLog.TraceLog.CRITICAL:
                    if (lSwitch <= WMLog.LogSwitch.CRITICAL)
                    {
                        WMLog._log.TraceFail(message);
                        Environment.Exit(-1);
                    }
                    break;

                case WMLog.TraceLog.DEBUG:
#if DEBUG
                    WMLog._log.TraceDebug(message);
#endif
                    break;

                case WMLog.TraceLog.BENCHMARK:
#if BENCHMARK
                    WMLog._log.TraceBenchmark(message);
#endif
                    break;

                default:
                    WMLog._log.Trace(message, typeTrace);
                    break;
            }
        }

        #region Obsolete members
        /// <summary>
        /// Obsolete. Do not use.
        /// </summary>
        /// <returns>May return somthing... or not.</returns>
        [Obsolete("Dont use, the log stream musn't be accessible", true)]
        public static System.IO.TextWriter os()
        { throw new NotImplementedException("Deprecated function! System.IO.TextWriter wasn't used"); }

        /// <summary>
        /// Send the log buffer to log out canal
        /// </summary>
        [Obsolete("Dont use, was replaced by {void TraceBuffer(WMLog.TraceLog typeTrace = WMLog.TraceLog.INFORMATION)}", true)]
        public static void Send()
        {
            WMLog.TraceBuffer(); ;
        }

        /// <summary>
        /// Add a string to  log file.
        /// </summary>
        /// <param name="text">String to add to log file.</param>
        [Obsolete("Dont use, was replaced by {void Trace(string message, WMLog.TraceLog typeTrace = WMLog.TraceLog.INFORMATION)}", false)]
        public static void Write(string text)
        {
            WMLog.Trace(text);
        }
        #endregion
    }
}
