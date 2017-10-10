﻿using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
  
    class AdapterProgram
    {
        private readonly TimeSpan _duration;

        private readonly ConsoleApp2.NLogAdapter<AdapterProgram> _logger; 

        static void Main(string[] args)
        {
            var duration = TimeSpan.FromSeconds(10);

            if (args.Length > 0)
            {
                var seconds = int.Parse(args[0]);
                duration = TimeSpan.FromSeconds(seconds);
            }

            new ConsoleApp2.AdapterProgram(duration).Run();

            NLog.LogManager.Flush();

            Console.WriteLine("Finished....");
            Console.ReadKey();
        }

        public AdapterProgram(TimeSpan duration)
        {
            _duration = duration;
            _logger = new ConsoleApp2.NLogAdapter<AdapterProgram>();
        }

        private void Run()
        {
            _logger.Log(new LogEntry(LoggingEventType.Information, "Benchmarking starting"));
            TestThroughput();
            //TestMultiThreading();
            //TestIdle();
            _logger.Log(new LogEntry(LoggingEventType.Warning, "Benchmarking ended"));

            Console.WriteLine($"Gen 0: {GC.CollectionCount(0)}");
            Console.WriteLine($"Gen 1: {GC.CollectionCount(1)}");
            Console.WriteLine($"Gen 2: {GC.CollectionCount(2)}");
        }

        private void TestThroughput()
        {
            Console.WriteLine("------------Testing Throughput------------");

            var sw = Stopwatch.StartNew();
            long counter = 0;
            while (sw.Elapsed < _duration)
            {
                counter++;
                _logger.Log(new LogEntry(LoggingEventType.Debug, $"Counter is: {counter}"));
            }

            Console.WriteLine($"Counter reached: {counter:n0}, Time Taken: {sw.Elapsed}");
        }

        private void TestMultiThreading()
        {
            Console.WriteLine("------------Testing MultiThreading------------");

            const int WorkerCount = 6;

            long totalCounter = 0;
            Func<int> action = () =>
            {
                var localCounter = 0;
                var sw = Stopwatch.StartNew();

                while (sw.Elapsed < _duration)
                {
                    _logger.Log(new LogEntry(LoggingEventType.Debug, $"Counter is: {++localCounter}"));
                }

                return localCounter;
            };

            var totalSw = Stopwatch.StartNew();

            Parallel.For(
                0,
                WorkerCount,
                new ParallelOptions { MaxDegreeOfParallelism = WorkerCount },
                () => 0,
                (i, state, partial) => action(),
                partialCounter => Interlocked.Add(ref totalCounter, partialCounter));

            Console.WriteLine($"Counter reached: {0:n0}, Time Taken: {1}", totalCounter, totalSw.Elapsed.ToString());
        }

        private void TestIdle()
        {
            Console.WriteLine("------------Testing Idle------------");

            var sw = Stopwatch.StartNew();
            long counter = 0;
            while (sw.Elapsed < _duration)
            {
                counter++;
                _logger.Log(new LogEntry(LoggingEventType.Error, $"Counter is: {counter}"));
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Counter reached: {0:n0}, Time Taken: {1}", counter, sw.Elapsed.ToString());
        }

    }
}



////https://github.com/NLog/NLog/wiki/Tutorial
//public static void MyMethod1()
//{
//    int k = 42;
//    int l = 100;

//    _logger.Trace("Sample trace message, k={0}, l={1}", k, l);
//    _logger.Debug("Sample debug message, k={0}, l={1}", k, l);
//    _logger.Info("Sample informational message, k={0}, l={1}", k, l);
//    _logger.Warn("Sample warning message, k={0}, l={1}", k, l);
//    _logger.Error("Sample error message, k={0}, l={1}", k, l);
//    _logger.Fatal("Sample fatal error message, k={0}, l={1}", k, l);
//    _logger.Log(LogLevel.Info, "Sample informational message, k={0}, l={1}", k, l);
//}

//const string server = "Seq", library = "NLog";

//// Structured logging: two named properties are captured using the message template:
//logger.Info("Hello, {Server}, from {Library}", server, library);

//// Text logging: the two properties are captured using positional arguments:
//logger.Info("Goodbye, {0}, from {1}", server, library);

//// Complex data can be captured and serialized into the event using the `@` directive:
//logger.Info("Current user is {@User}", new { Name = Environment.UserName, Tags = new[] { 1, 2, 3 } });        

//int flow = 300;
//int duration = 10;

//logger.Info("Setting flow to {Rate} L/s for {Duration} s", flow, duration);

//for (var i = 0; i < 100; ++i)
//{
//    logger.Info("Hello, {Name}, on iteration {Counter}", Environment.UserName, i);
//}