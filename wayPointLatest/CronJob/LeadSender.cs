using Quartz;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Security.Permissions;
using System.Threading.Tasks;
using System;
using TaskSchedular.Models;
using System.Linq;
using TaskSchedular.Helpers;
using System.Collections.Generic;

namespace TaskSchedular.CronJob
{
    public class LeadSender : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("----------------------------------------------------------------------------------------------------------");
          
            var db = new AppDBContext();
            var compaignId = Convert.ToDouble(context.MergedJobDataMap.Get("CompaignId"));

            var CompaignData = db.Campaigns.Where(a => a.Id == compaignId ).FirstOrDefault();
                if (CompaignData != null)
                {

                    var source =await db.source.Where(o => o.sourcecompanyname == CompaignData.sourcename).FirstOrDefaultAsync();

          
            repeat:
               /// var lead = await   db.sourcelead.OrderByDescending(o => o.Id).Where(o => (o.auth_key == source.auth_key && o.asignto == null && o.archieve == null )).FirstOrDefaultAsync();
              var lead = await   db.sourcelead.OrderByDescending(o => o.Id).Where(o => (o.auth_key == source.auth_key && o.asignto == null && o.archieve == null ) && (o.esp == CompaignData.esp || o.esp == CompaignData.espa || o.esp == CompaignData.espb || o.esp == CompaignData.espc || o.esp == CompaignData.espd || o.esp == CompaignData.espe || o.esp=="esp")).FirstOrDefaultAsync();
                
                if (lead != null) 
                {
           
                    string Labels = "";
                    string Phone = "";
                    string LastName = "";
                    string FirstName = "";
                    lead.asignto = CompaignData.campaignname;
                    db.sourcelead.Update(lead);
                    db.SaveChanges();
                
                if(LeadHandler.LeadExist(lead.Id))
                {
                    goto repeat;
                }
                else
                {
                    LeadHandler.SetLeadId(lead.Id);

                      
                        if (CompaignData.FirstName != string.Empty && CompaignData.FirstName != null)
                        {
                            FirstName = lead.first_name;

                        }
                        if (CompaignData.LastName != string.Empty && CompaignData.LastName != null)
                        {
                            LastName = lead.last_name;

                        }
                        if (CompaignData.Labels != string.Empty && CompaignData.Labels != null)
                        {
                            Labels = CompaignData.label;
                        }
                        if (CompaignData.Phone != string.Empty && CompaignData.Phone != null)
                        {
                            Phone = lead.phone;

                        }
                    }

                var emailJson = JsonConvert.SerializeObject(new
                    {
                        ListId = "162513",
                        Email = lead.email
                    });

                string strMessage = $"\n\n({context.JobDetail.Key.Name}) - LEAD:{lead.email} - Compaign:({CompaignData.campaignname}) - AssignedToday: {CompaignData.asignedtoday} - SRC:";

                var client = new RestClient("https://api.emailoversight.com/api/emailvalidation");
                    var request = new RestRequest("https://api.emailoversight.com/api/emailvalidation", Method.Post);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("apitoken", Config.Get("OverSightEmailApiKey"));
                    request.AddParameter("application/json", emailJson, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);
                string resultss = "";

                if(response.Content !=null)
                {
                    dynamic responseObj = JsonConvert.DeserializeObject(response.Content);
                    resultss = responseObj.Result;
                }

                    

                    if (resultss == "Verified")
                    {

                        ///Console.WriteLine(FirstName + "" + LastName + "" + Phone);
                       
                       
                        if (CompaignData.destination.Substring(0, 25) == "https://app.campaignrefin")
                    {
                        var clientt = new HttpClient();

                        var postData = new List<KeyValuePair<string, string>>();
                        postData.Add(new KeyValuePair<string, string>("key",CompaignData.auth_key));
                        postData.Add(new KeyValuePair<string, string>("first_name", FirstName));
                        postData.Add(new KeyValuePair<string, string>("last_name", LastName));
                        postData.Add(new KeyValuePair<string, string>("email", lead.email));
                       /// postData.Add(new KeyValuePair<string, string>("DataSource", CompaignData.label));
                        postData.Add(new KeyValuePair<string, string>("tags", Labels));

                        var content = new FormUrlEncodedContent(postData);

                       /// var RefineryRequest = new HttpRequestMessage(HttpMethod.Post, "https://app.campaignrefinery.com/rest/contacts/create-contact")
                        var RefineryRequest = new HttpRequestMessage(HttpMethod.Post, "https://app.campaignrefinery.com/rest/contacts/subscribe")
                        {
                            Content = content
                        };

                        var RefineryRespons = await clientt.SendAsync(RefineryRequest);
                        var RefineryResponsBody = await RefineryRespons.Content.ReadAsStringAsync();

                        if (RefineryRespons.IsSuccessStatusCode)
                        {
                            var dataa = await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                            dataa.asignto = CompaignData.campaignname;
                            db.sourcelead.Update(dataa);
                            await db.SaveChangesAsync();
                            string datetime = DateTime.Now.ToShortDateString();

                            var compaign = await db.Campaigns.Where(xm => xm.datetime == datetime && xm.Id == Convert.ToDouble(CompaignData.Id)).FirstOrDefaultAsync();
                            if (compaign != null)
                            {
                                compaign.asignedtoday = (Convert.ToDouble(compaign.asignedtoday) + 1).ToString();
                                compaign.totalasign = (Convert.ToDouble(compaign.totalasign) + 1).ToString();
                                db.Campaigns.Update(compaign);
                                await db.SaveChangesAsync();
                                Console.Out.WriteLine(strMessage + "sendinblue(1stblock)");
                            }
                            else
                            {
                                var comp = await db.Campaigns.Where(x => x.Id == Convert.ToDouble(CompaignData.Id)).FirstOrDefaultAsync();
                                comp.datetime = DateTime.Now.ToShortDateString();
                                comp.asignedtoday = (1).ToString();
                                comp.totalasign = (Convert.ToDouble(comp.totalasign) + 1).ToString();
                                db.Update(comp);
                                await db.SaveChangesAsync();
                                Console.Out.WriteLine(strMessage + "sendinblue(2ndblock)");
                            }

                        }
                        else
                        {

                            var srcLoad = await db.sourcelead.FindAsync(Convert.ToUInt32(lead.Id));
                            srcLoad.archieve = "false";
                            db.sourcelead.Update(srcLoad);
                            await db.SaveChangesAsync();
                            Console.Out.WriteLine("Lead Archived");

                        }
                    }
                    else if (CompaignData.destination.Substring(0, 25) == "https://api.sendinblue.co" || CompaignData.destination.Substring(0, 25) == "https://my.sendinblue.co")
                    {
                        int[] apiListIds = { Convert.ToInt32(CompaignData.list_id) };
                        List<int> listIds = apiListIds.ToList();
                      
                           
                            
                               var json = JsonConvert.SerializeObject(new
                                {

                                    attributes = new
                                    {

                                        Email = lead.email,
                                        LASTNAME = LastName,
                                        FIRSTNAME = FirstName,
                                        CITY = " ",
                                        STATE = " ",
                                        PHONE = Phone,
                                        SUBID1 = Labels
                                    },

                                    listIds = listIds.ToArray(),
                                    updateEnabled = true,
                                    email = lead.email
                                });
                            
                            
                            Console.WriteLine(json);    
                            var clientss = new RestClient(CompaignData.destination);
                        var requestss = new RestRequest(CompaignData.destination, Method.Post);
                        requestss.AddHeader("accept", "application/json");
                        requestss.AddHeader("content-type", "application/json");
                        requestss.AddHeader("api-key", CompaignData.auth_key);
                        requestss.AddParameter("application/json", json, ParameterType.RequestBody);
                        RestResponse responsess = await clientss.ExecuteAsync(requestss);
                        if (responsess.IsSuccessStatusCode)
                        {
                            var dataa = await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                            dataa.asignto = CompaignData.campaignname;
                            db.sourcelead.Update(dataa);
                            await db.SaveChangesAsync();
                            string datetime = DateTime.Now.ToShortDateString();

                            var compaign = await db.Campaigns.Where(xm => xm.datetime == datetime && xm.Id == Convert.ToDouble(CompaignData.Id)).FirstOrDefaultAsync();
                            if (compaign != null)
                            {
                                compaign.asignedtoday = (Convert.ToDouble(compaign.asignedtoday) + 1).ToString();
                                compaign.totalasign = (Convert.ToDouble(compaign.totalasign) + 1).ToString();
                                db.Campaigns.Update(compaign);
                                await db.SaveChangesAsync();
                                Console.Out.WriteLine(strMessage + "sendinblue(1stblock)");
                            }
                            else
                            {
                                var comp = await db.Campaigns.Where(x => x.Id == Convert.ToDouble(CompaignData.Id)).FirstOrDefaultAsync();
                                comp.datetime = DateTime.Now.ToShortDateString();
                                comp.asignedtoday = (1).ToString();
                                comp.totalasign = (Convert.ToDouble(comp.totalasign) + 1).ToString();
                                db.Update(comp);
                                await db.SaveChangesAsync();
                                Console.Out.WriteLine(strMessage + "sendinblue(2ndblock)");
                            }

                        }
                        else
                        {

                            var srcLoad = await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                            srcLoad.archieve = "false";
                            db.sourcelead.Update(srcLoad);
                            await db.SaveChangesAsync();
                            Console.Out.WriteLine("Lead Archived");

                        }


                    }


                    }
                    else
                    {

                        var srcLoad =await db.sourcelead.FindAsync(Convert.ToInt32(lead.Id));
                        Console.WriteLine("\n Unverified Email \n");
                        srcLoad.archieve = "Unverified";
                        db.sourcelead.Update(srcLoad);
                       await db.SaveChangesAsync();
                    Console.Out.WriteLine(strMessage + "SKIPPING(UNVERIFIED)");

                    }

                  

            }
        }
        }




     
    }
}