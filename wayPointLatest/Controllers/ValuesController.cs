using Microsoft.AspNetCore.Mvc;
using TaskSchedular.DataConfig;
using TaskSchedular.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace waypoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        AppDBContext d = new AppDBContext();
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
            string dsd= "";
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost("ProviderCreate")]
        public async Task<IActionResult> ProviderCreate(provider obj1)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<provider>();
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest();
                //}
                bool checkname = d.provider.ToList().Exists(p => p.Name.Equals(obj1.Name, StringComparison.CurrentCultureIgnoreCase));
                if (checkname)
                {
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.Already_Exists, null, null);
                    return Ok(Response);
                }
                d.provider.Add(obj1);
                await d.SaveChangesAsync();
                return Ok(Response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<provider>();
                    ResponseBuilder.SetWSResponse(Response, TaskSchedular.DataConfig.StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}

