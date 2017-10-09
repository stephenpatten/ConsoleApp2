using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            //var builder = new ContainerBuilder();
            // Register types that expose interfaces...
            //builder.RegisterType<c => new CreateLogger()>().As<NLog.ILogger>();

            MyMethod1();

        }


        //https://github.com/NLog/NLog/wiki/Tutorial

        public static void MyMethod1()
        {
            int k = 42;
            int l = 100;

            logger.Trace("Sample trace message, k={0}, l={1}", k, l);
            logger.Debug("Sample debug message, k={0}, l={1}", k, l);
            logger.Info("Sample informational message, k={0}, l={1}", k, l);
            logger.Warn("Sample warning message, k={0}, l={1}", k, l);
            logger.Error("Sample error message, k={0}, l={1}", k, l);
            logger.Fatal("Sample fatal error message, k={0}, l={1}", k, l);
            logger.Log(LogLevel.Info, "Sample informational message, k={0}, l={1}", k, l);
        }
    }
}
