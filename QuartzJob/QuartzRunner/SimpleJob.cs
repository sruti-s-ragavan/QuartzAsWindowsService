using System;
using Quartz;

namespace QuartzRunner
{
    public class SimpleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello World !!By {0} On {1}", context.JobDetail.JobDataMap["triggeredBy"], DateTime.Now);
        }
    }
}