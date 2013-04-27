using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using QuartzRunner;

namespace QuartzJob.Controllers
{

    //http://stackoverflow.com/questions/11968884/quartz-net-use-one-windows-service-to-execute-jobs-and-two-web-applications-to/11970904#11970904
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult StartMusic()
        {
            //TODO : This will be a common interface used by both runner and the scheduler
            StartJob();
            ViewBag.Started = true;

            return View("Index");
        }

        private void StartJob()
        {
//        http://stackoverflow.com/questions/1356789/how-to-use-quartz-net-with-asp-net
            var properties = new NameValueCollection();
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = "tcp://localhost:555/QuartzScheduler";
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);

            var scheduler = schedulerFactory.GetScheduler();
            
            IJobDetail job = JobBuilder.Create<SimpleJob>().Build();
            job.JobDataMap.Add("triggeredBy", "web");
            ITrigger jobTrigger = new SimpleTriggerImpl("OneTimeTrigger", 1, new TimeSpan());
            scheduler.ScheduleJob(job, jobTrigger);
        
        }
    }
}
