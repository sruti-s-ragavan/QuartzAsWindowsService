using System;
using System.Diagnostics;
using System.IO;
using Quartz;

namespace Jobs
{
    public class SimpleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

			//Jobs.dll should be part of Quartz server directory
            Console.WriteLine("Hello World !!By {0} On {1}", context.JobDetail.JobDataMap["triggeredBy"], DateTime.Now);
	        var file = new StreamWriter(@"C:\Quartz_log.txt",true);
	        var format = string.Join("Hello World !!By {0} On {1}", context.JobDetail.JobDataMap["triggeredBy"], DateTime.Now);
	        file.WriteLine(format +" --Written");
	        file.Flush();
			file.Close();
        }
    }
}