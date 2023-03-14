using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSchedular.DataConfig;
using TaskSchedular.Models;

namespace waypoint.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class customerController : Controller
    {
        AppDBContext d = new AppDBContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("getdata/{auth_key}")]
        public IActionResult getdata(string auth_key)
        {
            try
            {
                var record = d.sourcelead.Where(o => o.auth_key == auth_key).ToList();
                var Response = ResponseBuilder.BuildWSResponse<List<sourcelead>>();
               
                if (record != null)
                {
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.SUCCESS_CODE, null, record);
                 
                }
                else
                {
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.RECORD_NOTFOUND, null, null);
                }
                return Ok(Response);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<sourcelead>();
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("sendlead")]
        public async Task<IActionResult> sendlead(sourcelead obj1)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<sourcelead>();
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                int count = d.source.Where(o => o.auth_key == obj1.auth_key).Count();

                if (count >= 1)
                {
                    ///obj1.email = (String.IsNullOrEmpty(obj1.email)) ? "" : obj1.email;
                    //string input = obj1.email;
                    //string pattern = @"\b[A-Za-z0-9._%+-]+@gmail\.com\b";

                    //MatchCollection matches = Regex.Matches(input, pattern);
                    //if (matches.Count!=0)
                    //{


                    //foreach (Match match in matches)
                    //{
                    //    ResponseBuilder.SetWSResponse(Response, ABC.Shared.DataConfig.StatusCodes.Already_Exists, null, null);
                    //    return Ok(Response);
                    string yahooemailToCheck = obj1.email;
                    string yahooemailPattern = @"\b[A-Za-z0-9._%+-]+@yahoo\.com\b";

                    string outlookemailToCheck = obj1.email;
                    string outlookmailPattern = @"\b[A-Za-z0-9._%+-]+@outlook\.com\b";

                    string gmailinput = obj1.email;
                    string gmailpattern = @"\b[A-Za-z0-9._%+-]+@gmail\.com\b";

                    bool gmailmatches = Regex.IsMatch(gmailinput, gmailpattern);
                    bool yahooisValidEmail = Regex.IsMatch(yahooemailToCheck, yahooemailPattern);
                    bool outlookisValidEmail = Regex.IsMatch(outlookemailToCheck, outlookmailPattern);


                    string ymail = obj1.email;
                    string ymailpattern = @"\b[A-Za-z0-9._%+-]+@ymail\.com\b";

                    bool ymailmatch = Regex.IsMatch(ymail, ymailpattern);


                    string aol = obj1.email;
                    string aolpattern = @"\b[A-Za-z0-9._%+-]+@aol\.com\b";

                    bool aolmatch = Regex.IsMatch(aol, aolpattern);
                  

                    if (aolmatch)
                    {
                        obj1.esp = "aol";
                    }

                    if (ymailmatch)
                    {
                        obj1.esp = "ymail";

                    }
                    if (gmailmatches)
                    {
                        obj1.esp = "gmail";

                    }

                    if (yahooisValidEmail)
                    {
                        obj1.esp = "yahoo";
                    }
                    if (outlookisValidEmail)
                    {
                        obj1.esp = "outlook";
                    }
                    if (yahooisValidEmail!=true && outlookisValidEmail!=true && gmailmatches!=true && aolmatch!=true && ymailmatch!=true)
                    {
                        obj1.esp = "esp";
                    }
                    
                    obj1.datecreated = DateTime.Now.ToShortDateString();
                    d.sourcelead.Add(obj1);
                    await d.SaveChangesAsync();
                    string datenow = DateTime.Now.ToShortDateString();

                    int dupcount = d.sourcelead.Where(o => o.email == obj1.email && o.auth_key == obj1.auth_key).Count();
                    int dupcounttoday = d.sourcelead.Where(o => o.email == obj1.email && o.datecreated == datenow && o.auth_key == obj1.auth_key).Count();
                    int totallead = d.sourcelead.Where(o => o.auth_key == obj1.auth_key).Count();
                    int id = 0;
                    var sourcid = d.source.Where(o => o.auth_key == obj1.auth_key).ToList();
                    foreach (var i in sourcid)
                    {
                        id = i.Id;
                    }
                    var data = d.source.Find(Convert.ToInt32(id));

                    data.dupcount = (dupcount).ToString();
                    data.dupcounttoday = (dupcounttoday).ToString();
                    data.totalleads = (totallead).ToString();
                    d.Entry(data).State = EntityState.Modified;

                    d.SaveChanges();
                    int coujnttt = d.sourcelist.Where(o => o.list == obj1.list).Count();
                    if (coujnttt == 0)
                    {
                        sourcelist sl = new sourcelist();

                        sl.list = obj1.list;
                        sl.sourcename = obj1.auth_key;
                        d.sourcelist.Add(sl);
                        d.SaveChanges();

                    }

                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.SUCCESS_CODE, null, null);

                    return Ok(Response);

                }
               

                else
                {
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.ERROR_INVALID_AUTH, null, null);

                    return BadRequest("Auth Key is Invalid");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Something Went Wrong. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<sourcelead>();
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }
    

    [HttpPost("sendleadforcosmo")]
    public async Task<IActionResult> sendleadforcosmo(sendoncosmolead obj1)
    {
        try
        {
            var Response = ResponseBuilder.BuildWSResponse<sendoncosmolead>();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           

            if ( obj1.email != " " && obj1.email != null)
            {


                //    ResponseBuilder.SetWSResponse(Response, ABC.Shared.DataConfig.StatusCodes.Already_Exists, null, null);
                //    return Ok(Response);
                obj1.datecreated = DateTime.Now.ToShortDateString();
                d.sendoncosmolead.Add(obj1);
                await d.SaveChangesAsync();
                string datenow = DateTime.Now.ToShortDateString();

                ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.SUCCESS_CODE, null, null);
                Console.WriteLine(Response.Status);
                return Ok(Response);

            }
            else
            {
                ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.ERROR_INVALID_AUTH, null, null);

                return BadRequest("Auth Key is Invalid");

            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
            {
                var Response = ResponseBuilder.BuildWSResponse<sourcelead>();
                ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.FIELD_REQUIRED, null, null);
                return Ok(Response);
            }
            return BadRequest(ex.Message);
        }
    }




    [HttpPost("sendleadforccg")]
    public async Task<IActionResult> sendleadforccg(forccgcosmolead obj1)
    {
        try
        {
            var Response = ResponseBuilder.BuildWSResponse<forccgcosmolead>();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            if (obj1.email != " " && obj1.email != null)
            {


                //    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.Already_Exists, null, null);
                //    return Ok(Response);
                obj1.datecreated = DateTime.Now.ToShortDateString();
                d.forccgcosmolead.Add(obj1);
                await d.SaveChangesAsync();
                string datenow = DateTime.Now.ToShortDateString();

                ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.SUCCESS_CODE, null, null);
                Console.WriteLine(Response.Status);
                return Ok(Response);

            }
            else
            {
                ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.ERROR_INVALID_AUTH, null, null);

                return BadRequest("Auth Key is Invalid");

            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
            {
                var Response = ResponseBuilder.BuildWSResponse<sourcelead>();
                ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.FIELD_REQUIRED, null, null);
                return Ok(Response);
            }
            return BadRequest(ex.Message);
        }
    }
}
}

