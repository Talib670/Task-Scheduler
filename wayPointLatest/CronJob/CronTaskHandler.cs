using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskSchedular.Models;

namespace TaskSchedular.CronJob
{
    public class CronTaskHandler
    {
        public CronTaskHandler() { 
        }
        private int CalculateInerval(int noJobs)
        {
            var timeFor1JobInHours = 1.0 / noJobs;
            var timeInSeconds = (double)(timeFor1JobInHours * 3600);
            return Convert.ToInt32(timeInSeconds);
        }

        public async Task  Run() {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            await Console.Out.WriteLineAsync("CronJob Background service running");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            var db = new AppDBContext();
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            await scheduler.Clear();
            string nowdatee = DateTime.Now.ToShortDateString();
            string dbdate = "";

            var date = db.Campaigns.Where( a=> a.status == "Active").FirstOrDefault();
          
                dbdate = date.datetime;

            

            if ((nowdatee) != (dbdate))
            {

                SqlConnection con = new SqlConnection(DecHelper.ConnectionString);
                con.Open();
                string str = @"UPDATE [db_a786af_waypoint].[dbo].[Campaigns] SET [datetime] = '" + DateTime.Now.ToShortDateString() + "',asignedtoday='0'";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
                var compeigns = db.Campaigns.ToList().OrderByDescending(a=>a.Id).Where(a=> Convert.ToDouble(a.leadperday) >= Convert.ToDouble(a.asignedtoday) && a.status== "Active");
            Console.WriteLine(compeigns.Count());
            foreach (var compeign in compeigns )
            {
                var interval = CalculateInerval(Convert.ToInt32(compeign.deliverythrottle));
                var jobId = "JOB:"+compeign.Id+"-INTERVAL:"+ interval.ToString()+" Sec";
                var triggerId = "TRIGGER_" + compeign.Id + "_" + Guid.NewGuid().ToString();

                IJobDetail job = JobBuilder.Create<LeadSender>()
                  .WithIdentity(jobId, "group1")
                  .UsingJobData("CompaignId",compeign.Id)
                  .StoreDurably(true)
                  .Build();

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerId, "group1")
                .StartNow()
                .ForJob(job)
                .WithSimpleSchedule(x =>{
                    x.WithIntervalInSeconds(interval);
                    x.RepeatForever();

                }) .Build();

                await scheduler.ScheduleJob(job, trigger);

            }


        }
   
    
    
    
    }

}
