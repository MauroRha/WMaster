namespace WMaster.Win32.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using WMaster.Win32.Diagnostics;

    /// <summary>
    /// Unit test of log class.
    /// </summary>
    [TestClass]
    public class LogTests
    {
        /// <summary>
        /// Initialisation of tests datas.
        /// </summary>
        [TestInitialize]
        public void InitializeTests()
        {
            // Ned to know if log file can be create.
            if (File.Exists(Log.LogFilename))
            { File.Delete(Log.LogFilename); }
        }

        /// <summary>
        /// Cleanup all data created by tests.
        /// </summary>
        [TestCleanup]
        public void CleanupTests()
        {
            if (File.Exists(Log.LogFilename))
            { File.Delete(Log.LogFilename); }
        }

        [TestMethod]
        public void Log_TraceString()
        {
            string logStringToWrite = "Log writing test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.Switch = WMLog.LogSwitch.INFORMATION;

            Log.LogInstance.Trace(logStringToWrite, WMLog.TraceLog.INFORMATION);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }

        [TestMethod]
        public void Log_TraceInformation()
        {
            string logStringToWrite = "Log writing information test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.TraceInformation(logStringToWrite);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }

        [TestMethod]
        public void Log_TraceWarning()
        {
            string logStringToWrite = "Log writing warning test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.TraceWarning(logStringToWrite);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }

        [TestMethod]
        public void Log_TraceError()
        {
            string logStringToWrite = "Log writing error test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.TraceError(logStringToWrite);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }

        [TestMethod]
        public void Log_TraceFail()
        {
            string logStringToWrite = "Log writing fail test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.TraceFail(logStringToWrite);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }

        [TestMethod]
        public void Log_TraceDebug()
        {
            string logStringToWrite = "Log writing debug test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.TraceDebug(logStringToWrite);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }

        [TestMethod]
        public void Log_TraceBenchmark()
        {
            string logStringToWrite = "Log writing benchmark test.";

            Assert.IsNotNull(Log.LogInstance, "LogInstance is null and cannot been initialized!");

            Log.LogInstance.TraceBenchmark(logStringToWrite);
            Assert.IsTrue(File.Exists(Log.LogFilename), Log.LogFilename + " wasen't created!");

            string logFileResult = File.ReadAllText(Log.LogFilename);
            Assert.IsTrue(logFileResult.Contains(logStringToWrite), "Log file dosen't contain file logged!");
        }
    }
}
