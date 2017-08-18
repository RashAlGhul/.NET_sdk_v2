using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMA.MobileConnect.Test
{
    [TestFixture, Parallelizable]
    public class LogTests
    {
        [SetUp]
        public void SetUp()
        {
            Log.RegisterLogger(null);
        }

        [Test]
        public void DebugShouldNotExecuteMessageFuncIfLoggerNull()
        {
            Log.Debug(() =>
            {
                Assert.Fail();
                return "";
            });
        }

        [Test]
        public void DebugShouldExecuteMessageFunc()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Debug);
            Log.Debug(() =>
            {
                return "debug";
            });
        }

        [Test]
        public void DebugShouldExitFromVoid()
        {
            Log.Debug("Message");
        }

        [Test]
        public void DebugShouldExecuteMessage()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Debug);
            Log.Debug("Message");
        }

        [Test]
        public void InfoShouldNotExecuteMessageFuncIfLoggerNull()
        {
            Log.Info(() =>
            {
                Assert.Fail();
                return "";
            });
        }

        [Test]
        public void InfoShouldExecuteMessageFunc()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Info);
            Log.Info(() =>
            {
                return "Info";
            });
        }

        [Test]
        public void InfoShouldExitFromVoid()
        {
            Log.Info("Message");
        }

        [Test]
        public void InfoShouldExecuteMessage()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Info);
            Log.Info("Message");
        }

        [Test]
        public void WarningShouldNotExecuteMessageFuncIfLoggerNull()
        {
            Log.Warning(() =>
            {
                Assert.Fail();
                return "";
            });
        }

        [Test]
        public void WarningShouldNotExecuteMessageFunc()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger);
            Log.Warning(() =>
            {
                return "warning";
            });
        }

        [Test]
        public void WarningShouldExitFromVoid()
        {
            Log.Warning("Message");
        }

        [Test]
        public void WarningShouldExecuteMessage()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger);
            Log.Warning("Message");
        }

        [Test]
        public void ErrorShouldNotExecuteMessageFuncIfLoggerNull()
        {
            Log.Error(() =>
            {
                Assert.Fail();
                return "";
            });
        }

        [Test]
        public void ErrorShouldExecuteMessageFunc()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Error);
            Log.Error(() =>
            {
                return "error";
            });
        }

        [Test]
        public void ErrorShouldExitFromVoid()
        {
            Log.Error("Message");
        }

        [Test]
        public void ErrorShouldExecuteMessage()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Error);
            Log.Error("Message");
        }

        [Test]
        public void FatalShouldNotExecuteMessageFuncIfLoggerNull()
        {
            Log.Fatal(() =>
            {
                Assert.Fail();
                return "";
            }, null);
        }

        [Test]
        public void FatalShouldExecuteMessageFuncIfLoggerNull()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Fatal);
            Log.Fatal(() =>
            {
                return "fatal";
            }, null);
        }

        [Test]
        public void FatalShouldExitFromVoid()
        {
            Log.Fatal("Message", new Exception("exception"));
        }

        [Test]
        public void FatalShould()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Fatal);
            Log.Fatal("Message", new Exception("exception"));
        }

        [Test]
        public void FatalShouldExecuteMessage()
        {
            TestLogger logger = new TestLogger();
            Log.RegisterLogger(logger, LogLevel.Fatal);
            Log.Error("Message");
        }
    }
}