using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostTask([FromBody] Userinfo user)
        {
            APIresult apiResult = new APIresult();

            apiResult = Utils.Validate.CheckUserInfo(user);

            ObjectResult result = new ObjectResult(apiResult);
            if (apiResult.hasErr == false)
            {
                result.ContentTypes.Add("application/json");


                Utils.RabbitMQ.publish(apiResult);
                result.StatusCode = StatusCodes.Status200OK;

            }
            else if (apiResult.hasErr == true)
            {

                result.ContentTypes.Add("application/json");
                result.StatusCode = StatusCodes.Status401Unauthorized;


            }

            return result;
        }
    }
}
