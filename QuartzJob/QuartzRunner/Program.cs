using System;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using Quartz.Simpl;
using Quartz.Spi;
using log4net.Config;

namespace QuartzRunner
{
    public class Program
    {
        private static IScheduler scheduler;
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteClient";

            // set thread pool info
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // set remoting expoter
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = "tcp://localhost:555/QuartzScheduler";

            var scheduleFactory = new StdSchedulerFactory(properties);

            scheduler = scheduleFactory.GetScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SimpleJob>().Build();

            ITrigger jobTrigger = new SimpleTriggerImpl("TenSecondTrigger", 20, new TimeSpan(0, 0, 0, 5));
            scheduler.ScheduleJob(job, jobTrigger);
        }

        
    }
}
