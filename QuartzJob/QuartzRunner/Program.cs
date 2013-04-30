using System;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace QuartzRunner
{
    public class Program
    {
        private static IScheduler scheduler;
        public static void Main(string[] args)
        {
//            XmlConfigurator.Configure();

           var properties = new NameValueCollection();
		   properties["quartz.scheduler.instanceName"] = "ServerScheduler";

            // set thread pool info
            properties["quartz.threadPool.threadCount"] = "0";
           
            // set remoting expoter
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = "tcp://localhost:555/QuartzScheduler";

            var scheduleFactory = new StdSchedulerFactory(properties);

            scheduler = scheduleFactory.GetScheduler();
	        var md = scheduler.GetMetaData();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<Jobs.SimpleJob>().Build();
	        
            ITrigger jobTrigger = new SimpleTriggerImpl("TenSecondTrigger", 20, new TimeSpan(0, 0, 0, 5));

			//Periodic scheduling
			scheduler.ScheduleJob(job,jobTrigger);

			//Trigger a job from elsewhere without a trigger for one time
	        var jobKey = job.Key;
	        var jobDataMap = job.JobDataMap;
	        scheduler.TriggerJob(jobKey,jobDataMap);			
        }

        
    }
}
