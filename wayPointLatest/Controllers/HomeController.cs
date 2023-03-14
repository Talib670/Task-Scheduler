using ABC.Customer.Domain.DataConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

using static ABC.Customer.Domain.DataConfig.RequestSender;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using RestSharp;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Hosting;

using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Security.Policy;
using Microsoft.VisualBasic;

using Microsoft.Data.SqlClient;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Numerics;
using TaskSchedular.Models;

namespace waypoint.Controllers
{
    public class HomeController : Controller
    {
        public static string username;
        public static string urecby
        {
            get { return username; }
            set { username = value; }

        }
        public static string incorerct;
        public static string recbyyincorrect
        {
            get { return incorerct; }
            set { incorerct = value; }

        }
        AppDBContext d = new AppDBContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult SystemUser()
        {
            if (urecby != null)
            {
                var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
                if (CheckAccess.SystemRoll == "Admin")
                {
                    ViewBag.user = d.AdminLogin.ToList();
              
                return View();
            }
                else
                {
                    return RedirectToAction("error", "Home");
                }
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
            //public class ScheduledTask : BackgroundService
            //{
            //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            //    {
            //        while (!stoppingToken.IsCancellationRequested)
            //        {
            //            Console.WriteLine("Success");

            //        }
            //        await Task.Delay(TimeSpan.FromSeconds(10));
            //    }
            //        }


            //public class ScheduledTask : BackgroundService
            //{
            //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            //    {
            //        while (!stoppingToken.IsCancellationRequested)
            //        {
            //            AppDBContext d = new AppDBContext();

            //            //  string nowdatee = DateTime.Now.ToShortDateString().Substring(2,1);
            //            string nowdatee = DateTime.Now.ToShortDateString();
            //            string dbdate = "";

            //            var compigns = d.Campaigns.ToList();
            //            foreach (var item in compigns)
            //            {
            //                /// dbdate = item.datetime.Substring(2,1);
            //                dbdate = item.datetime;

            //            }

            //            if ((nowdatee) != (dbdate))
            //            {

            //                SqlConnection con = new SqlConnection(DecHelper.ConnectionString);
            //                con.Open();
            //                string str = @"UPDATE [db_a786af_waypoint].[dbo].[Campaigns] SET [datetime] = '" + DateTime.Now.ToShortDateString() + "',asignedtoday='0'";
            //                SqlCommand cmd = new SqlCommand(str, con);
            //                cmd.ExecuteNonQuery();
            //                con.Close();

            //                //var compignsupdate =  d.Campaigns.ToList();
            //                //foreach (var it in compignsupdate)
            //                //{
            //                //    var dataass =d.Campaigns.Find(it.Id);
            //                //    dataass.datetime = DateTime.Now.ToShortDateString();

            //                //    dataass.asignedtoday = (0).ToString();

            //                //    d.Entry(dataass).State = EntityState.Modified;
            //                //    d.SaveChanges();



            //            }
            //            var compign = d.Campaigns.OrderByDescending(o => o.Id).Where(x => x.status == "Active" && Convert.ToInt32(x.leadperday) >= Convert.ToInt32(x.asignedtoday)).ToList();

            //            //int count = d.Campaigns.Where(x => x.status == "Active" && Convert.ToInt32(x.leadperday) >= Convert.ToInt32(x.asignedtoday)).Count();
            //            if (compign.Count >= 1)
            //            {
            //                int[] intervals = new int[compign.Count];
            //                int counter = 0;

            //                foreach (var element in compign)
            //                {
            //                    intervals[counter] = Convert.ToInt32(element.deliverythrottle);
            //                    counter++;
            //                }
            //                HashSet<int> uniqueNumbers = new HashSet<int>(intervals);

            //                int[] result = uniqueNumbers.ToArray();
            //                Array.Sort(result, (x, y) => y.CompareTo(x));

            //                //            // Create an array of campaigns and their intervals
            //                //            int[][] campaigns = {

            //                //    new int[] { 2, 1 }, // Campaign 1 with interval of 10 seconds
            //                //    new int[] { 4, 2 },  // Campaign 2 with interval of 8 seconds
            //                //    new int[] { 6, 3 },  // Campaign 3 with interval of 6 seconds
            //                //    new int[] { 8, 4 },  // Campaign 4 with interval of 4 seconds
            //                //    new int[] { 10, 5 }   // Campaign 5 with interval of 2 seconds
            //                //};


            //                //for (int i = 0; i < result.Length; i++)
            //                for (int i = 0; i < 1; i++)
            //                {

            //                    int interval = result[i];

            //                    List<int> smallvalue = result.Where(o => o <= interval).ToList();
            //                   /// int largevalue = result.Max();
            //                   /// int repeatnumber = 0;
            //                    foreach (var item in smallvalue)
            //                    {
            //                        /// Console.WriteLine(item + "=" + interval / item);
            //                        ///int countertoday = 0;
            //                        //int cc = 1;
            //                        for (int j = 0; j < interval / item; j++)
            //                        {
            //                            Console.WriteLine("Start"+(interval / item));
            //                            ///  Console.WriteLine("Start"+ cc++);
            //                            Console.WriteLine(item);
            //                            var nowcompign = d.Campaigns.Where(x => x.status == "Active" && Convert.ToInt32(x.leadperday) >= Convert.ToInt32(x.asignedtoday) && Convert.ToInt32(x.deliverythrottle) == item).ToList();

            //                            Console.WriteLine(nowcompign.Count()) ;
            //                            foreach (var itemss in nowcompign)
            //                    {



            //                              /// Console.WriteLine(item);
            //                        //Thread threads = new Thread(() => ExecuteCampaignAsync(interval));
            //                        //threads.Start();

            //                        AppDBContext dd = new AppDBContext();
            //                        string comegnname = " ";

            //                        string label = " ";
            //                        string maill = " ";
            //                        string mailla = " ";
            //                        string maillb = " ";
            //                        string maillc = " ";
            //                        string mailld = " ";
            //                        string maille = " ";
            //                        ///var compeginlink = dd.Campaigns.Where(o => Convert.ToInt32(o.deliverythrottle) == Convert.ToInt32(itemss.deliverythrottle) && Convert.ToInt32(o.leadperday) >= Convert.ToInt32(o.asignedtoday)).ToList();
            //                        /// foreach (var item in compeginlink)

            //                        comegnname = itemss.campaignname;
            //                                int asigntoday = 0;

            //                                int totalasign = 0;
            //                                int sqlid = 0;
            //                                ///  Console.WriteLine(comegnname); 
            //                                string sourcname = " ";
            //                        string authkey = " ";
            //                        //string list = " ";
            //                        string apilink = " ";
            //                        string apikey = " ";
            //                        int apilistid = 0;
            //                        label = itemss.label;
            //                        maill = itemss.esp;
            //                        mailla = itemss.espa;
            //                        maillb = itemss.espb;
            //                        maillc = itemss.espc;
            //                        mailld = itemss.espd;
            //                        maille = itemss.espe;

            //                        /// var findresoursename = dd.Campaigns.Where(o => o.Id == (@itemss.Id)).ToList();
            //                        //foreach (var it in findresoursename)
            //                        //{
            //                        sourcname = itemss.sourcename;

            //                                asigntoday = Convert.ToInt32(itemss.asignedtoday);
            //                                Console.WriteLine(asigntoday);
            //                                //Console.WriteLine("today"+ (asigntoday+countertoday++));
            //                                //Console.WriteLine("today"+ (totalasign+ countertoday++));
            //                                totalasign = Convert.ToInt32(itemss.totalasign);
            //                        sqlid = Convert.ToInt32(itemss.Id);
            //                        apikey = itemss.auth_key;
            //                        apilink = itemss.destination;
            //                        apilistid = itemss.list_id;
            //                        string str = apilink;
            //                        string res = str.Substring(0, 25);


            //                        var findresoursenamefromsource = dd.source.Where(o => o.Name == (sourcname).ToString()).Take(1).ToList();
            //                        foreach (var items in findresoursenamefromsource)
            //                        {
            //                            authkey = items.auth_key;
            //                        }

            //                        var leadlist = dd.sourcelead.OrderByDescending(o => o.Id).Where(o => o.auth_key == (authkey).ToString() && o.asignto == null && o.archieve != "false" && o.archieve!= "Unverified" && o.esp == maill || o.esp == mailla || o.esp == maillb || o.esp == maillc || o.esp == mailld || o.esp == maille).Take(1).ToList();

            //                        foreach (var lead in leadlist)
            //                        {


            //                                    //var client = new HttpClient();

            //                                    //var data = new { first_name = lead.first_name, email = lead.email, phone = lead.phone, dob = lead.dob, age = lead.age };

            //                                    //var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            //                                    //string apiUrl = apilink;
            //                                    //string url = apiUrl + "/" + @item.destination;
            //                                    //var response = await client.PostAsync(url, content);


            //                                    var emailjson = JsonConvert.SerializeObject(new
            //                                    {



            //                                        ListId = "162513",

            //                                        Email = lead.email
            //                                    });

            //                                    // string json = "{\"ListId\":162513,\"Email\":\"munirahmad20786@gmail.com\"}";

            //                                    var client = new RestClient("https://api.emailoversight.com/api/emailvalidation");
            //                                    var request = new RestRequest("https://api.emailoversight.com/api/emailvalidation", Method.Post);
            //                                    request.AddHeader("accept", "application/json");
            //                                    request.AddHeader("content-type", "application/json");
            //                                    request.AddHeader("apitoken", "f09fd596-0253-47d9-98a7-bf66dcc97f2e");
            //                                    //  request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"{lead.email}\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"{lead.first_name}\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":true,\"email\":\"{lead.email}\"}", ParameterType.RequestBody);
            //                                    request.AddParameter("application/json", emailjson, ParameterType.RequestBody);
            //                                    RestResponse response = client.Execute(request);
            //                                    dynamic responseObj = JsonConvert.DeserializeObject(response.Content);
            //                                    //Console.WriteLine(response.Content);
            //                                    // Extract the value of the "Result" property
            //                                    string resultss = responseObj.Result;
            //                                   /// Console.WriteLine(resultss);
            //                                    if (resultss == "Verified")
            //                                    {

            //                                if (res == "https://app.campaignrefin")
            //                                {

            //                                    var clientts = new RestClient(apilink);
            //                                    var requesttss = new RestRequest(apilink, Method.Post);

            //                                    requesttss.AddHeader("accept", "application/x-www-form-urlencoded");

            //                                    requesttss.AddHeader("content-type", "application/x-www-form-urlencoded");
            //                                    /// requesttss.AddHeader("api-key", "le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth");

            //                                    ///  requesttss.AddParameter("application/x-www-form-urlencoded", "{\"immediate_cleaning\":0,\"key\":\"le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth\",\"email\":\"a@gmail.com\",\"first_name\":\"asdsd\",\"last_name\":\"dssds\"}", ParameterType.RequestBody);
            //                                    requesttss.AddParameter("application/x-www-form-urlencoded", "immediate_cleaning=0&key='" + apikey + "'&email='" + lead.email + "'&first_name='" + lead.first_name + "'", ParameterType.RequestBody);
            //                                    RestResponse responseds = clientts.Execute(requesttss);

            //                                    if (responseds.IsSuccessStatusCode)
            //                                    {

            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                        dataa.asignto = (comegnname).ToString();
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();

            //                                    }
            //                                    else
            //                                    {
            //                                        // There was an error
            //                                    }
            //                                }
            //                                if (res == "https://api.sendinblue.co" || res == "https://my.sendinblue.co")
            //                                {
            //                                    int[] apiListIds = { apilistid };
            //                                    List<int> listIds = apiListIds.ToList();
            //                                    ///List<int> listId = new List<int> {apilistid};

            //                                    var json = JsonConvert.SerializeObject(new
            //                                    {

            //                                        attributes = new
            //                                        {
            //                                            Email = lead.email,
            //                                            LASTNAME = lead.last_name,
            //                                            FIRSTNAME = lead.first_name,
            //                                            CITY = " ",
            //                                            STATE = " ",
            //                                            PHONE = lead.phone,

            //                                            SUBID1 = label
            //                                        },

            //                                        listIds = listIds.ToArray(),
            //                                        updateEnabled = true,
            //                                        email = lead.email
            //                                    });


            //                                    var clientss = new RestClient(apilink);
            //                                    var requestss = new RestRequest(apilink, Method.Post);
            //                                    requestss.AddHeader("accept", "application/json");
            //                                    requestss.AddHeader("content-type", "application/json");
            //                                    requestss.AddHeader("api-key", apikey);
            //                                    //  request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"{lead.email}\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"{lead.first_name}\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":true,\"email\":\"{lead.email}\"}", ParameterType.RequestBody);
            //                                    requestss.AddParameter("application/json", json, ParameterType.RequestBody);
            //                                    RestResponse responsess = clientss.Execute(requestss);

            //                                            ///Console.WriteLine(responsess.ToString());   
            //                                    //var client = new RestClient(apilink);
            //                                    //var request = new RestRequest(apilink, Method.Post);
            //                                    //request.AddHeader("accept", "application/json");
            //                                    //request.AddHeader("content-type", "application/json");
            //                                    //request.AddHeader("api-key", apikey);
            //                                    //// request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"'"+lead.email+ "'\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"'"+ lead.first_name + "'\",\"STATE\":\"Punjab\",\"PHONE\":\"'" + lead.phone+ "'\"},\"listIds\":["+apilistid +"],\"updateEnabled\":false,\"email\":\"'"+lead.email+"'\"}", ParameterType.RequestBody);
            //                                    //request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"khairpur@gmail.comdsd\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"mmm\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":false,\"email\":\"khairpur@gmail.com\"}", ParameterType.RequestBody);
            //                                    //RestResponse response = client.Execute(request);

            //                                    if (responsess.IsSuccessStatusCode)
            //                                    {

            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                        dataa.asignto = (comegnname).ToString();
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();

            //                                        string datetime = DateTime.Now.ToShortDateString();
            //                                        var dataas = dd.Campaigns.Where(xm => xm.datetime == datetime && xm.Id == Convert.ToInt32(sqlid)).FirstOrDefault();
            //                                        if (dataas != null)

            //                                        {
            //                                                    //  Console.WriteLine(comegnname);
            //                                                    /// Console.WriteLine(asigntoday + countertoday++);

            //                                            dataas.asignedtoday = (asigntoday +(interval / item)).ToString();
            //                                            dataas.totalasign = (totalasign + (interval / item)).ToString();

            //                                            dd.Entry(dataas).State = EntityState.Modified;
            //                                            dd.SaveChanges();
            //                                                    Console.WriteLine(comegnname+"asigntoday:" + (asigntoday +1));
            //                                                    // Console.WriteLine("Success");
            //                                                }
            //                                        else
            //                                        {
            //                                            var dataass = dd.Campaigns.Where(x => x.Id == Convert.ToInt32(sqlid)).FirstOrDefault();
            //                                            dataass.datetime = DateTime.Now.ToShortDateString();
            //                                            dataass.asignedtoday = (1).ToString();
            //                                            dataass.totalasign = (totalasign + 1).ToString();
            //                                            dd.Entry(dataass).State = EntityState.Modified;
            //                                            dd.SaveChanges();
            //                                        }

            //                                    }
            //                                    else
            //                                    {

            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));
            //                                                Console.WriteLine("false");
            //                                        dataa.archieve = "false";
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();
            //                                    }
            //                                    //Thread.Sleep(interval * 1000);


            //                                    //var clientt = new RestClient("https://my.sendinblue.com/users/list/id/4");

            //                                    //var requestts = new RestRequest(apilink, Method.Post);

            //                                    //requestts.AddHeader("accept", "application/json");

            //                                    //requestts.AddHeader("content-type", "application/json");
            //                                    //requestts.AddHeader("api-key", apikey);

            //                                    //requestts.AddParameter("application/json", "{\"jsonBody\":[{\"email\":\"'"+ lead.email+ "'\",\"attributes\":{\"LASTNAME\":\"Noemi\",\"FIRSTNAME \":\"'"+ lead.first_name+ "'\",\"COUNTRY\":\"DE\",\"CITY\":\"Bahawalpur\",\"BIRTHDAY\":\"'"+ lead.dob + "'\",\"PREFERED_COLOR\":\"BLACK\"}}],\"newList\":{\"listName\":\"ContactImport - 2017-05\",\"folderId\":5},\"emailBlacklist\":false,\"smsBlacklist\":false,\"updateExistingContacts\":true,\"emptyContactsAttributes\":true}", ParameterType.RequestBody);
            //                                    //RestResponse responsed = clientt.Execute(requestts);
            //                                    //if (responsed.IsSuccessStatusCode)
            //                                    //{

            //                                    //    var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                    //    dataa.asignto = (comegnname).ToString();
            //                                    //    dd.Entry(dataa).State = EntityState.Modified;
            //                                    //    dd.SaveChanges();

            //                                    //}
            //                                }
            //                                        ///System.Threading.Thread.Sleep(TimeSpan.FromSeconds(interval));
            //                                        /// await Task.Delay(interval * 1000);
            //                                        //await Task.Delay(TimeSpan.FromSeconds(10));
            //                                        ///Console.WriteLine("10 Second");
            //                                    }
            //                                    else
            //                                    {
            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));
            //                                        Console.WriteLine("Unverified"); 
            //                                        dataa.archieve = "Unverified";
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();
            //                                    }
            //                                }

            //                    }


            //                }

            //                }

            //                    /// Console.WriteLine("delay");
            //                    int timesleep = interval / 2;
            //                    await Task.Delay(timesleep * 1000);

            //                    //await Task.Delay(interval * 1000);

            //                }

            //                // Your task code here
            //                //await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            //            }

            //            else
            //            {
            //                await Task.Delay(TimeSpan.FromSeconds(10));
            //            }
            //        }
            //    }
            //}





            //public class ScheduledTask : BackgroundService
            //{
            //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            //    {
            //        while (!stoppingToken.IsCancellationRequested)
            //        {
            //            AppDBContext d = new AppDBContext();

            //            //  string nowdatee = DateTime.Now.ToShortDateString().Substring(2,1);
            //            string nowdatee = DateTime.Now.ToShortDateString();
            //            string dbdate = "";

            //            var compigns = d.Campaigns.Take(1).ToList();
            //            foreach (var item in compigns)
            //            {
            //                /// dbdate = item.datetime.Substring(2,1);
            //                dbdate = item.datetime;

            //            }

            //            if ((nowdatee) != (dbdate))
            //            {

            //                SqlConnection con = new SqlConnection(DecHelper.ConnectionString);
            //                con.Open();
            //                string str = @"UPDATE [db_a786af_waypoint].[dbo].[Campaigns] SET [datetime] = '" + DateTime.Now.ToShortDateString() + "',asignedtoday='0'";
            //                SqlCommand cmd = new SqlCommand(str, con);
            //                cmd.ExecuteNonQuery();
            //                con.Close();

            //                //var compignsupdate =  d.Campaigns.ToList();
            //                //foreach (var it in compignsupdate)
            //                //{
            //                //    var dataass =d.Campaigns.Find(it.Id);
            //                //    dataass.datetime = DateTime.Now.ToShortDateString();

            //                //    dataass.asignedtoday = (0).ToString();

            //                //    d.Entry(dataass).State = EntityState.Modified;
            //                //    d.SaveChanges();



            //            }
            //            var compign = d.Campaigns.OrderByDescending(o => o.Id).Where(x => x.status == "Active" && Convert.ToInt32(x.leadperday) >= Convert.ToInt32(x.asignedtoday)).ToList();

            //            //int count = d.Campaigns.Where(x => x.status == "Active" && Convert.ToInt32(x.leadperday) >= Convert.ToInt32(x.asignedtoday)).Count();
            //            if (compign.Count >= 1)
            //            {
            //                int[] intervals = new int[compign.Count];
            //                int counter = 0;

            //                foreach (var element in compign)
            //                {
            //                    intervals[counter] = Convert.ToInt32(element.deliverythrottle);
            //                    counter++;
            //                }
            //                HashSet<int> uniqueNumbers = new HashSet<int>(intervals);

            //                int[] result = uniqueNumbers.ToArray();

            //                //            // Create an array of campaigns and their intervals
            //                //            int[][] campaigns = {

            //                //    new int[] { 2, 1 }, // Campaign 1 with interval of 10 seconds
            //                //    new int[] { 4, 2 },  // Campaign 2 with interval of 8 seconds
            //                //    new int[] { 6, 3 },  // Campaign 3 with interval of 6 seconds
            //                //    new int[] { 8, 4 },  // Campaign 4 with interval of 4 seconds
            //                //    new int[] { 10, 5 }   // Campaign 5 with interval of 2 seconds
            //                //};


            //                for (int i = 0; i < result.Length; i++)
            //                {
            //                    int interval = intervals[i];

            //                    var nowcompign = d.Campaigns.Where(x => x.status == "Active" && Convert.ToInt32(x.leadperday) >= Convert.ToInt32(x.asignedtoday) && Convert.ToInt32(x.deliverythrottle) <= interval).ToList();
            //                    foreach (var itemss in nowcompign)
            //                    {

            //                        //Thread threads = new Thread(() => ExecuteCampaignAsync(interval));
            //                        //threads.Start();

            //                        AppDBContext dd = new AppDBContext();
            //                        string comegnname = " ";
            //                        int asigntoday = 0;
            //                        int totalasign = 0;
            //                        int sqlid = 0;
            //                        string label = " ";
            //                        string maill = " ";
            //                        string mailla = " ";
            //                        string maillb = " ";
            //                        string maillc = " ";
            //                        string mailld = " ";
            //                        string maille = " ";
            //                        ///var compeginlink = dd.Campaigns.Where(o => Convert.ToInt32(o.deliverythrottle) == Convert.ToInt32(itemss.deliverythrottle) && Convert.ToInt32(o.leadperday) >= Convert.ToInt32(o.asignedtoday)).ToList();
            //                        /// foreach (var item in compeginlink)

            //                        comegnname = itemss.campaignname;

            //                        string sourcname = " ";
            //                        string authkey = " ";
            //                        //string list = " ";
            //                        string apilink = " ";
            //                        string apikey = " ";
            //                        int apilistid = 0;
            //                        label = itemss.label;
            //                        maill = itemss.esp;
            //                        mailla = itemss.espa;
            //                        maillb = itemss.espb;
            //                        maillc = itemss.espc;
            //                        mailld = itemss.espd;
            //                        maille = itemss.espe;



            //                        /// var findresoursename = dd.Campaigns.Where(o => o.Id == (@itemss.Id)).ToList();
            //                        //foreach (var it in findresoursename)
            //                        //{
            //                        sourcname = itemss.sourcename;
            //                        asigntoday = Convert.ToInt32(itemss.asignedtoday);
            //                        totalasign = Convert.ToInt32(itemss.totalasign);
            //                        sqlid = Convert.ToInt32(itemss.Id);
            //                        apikey = itemss.auth_key;
            //                        apilink = itemss.destination;
            //                        apilistid = itemss.list_id;
            //                        string str = apilink;
            //                        string res = str.Substring(0, 25);


            //                        var findresoursenamefromsource = dd.source.Where(o => o.Name == (sourcname).ToString()).Take(1).ToList();
            //                        foreach (var items in findresoursenamefromsource)
            //                        {
            //                            authkey = items.auth_key;
            //                        }

            //                        var leadlist = dd.sourcelead.OrderByDescending(o => o.Id).Where(o => o.auth_key == (authkey).ToString() && o.asignto == null && o.archieve != "false"&& o.esp==maill || o.esp==mailla||o.esp==maillb || o.esp==maillc || o.esp==mailld || o.esp==maille).Take(1).ToList();

            //                        foreach (var lead in leadlist)
            //                        {


            //                            //var client = new HttpClient();

            //                            //var data = new { first_name = lead.first_name, email = lead.email, phone = lead.phone, dob = lead.dob, age = lead.age };

            //                            //var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            //                            //string apiUrl = apilink;
            //                            //string url = apiUrl + "/" + @item.destination;
            //                            //var response = await client.PostAsync(url, content);
            //                            var emailjson = JsonConvert.SerializeObject(new
            //                            {



            //                                ListId = "162513",

            //                                Email = lead.email
            //                            });

            //                            // string json = "{\"ListId\":162513,\"Email\":\"munirahmad20786@gmail.com\"}";

            //                            var client = new RestClient("https://api.emailoversight.com/api/emailvalidation");
            //                            var request = new RestRequest("https://api.emailoversight.com/api/emailvalidation", Method.Post);
            //                            request.AddHeader("accept", "application/json");
            //                            request.AddHeader("content-type", "application/json");
            //                            request.AddHeader("apitoken", "f09fd596-0253-47d9-98a7-bf66dcc97f2e");
            //                            //  request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"{lead.email}\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"{lead.first_name}\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":true,\"email\":\"{lead.email}\"}", ParameterType.RequestBody);
            //                            request.AddParameter("application/json", emailjson, ParameterType.RequestBody);
            //                            RestResponse response = client.Execute(request);
            //                            dynamic responseObj = JsonConvert.DeserializeObject(response.Content);

            //                            // Extract the value of the "Result" property
            //                            string resultss = responseObj.Result;

            //                            if (resultss == "Verified")
            //                            {
            //                                if (res == "https://app.campaignrefin")
            //                                {

            //                                    var clientts = new RestClient(apilink);
            //                                    var requesttss = new RestRequest(apilink, Method.Post);

            //                                    requesttss.AddHeader("accept", "application/x-www-form-urlencoded");

            //                                    requesttss.AddHeader("content-type", "application/x-www-form-urlencoded");
            //                                    /// requesttss.AddHeader("api-key", "le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth");

            //                                    ///  requesttss.AddParameter("application/x-www-form-urlencoded", "{\"immediate_cleaning\":0,\"key\":\"le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth\",\"email\":\"a@gmail.com\",\"first_name\":\"asdsd\",\"last_name\":\"dssds\"}", ParameterType.RequestBody);
            //                                    requesttss.AddParameter("application/x-www-form-urlencoded", "immediate_cleaning=0&key='" + apikey + "'&email='" + lead.email + "'&first_name='" + lead.first_name + "'", ParameterType.RequestBody);
            //                                    RestResponse responseds = clientts.Execute(requesttss);

            //                                    if (responseds.IsSuccessStatusCode)
            //                                    {

            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                        dataa.asignto = (comegnname).ToString();
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();

            //                                    }
            //                                    else
            //                                    {
            //                                        // There was an error
            //                                    }
            //                                }
            //                                if (res == "https://api.sendinblue.co" || res == "https://my.sendinblue.co")
            //                                {
            //                                    int[] apiListIds = { apilistid };
            //                                    List<int> listIds = apiListIds.ToList();
            //                                    ///List<int> listId = new List<int> {apilistid};

            //                                    var json = JsonConvert.SerializeObject(new
            //                                    {

            //                                        attributes = new
            //                                        {
            //                                            Email = lead.email,
            //                                            LASTNAME = lead.last_name,
            //                                            FIRSTNAME = lead.first_name,
            //                                            CITY = " ",
            //                                            STATE = " ",
            //                                            PHONE = lead.phone,

            //                                            SUBID1 = label
            //                                        },

            //                                        listIds = listIds.ToArray(),
            //                                        updateEnabled = true,
            //                                        email = lead.email
            //                                    });


            //                                    var clientss = new RestClient(apilink);
            //                                    var requestss = new RestRequest(apilink, Method.Post);
            //                                    requestss.AddHeader("accept", "application/json");
            //                                    requestss.AddHeader("content-type", "application/json");
            //                                    requestss.AddHeader("api-key", apikey);
            //                                    //  request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"{lead.email}\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"{lead.first_name}\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":true,\"email\":\"{lead.email}\"}", ParameterType.RequestBody);
            //                                    requestss.AddParameter("application/json", json, ParameterType.RequestBody);
            //                                    RestResponse responsess = clientss.Execute(requestss);


            //                                    //var client = new RestClient(apilink);
            //                                    //var request = new RestRequest(apilink, Method.Post);
            //                                    //request.AddHeader("accept", "application/json");
            //                                    //request.AddHeader("content-type", "application/json");
            //                                    //request.AddHeader("api-key", apikey);
            //                                    //// request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"'"+lead.email+ "'\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"'"+ lead.first_name + "'\",\"STATE\":\"Punjab\",\"PHONE\":\"'" + lead.phone+ "'\"},\"listIds\":["+apilistid +"],\"updateEnabled\":false,\"email\":\"'"+lead.email+"'\"}", ParameterType.RequestBody);
            //                                    //request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"khairpur@gmail.comdsd\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"mmm\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":false,\"email\":\"khairpur@gmail.com\"}", ParameterType.RequestBody);
            //                                    //RestResponse response = client.Execute(request);

            //                                    if (responsess.IsSuccessStatusCode)
            //                                    {

            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                        dataa.asignto = (comegnname).ToString();
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();

            //                                        string datetime = DateTime.Now.ToShortDateString();
            //                                        var dataas = dd.Campaigns.Where(xm => xm.datetime == datetime && xm.Id == Convert.ToInt32(sqlid)).FirstOrDefault();
            //                                        if (dataas != null)

            //                                        {

            //                                            dataas.asignedtoday = (asigntoday + 1).ToString();
            //                                            dataas.totalasign = (totalasign + 1).ToString();
            //                                            dd.Entry(dataas).State = EntityState.Modified;
            //                                            dd.SaveChanges();
            //                                        }
            //                                        else
            //                                        {
            //                                            var dataass = dd.Campaigns.Where(x => x.Id == Convert.ToInt32(sqlid)).FirstOrDefault();
            //                                            dataass.datetime = DateTime.Now.ToShortDateString();
            //                                            dataass.asignedtoday = (1).ToString();
            //                                            dataass.totalasign = (totalasign + 1).ToString();
            //                                            dd.Entry(dataass).State = EntityState.Modified;
            //                                            dd.SaveChanges();
            //                                        }

            //                                    }
            //                                    else
            //                                    {

            //                                        var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                        dataa.archieve = "false";
            //                                        dd.Entry(dataa).State = EntityState.Modified;
            //                                        dd.SaveChanges();
            //                                    }
            //                                    //Thread.Sleep(interval * 1000);


            //                                    //var clientt = new RestClient("https://my.sendinblue.com/users/list/id/4");

            //                                    //var requestts = new RestRequest(apilink, Method.Post);

            //                                    //requestts.AddHeader("accept", "application/json");

            //                                    //requestts.AddHeader("content-type", "application/json");
            //                                    //requestts.AddHeader("api-key", apikey);

            //                                    //requestts.AddParameter("application/json", "{\"jsonBody\":[{\"email\":\"'"+ lead.email+ "'\",\"attributes\":{\"LASTNAME\":\"Noemi\",\"FIRSTNAME \":\"'"+ lead.first_name+ "'\",\"COUNTRY\":\"DE\",\"CITY\":\"Bahawalpur\",\"BIRTHDAY\":\"'"+ lead.dob + "'\",\"PREFERED_COLOR\":\"BLACK\"}}],\"newList\":{\"listName\":\"ContactImport - 2017-05\",\"folderId\":5},\"emailBlacklist\":false,\"smsBlacklist\":false,\"updateExistingContacts\":true,\"emptyContactsAttributes\":true}", ParameterType.RequestBody);
            //                                    //RestResponse responsed = clientt.Execute(requestts);
            //                                    //if (responsed.IsSuccessStatusCode)
            //                                    //{

            //                                    //    var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                    //    dataa.asignto = (comegnname).ToString();
            //                                    //    dd.Entry(dataa).State = EntityState.Modified;
            //                                    //    dd.SaveChanges();

            //                                    //}
            //                                }
            //                                ///System.Threading.Thread.Sleep(TimeSpan.FromSeconds(interval));
            //                                await Task.Delay(interval * 1000);
            //                            }
            //                            else
            //                            {
            //                                var dataa = dd.sourcelead.Find(Convert.ToInt32(lead.Id));

            //                                dataa.archieve = "Unverified";
            //                                dd.Entry(dataa).State = EntityState.Modified;
            //                                dd.SaveChanges();
            //                            }
            //                        }

            //                    }




            //                    //await Task.Delay(interval * 1000);

            //                }

            //                // Your task code here
            //                //await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            //            }

            //            else
            //            {
            //                await Task.Delay(TimeSpan.FromSeconds(10));
            //            }
            //        }
            //    }
            //}





            //static void Main(string[] args)
            //{ 
            //    static Timer timer;

            //    // Set up a timer to trigger every 10 minutes
            //    timer = new Timer(3000);
            //    timer.Elapsed += Timer_Elapsed;
            //    timer.Start();

            //    Console.WriteLine("Press any key to exit...");
            //    Console.ReadKey();

            //    // Clean up the timer
            //    timer.Stop();
            //    timer.Dispose();
            //}


            public IActionResult Index()
        {
            var client = new RestClient("https://api.sendinblue.com/v3/contacts");
            var request = new RestRequest("https://api.sendinblue.com/v3/contacts",Method.Post);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("api-key", "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");
            request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"khairpur@gmail.comdsd\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"mmm\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":false,\"email\":\"khairpur@gmail.com\"}", ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult everflow_offer()
        {
            ViewBag.comeign = d.Campaigns.ToList();
            ViewBag.sourcelead = d.sourcelead.ToList();
            ViewBag.source = d.source.ToList();
            /// ViewBag.source = d.source.Where(o=> o.asignto==null || o.asignto==" " ).ToList();
            return View();
        }
        public IActionResult gmail()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult checkvalid()
        {
            string username = Request.Form["username"].ToString();
            string password = Request.Form["password"].ToString();

            var count = d.AdminLogin.Where(o => o.Gmail == username && o.Password == password ).Count();
            if (count >= 1)
            {
                ViewBag.use = d.AdminLogin.Where(o => o.Gmail == username && o.Password == password).ToList();
                foreach (var i in ViewBag.use)
                {
                    ViewBag.user = i.UserName;
                    urecby = i.Gmail;


                }
                return RedirectToAction("welcome", "Home");
            }
            else
            {
                return RedirectToAction("login", "Home");
            }

        }

        public IActionResult login()
        {
            ViewBag.incorrect = recbyyincorrect;
            recbyyincorrect = " ";
            return View();
        }
        //[HttpGet]
        //public IActionResult delete(string sourceid)
        //{
        //    var data = d.Campaigns.Find(Convert.ToInt32(sourceid));
            
        //    data.status = "archive";
        //    d.Entry(data).State = EntityState.Modified;
        //    d.SaveChanges();
        //    return RedirectToAction("compeign", "Home");
        //}
        [HttpGet]
        public IActionResult restore(string sourceid)
        {
            var data = d.Campaigns.Find(Convert.ToInt32(sourceid));

            data.status = "Active";
            d.Entry(data).State = EntityState.Modified;
            d.SaveChanges();
            return RedirectToAction("compeign", "Home");
        }

        public async Task<string> hitapiAsync(string sourceid)
        {
            ///garbage of campaign Refinery

            //       var clientts = new RestClient(CompaignData.destination);
            //       var requesttss = new RestRequest(CompaignData.destination, Method.Post);

            //       requesttss.AddHeader("accept", "application/x-www-form-urlencoded");
            //      requesttss.AddHeader("content-type", "application/x-www-form-urlencoded");

            //requesttss.AddHeader("key", "le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth");

            //   // serialize the object to JSON format

            // //  requesttss.AddParameter("application/json", "{\"immediate_cleaning\":0,\"key\":\"le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth\",\"email\":\"a@gmail.com\",\"first_name\":\"asdsd\",\"last_name\":\"dssds\"}", ParameterType.RequestBody);
            //   ////requesttss.AddParameter("application/json", json, ParameterType.RequestBody);
            //   requesttss.AddParameter("application/x-www-form-urlencoded", "immediate_cleaning=0&&email='" + lead.email + "'&&first_name='" + lead.first_name + "' &&last_name='Khan'", ParameterType.RequestBody);
            //       RestResponse responseds = await clientts.ExecuteAsync(requesttss);





            //var client = new RestClient("https://app.campaignrefinery.com/rest/contacts/create-contact");
            //var request = new RestRequest("https://app.campaignrefinery.com/rest/contacts/create-contact", Method.Post);
            //request.AddHeader("accept", "application/json");
            //request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\"immediate_cleaning\":0,\"key\":\"le6w4bjm0saaqwz7qpdduyhfxxnsykcztogbvnth\",\"email\":\"usman20@gmail.com\",\"first_name\":\"Munir\",\"last_name\":\"mmm\"}", ParameterType.RequestBody);
            //RestResponse response = client.Execute(request);




            var client = new HttpClient();

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("key", "vtg6f3vjwy08mtiwdrhkeo4udzf7lygqnprulhqx"));
            postData.Add(new KeyValuePair<string, string>("first_name", "Camapaign"));
            postData.Add(new KeyValuePair<string, string>("last_name", "khan"));
            postData.Add(new KeyValuePair<string, string>("email", "nadeem@gmail.com"));
            postData.Add(new KeyValuePair<string, string>("tags", "189818f8-a1f6-4fc4-8af9-75c41f4e6c23"));
            var content = new FormUrlEncodedContent(postData);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://app.campaignrefinery.com/rest/contacts/subscribe")
          
            {
                Content = content
            };

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("HTTP Error: " + response.StatusCode + " - " + responseBody);
            }

            return responseBody;
            ///return Ok();
        }
        [HttpGet]
        public IActionResult delete_provider(string providerid)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            {
                var data = d.provider.Find(Convert.ToInt32(providerid));
                d.provider.Remove(data);
                d.SaveChanges();
                return RedirectToAction("provider", "Home");
            }
            
        }
        [HttpGet]
        public IActionResult edit(string sourceid)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
          else
            {
                ViewBag.Campaigns = d.Campaigns.Where(o => o.Id == Convert.ToInt32(sourceid)).ToList();
                /// ViewBag.sourcelead = d.sourcelead.ToList();
                ViewBag.source = d.source.ToList();
                return View();
            }
           
        }
        [HttpGet]
        public IActionResult UserEdit(string tid)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
           
                if (CheckAccess.SystemRoll == "Visitor")
                {
                    return RedirectToAction("error", "Home");
                }
                else
            {
                ViewBag.UserData = d.AdminLogin.Where(o => o.AdminLoginId == Convert.ToInt32(tid)).ToList();
            
            return View();
        }
           
        }
        [HttpGet]
        public IActionResult edit_datasource(string sourceid)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            {
                ViewBag.Campaigns = d.source.Where(o => o.Id == Convert.ToInt32(sourceid)).ToList();
                /// ViewBag.sourcelead = d.sourcelead.ToList();

                ViewBag.vendor = d.provider.ToList();
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult edit_provider(string providerid)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            {
                ViewBag.provider = d.provider.Where(o => o.Id == Convert.ToInt32(providerid)).ToList();

                return View();
            }
           
        }
        public IActionResult welcome()
        {
            if (urecby != null)
            {

                return View();
        }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult servertime()
        {

            return View();
        }
        public IActionResult provider()
        {
            if (urecby != null)
            {
                var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
                if (CheckAccess.AccessProvider != null)
                { 
                    ViewBag.vendor = d.provider.ToList();
            return View();
        }
                else
                {
                    return RedirectToAction("error", "Home");
                }
        }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult source()
        {

            if (urecby != null)
            {
                var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
               if(CheckAccess.AccessProvider!=null)
                { 
                ViewBag.vendor = d.provider.ToList();
            ViewBag.source = d.source.ToList();
            return View();
        }
                else
                {
                    return RedirectToAction("error", "Home");
                }
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult customers()
        {
            if (urecby != null)
            {
                return View();
        }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult campaigns()
        {
            if (urecby != null)
            {
                return View();
        }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult ping_trees()
        {
            if (urecby != null)
            {
                return View();
        }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult search()
        {
            if (urecby != null)
            {
                return View();
        }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult lead_mining()
        {
            return View();
        }
        public IActionResult reports()
        {
            return View();
        }
        public  IActionResult campaign()
        {
            if (urecby != null)
            {
                var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
                if (CheckAccess.AccessProvider != null)

                { 
                    ViewBag.comeign = d.Campaigns.Where(o=> o.status!= "archive" ).OrderByDescending(o=> o.Id).ToList();
            ///ViewBag.sourcelead =await d.sourcelead.ToListAsync();
            ViewBag.source = d.source.ToList();
           /// ViewBag.source = d.source.Where(o=> o.asignto==null || o.asignto==" " ).ToList();
            return View();
        }
                else
                {
                    return RedirectToAction("error", "Home");
                }
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult restore_compeign()
        {
            if (urecby != null)
            {
                ViewBag.comeign = d.Campaigns.Where(o => o.status == "archive").ToList();
                ///ViewBag.sourcelead =await d.sourcelead.ToListAsync();
                ViewBag.source = d.source.ToList();
                /// ViewBag.source = d.source.Where(o=> o.asignto==null || o.asignto==" " ).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        [HttpGet]
        public string getsourcelist(string sourcetid)
        {

            string authkey = "";
            //  var record = d.tblsections.Where(o => o.classname == id).ToList();
            var authtoken = d.source.Where(o => o.Name == sourcetid).ToList().Take(1) ;
            foreach (var i in authtoken)
            {
                authkey = i.auth_key;
            }
            var citylist = d.sourcelist.Where(o => o.sourcename == authkey).ToList();
            string html = " ";
           
            html += "<option value=" + 0 + ">" + "Select Section" + "</option> ";
            
            foreach (var item in citylist)
            {
                html += "<option value=" + item.list + ">" + item.list + "</option> ";
          
            }
            return html;

        }
        [HttpPost]
        public async Task<IActionResult> save_campaign(string tid, List<string> ListAttributes, string espa, string espb,string espc, string espd, string espe, string espf, string status, string listid, string destination,string sourcelist, string source, string postpone_interval,string notes,string delivery_delay,string label_name,string rate,string leadperday, string totalleads,string valid_responses,string ref_key,string compeignname,string Customer,string exclusivity)
        {
           
                var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            { 
                    string Labels = null;
            string Phone = null;
            string LastName = null;
            string FirstName = null;

            foreach (var item in ListAttributes)
            {
                Console.WriteLine(item);
                if (item== "FirstName")
                {
                    FirstName = item;
                }
                if (item== "Label")
                {
                    Labels = item;

                }
                if (item == "Phone")
                {
                    Phone = item;

                }
                if (item == "LastName")
                {
                    LastName = item;

                }
            }
           
            if (tid!=null && tid!=" " && tid!=string.Empty )
            {
                ///int perseconds = Convert.ToInt32(Convert.ToInt32(delivery_delay) * Convert.ToInt32(60) * Convert.ToInt32(60)) / Convert.ToInt32(totalleads);
               
                var data = d.Campaigns.Find(Convert.ToInt32(tid));
                data.campaignname = (compeignname).ToString();
                 data.notes = notes;
                 data.PostponeDelivery = postpone_interval;
                 data.label = label_name;
                 data.totalleads = totalleads;
                 data.validresponses = valid_responses;
                 data.rate = rate;
                 data.destination = destination;
                 data.campaignname = compeignname;
                 data.auth_key = ref_key;
                 data.deliverythrottle = totalleads;
                 data.status = status;
                 data.sourcename = source;
                 data.leadperday = leadperday;
                 data.list = sourcelist;
                data.esp = espa;
                data.espa = espb;
                data.espb = espc;
                data.espc = espd;
                data.espd = espe;
                data.espe = espf;
                data.FirstName = FirstName;
                data.LastName = LastName;
                data.Labels = Labels;
                data.Phone = Phone;
               
                 data.list_id = Convert.ToInt32(listid);
                d.Entry(data).State = EntityState.Modified;
                d.SaveChanges();

                int id = 0;
                var findsourcid = await d.source.Where(o => o.sourcecompanyname == source).ToListAsync();
                foreach (var i in findsourcid)
                {
                    id = i.Id;
                }
                var SourceData = d.source.Find(Convert.ToInt32(id));

                SourceData.asignto = (compeignname).ToString();
                d.Entry(SourceData).State = EntityState.Modified;
                await d.SaveChangesAsync();
                return RedirectToAction("compeign", "Home");
            }
            else
            {
                
            if (source != null || source != " " || source!=string.Empty || listid!=string.Empty || listid!=" " || listid !=null  )
            {
                    int count =await d.Campaigns.Where(o => o.campaignname == compeignname).CountAsync();
                    if (count==0)
                    {

                    ///int perseconds = Convert.ToInt32(Convert.ToInt32(delivery_delay) *Convert.ToInt32(60)*Convert.ToInt32(60)) / Convert.ToInt32(totalleads);
                Campaigns c = new Campaigns();
                c.notes = notes;
                c.PostponeDelivery = postpone_interval;
                c.label = label_name;
                c.totalleads = totalleads;
                c.validresponses = valid_responses;
                c.rate = rate;
                        c.esp = espa;
                        c.espa = espb;
                        c.espb = espc;
                        c.espc = espd;
                        c.espd = espe;
                        c.espe = espf;
                        c.destination = destination;
                c.campaignname = compeignname;
                    c.datetime = DateTime.Now.ToShortDateString();
                c.auth_key = ref_key;
                c.deliverythrottle = totalleads;
                c.status = "Active";
                    c.asignedtoday = "0";
                    c.totalremaining = "0";
                    c.totalasign = "0";          
                c.sourcename = source;
                c.list = sourcelist;
                c.leadperday = leadperday;
                        c.FirstName = FirstName;
                        c.LastName = LastName;
                        c.Labels = Labels;
                        c.Phone = Phone;
                        c.list_id =Convert.ToInt32(listid);
                d.Campaigns.Add(c);
                d.SaveChanges();
                int id = 0;
                var findsourcid =await d.source.Where(o => o.sourcecompanyname == source).ToListAsync();
                foreach (var i in findsourcid)
                {
                    id = i.Id;
                }
                var data = d.source.Find(Convert.ToInt32(id));

                data.asignto = (compeignname).ToString();
                d.Entry(data).State = EntityState.Modified;
               await d.SaveChangesAsync();
                return RedirectToAction("compeign", "Home");
            }
                    else
                    {
                        return RedirectToAction("compeign", "Home");
                    }
                }
            else
            {
                return RedirectToAction("compeign", "Home");
            }
               
            }
        }
           
        }
        [HttpGet]
        public IActionResult lead_detail(string tid)
        {
            ViewBag.lead = d.sourcelead.Where(o => o.asignto == tid).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult clone(string campaignid)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            {
                var lead = d.Campaigns.Where(o => o.Id ==Convert.ToInt32 (campaignid)).FirstOrDefault();
            Campaigns c = new Campaigns();
            c.notes = lead.notes;
            c.PostponeDelivery = lead.PostponeDelivery;
            c.label = lead.label;
            c.totalleads = lead.totalleads;
            c.validresponses = lead.validresponses;
            c.rate = lead.rate;
            c.esp = lead.esp;
            c.espa = lead.espa;
            c.espb = lead.espb;
            c.espc = lead.espc;
            c.espd = lead.espd;
            c.espe = lead.espe;
            c.destination = lead.destination;
            c.campaignname = lead.campaignname+" Draft";
            c.datetime = DateTime.Now.ToShortDateString();
            c.auth_key = lead.auth_key;
            c.deliverythrottle = lead.deliverythrottle;
            c.status = "InActive";
            c.asignedtoday = "0";
            c.totalremaining = "0";
            c.totalasign = "0";
            c.sourcename = lead.sourcename;
            
            c.leadperday = lead.leadperday;
            c.list_id = Convert.ToInt32(lead.list_id);
            d.Campaigns.Add(c);
            d.SaveChanges();
            int id = 0;
            var findsourcid = d.source.Where(o => o.sourcecompanyname == lead.sourcename).ToList();
            foreach (var i in findsourcid)
            {
                id = i.Id;
            }
            var data = d.source.Find(Convert.ToInt32(id));

            data.asignto = (lead.campaignname + " Draft").ToString();
            d.Entry(data).State = EntityState.Modified;
             d.SaveChanges();
            return RedirectToAction("compeign", "Home");
        }
            
        }
        [HttpGet]
        public IActionResult source_detail(string tid)
        {
            ViewBag.lead = d.sourcelead.Where(o => o.auth_key == tid).ToList();
            return View();
        }

        public class EmailValidationRequest
        {
            public int ListId { get; set; }
            public string Email { get; set; }
        }

        public class EmailValidationResponse
        {
            public int ListId { get; set; }
            public string Email { get; set; }
            public int ResultId { get; set; }
            public string Result { get; set; }
        }

        static string _apiURL = "https://api.emailoversight.com/api/emailvalidation";
        static string _apiToken = "f09fd596-0253-47d9-98a7-bf66dcc97f2e";

        public IActionResult clientss()
        {
             string _apiURL = "https://api.emailoversight.com/api/emailvalidation";
            string _apiToken = "f09fd596-0253-47d9-98a7-bf66dcc97f2e";
            EmailValidationResponse resps = new EmailValidationResponse();
            using (var client = new HttpClient())
            {
               
                    client.BaseAddress = new Uri(string.Format("{0}?apitoken={1}&listid={2}&email={3}", _apiURL, _apiToken, "25", "joe@testdomain.com"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(0, 0, 0, 1);
                    HttpResponseMessage response = client.GetAsync("").Result;

                    if (response.IsSuccessStatusCode)
                    {
                       /// resps = response.Content.ReadAsAsync<EmailValidationResponse>().Result;
                    }
                
             

        
            return Ok(resps);
                
            
        }
        }
        public IActionResult clients()
        {



            string json = "{\"ListId\":162513,\"Email\":\"munirahmad20786@gmail.com\"}";

            var client = new RestClient("https://api.emailoversight.com/api/emailvalidation");
                var request = new RestRequest("https://api.emailoversight.com/api/emailvalidation", Method.Post);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("apitoken", "f09fd596-0253-47d9-98a7-bf66dcc97f2e");
                //  request.AddParameter("application/json", "{\"attributes\":{\"Email\":\"{lead.email}\",\"LASTNAME\":\"usmannnkahn\",\"FIRSTNAME\":\"{lead.first_name}\",\"CITY\":\"Bahawalpur\",\"STATE\":\"Punjab\"},\"listIds\":[9],\"updateEnabled\":true,\"email\":\"{lead.email}\"}", ParameterType.RequestBody);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
            dynamic responseObj = JsonConvert.DeserializeObject(response.Content);

            // Extract the value of the "Result" property
            string result = responseObj.Result;

            Console.WriteLine(result);
            /// {\"ListId\":162513,\"Email\":\"munirahmad20786@gmail.com\",\"Result\":\"Verified\",\"ResultId\":1,\"EmailDomainGroupId\":5,\"EmailDomainGroup\":\"GOOGLE\",\"EmailDomainGroupShort\":\"GOO\",\"EOF\":-1}"
            Console.WriteLine(response.Content);











                // trevor @yahoo.com
                //trevor@aol.com
                //trevor@ymail.com
                //trevor@gmail.com
                string yahooemailToCheck = "trevor@yahoo.com";
            string yahooemailPattern = @"\b[A-Za-z0-9._%+-]+@yahoo\.com\b";
           

            string gmailinput = "a@gmail.com";
            string gmailpattern = @"\b[A-Za-z0-9._%+-]+@gmail\.com\b";

            bool gmailmatches = Regex.IsMatch(gmailinput, gmailpattern);
            bool yahooisValidEmail = Regex.IsMatch(yahooemailToCheck, yahooemailPattern);


            string ymail = "trevor@ymail.com";
            string ymailpattern = @"\b[A-Za-z0-9._%+-]+@ymail\.com\b";

            bool ymailmatch = Regex.IsMatch(ymail, ymailpattern);


            string aol = "trevor@aol.com";
            string aolpattern = @"\b[A-Za-z0-9._%+-]+@aol\.com\b";

            bool aolmatch = Regex.IsMatch(aol, aolpattern);

            
            if (aolmatch)
            {
                Console.WriteLine(aol + " is a valid email address.");

            }

            if (ymailmatch)
            {
                Console.WriteLine(ymail + " is a valid email address.");

            }
            if (gmailmatches)
            {
                Console.WriteLine(gmailinput + " is a valid email address.");

            }

                    if (yahooisValidEmail)
            {
                Console.WriteLine(yahooemailToCheck + " is a valid email address.");
            }
            else
            {
                Console.WriteLine(yahooemailToCheck + " is not a valid email address.");
            }
            return View();
        }
        public IActionResult activate_compeign()
        {
            return View();
        }
        public IActionResult apidocumentation()
        {
            return View();
        }
        public async Task<IActionResult> addproviderAsync(string tid, string notes, string contact, string email, string phone, string zip, string state, string address, string vendorname)
        {
            //{
            //    var client = new HttpClient();
            //    //email = (String.IsNullOrEmpty(email)) ? "" : email;
            //    //var lead = new sourcelead() {
            //    //    user_id = 1,
            //    //    email = null,
            //    //    first_name=null,last_name=null,
            //    //};

            //    var data = new { user_id = 1, group_id = 2, source_id = 3, first_name = "", last_name ="", email = email, list = "mysecondlist", phone = "1111", city = "{city}", dob = "03-01-2023", age = "79", zip = "{zip}", state = "{state}", url = "{https//}", optin_date = "{optin_date}", timezone_dst_flag = "timezone_dst_flag", exception = "exception", status = "verified", added_source = "added_source", lu_carrier_code = "lu_carrier_code", lu_international_format = "lu_international_format", lu_local_format = "lu_local_format", lu_carrier_name = "lu_carrier_name", lu_carrier_error_code = "lu_carrier_error_code", created_at = "{created_at}", updated_at = "{updated_at}", auth_key = "ybvvknovxjdksjpam69lfm6ljnl0ojghy3g", };

            //    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            //    var response = await client.PostAsync("https://localhost:5001/api/customer/sendlead", content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("success");
            //        //lead send successfully
            //    }
            //    else
            //    {
            //        //there was an error
            //    }
            //}


            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            {
                if (tid != null && tid != " " && tid != string.Empty)
            {
                var data = d.provider.Find(Convert.ToInt32(tid));
                data.Name = vendorname;
                data.notes = notes;
                data.phone = phone;
                data.zip = zip;
                data.state = state;
                data.address = address;
             
                d.Entry(data).State = EntityState.Modified;
                d.SaveChanges();
                return RedirectToAction("provider", "Home");
            }
            else
            { 

            //    // Replace YOUR_API_URL with the actual URL of your API
            //    string apiUrl = "https://localhost:5001/api/customer/getdata";

            //    // Add the ID of the record you want to retrieve to the API URL
            //    string url = apiUrl + "/" + "6oFeVwmmcIqilrPF80RvNN2TYA8IJram8aK";

            //    // Create a new HTTP request
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //    // Set the method to GET (default is GET)
            //    request.Method = "GET";

            //    // Send the request and retrieve the response
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //    // Get the stream associated with the response
            //    Stream receiveStream = response.GetResponseStream();

            //    // Read the stream into a string
            //    string responseString = new StreamReader(receiveStream).ReadToEnd();

            //    // Close the stream
            //    receiveStream.Close();

            //    // Close the response
            //    response.Close();

            //    // Deserialize the response string into a JSON object
            //    if (responseString != null)
            //    { 
            //        dynamic json = JsonConvert.DeserializeObject(responseString);
            //}
            //else{
            //        //Error
            //    }

            //var client = new HttpClient();

            //var data = new { first_name = "John", email = "mm@gmail.com", list = "mysecondlist", phone = "1111", dob = "03-01-2023", age = "79", auth_key = "24QG5z1YyExKSUoA9J9znUKXTkLhnYvB2qe", };

            //var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            //var response = await client.PostAsync("https://emaildeployment.net/api/customer/sendlead", content);

            //if (response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine("Success");
            //    //Lead Send SuccessFully
            //}
            //else
            //{
            //    //There was an error
            //}


            int length = 35;

            // Create a string of characters, numbers, and special characters
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chars = new char[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                // Get a random character from the validChars string
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            // Convert the char array to a string and print it
            string randomString = new string(chars);
           
        
        provider p = new provider();
            p.Name = vendorname;
            p.notes = notes;
            p.phone = phone;
            p.zip = zip;
            p.state = state;
        p.address = address;
            p.auth_key = randomString;
            d.provider.Add(p);
            d.SaveChanges();


            return RedirectToAction("provider", "Home");
        }
        }
           
        }
        public IActionResult addsource( string tid,string dupe_check, string rate, string status, string sourcecompanyname, string vendorname)
        {
            var CheckAccess = d.AdminLogin.Where(o => o.Gmail == urecby).FirstOrDefault();
            if (CheckAccess.SystemRoll == "Visitor")
            {
                return RedirectToAction("error", "Home");
            }
            else
            { 
                if (tid != null && tid != " " && tid != string.Empty)
            {
                var data = d.source.Find(Convert.ToInt32(tid));
                data.Name = vendorname;
                data.dupe_check = dupe_check;

                data.rate = rate;
                data.status = status;
                
                data.sourcecompanyname = sourcecompanyname;
              
                d.Entry(data).State = EntityState.Modified;
                d.SaveChanges();

            }
            else
            {

            


            int length = 35;

            // Create a string of characters, numbers, and special characters
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chars = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                // Get a random character from the validChars string
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            // Convert the char array to a string and print it
            string randomString = new string(chars);          
            source p = new source();
            p.Name = vendorname;
            p.dupe_check = dupe_check;
            p.datecreated = DateTime.Now.ToShortDateString();
            p.rate = rate;
            p.status = status;
            p.auth_key = randomString;
            p.sourcecompanyname = sourcecompanyname;
            d.source.Add(p);
            d.SaveChanges();
           
        }
            return RedirectToAction("source", "Home");
        }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
