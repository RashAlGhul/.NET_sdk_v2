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
            Logger logger = new Logger();
            Log.RegisterLogger(logger, LogLevel.Debug);
            Log.Debug(() =>
            {
                return "debug";
            });
        }

        [Test]
        public void DebugShouldExitFromVoid()
        {
            Log.RegisterLogger(null);
            Log.Debug("Message");
        }

        [Test]
        public void DebugShouldExecuteMessage()
        {
            Logger logger = new Logger();
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
            Logger logger = new Logger();
            Log.RegisterLogger(logger, LogLevel.Info);
            Log.Info(() =>
            {
                return "Info";
            });
        }

        [Test]
        public void InfoShouldExitFromVoid()
        {
            Log.RegisterLogger(null);
            Log.Info("Message");
        }

        [Test]
        public void InfoShouldExecuteMessage()
        {
            Logger logger = new Logger();
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
            Logger logger = new Logger();
            Log.RegisterLogger(logger);
            Log.Warning(() =>
            {
                return "warning";
            });
        }

        [Test]
        public void WarningShouldExitFromVoid()
        {
            Log.RegisterLogger(null);
            Log.Warning("Message");
        }

        [Test]
        public void WarningShouldExecuteMessage()
        {
            Logger logger = new Logger();
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
            Logger logger = new Logger();
            Log.RegisterLogger(logger, LogLevel.Error);
            Log.Error(() =>
            {
                return "error";
            });
        }

        [Test]
        public void ErrorShouldExitFromVoid()
        {
            Log.RegisterLogger(null);
            Log.Error("Message");
        }

        [Test]
        public void ErrorShouldExecuteMessage()
        {
            Logger logger = new Logger();
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
            Logger logger = new Logger();
            Log.RegisterLogger(logger, LogLevel.Fatal);
            Log.Fatal(() =>
            {
                return "fatal";
            }, null);
        }

        [Test]
        public void FatalShouldExitFromVoid()
        {
            Log.RegisterLogger(null);
            Log.Fatal("Message", new Exception("exception"));
        }

        [Test]
        public void FatalShouldExecuteMessage()
        {
            Logger logger = new Logger();
            Log.RegisterLogger(logger, LogLevel.Fatal);
            Log.Error("Message");
        }

        [TearDown]
        public void TearDown()
        {
            Log.RegisterLogger(null);
        }

        private class Logger : ILogger
        {
            public void Info(string message)
            {
                Console.WriteLine($"Info: {message}");
            }

            public void Debug(string message)
            {
                Console.WriteLine($"Debug: {message}");
            }

            public void Warning(string message)
            {
                Console.WriteLine($"Warning: {message}");
            }

            public void Error(string message, Exception ex)
            {
                Console.WriteLine($"Error: {message}");
            }

            public void Fatal(string message, Exception ex)
            {
                Console.WriteLine($"Fatal: {message}");
            }
        }
    }
}