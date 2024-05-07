using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using prepare4Eplan.Resourses;

namespace prepare4Eplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class LocalLizationController : ControllerBase
    {
        public readonly IStringLocalizer<SharedResources> stringLocalizer;
        public  LocalLizationController(IStringLocalizer<SharedResources> _stringLocalizer)
        {
            stringLocalizer=_stringLocalizer;
        }
        [HttpGet]
        public ActionResult Index()
        {
            int x = 30;
            if(x>50)
            {
                return Ok();
            }

            else
            {
                string dd= stringLocalizer[SharedResourcesKeys.checkdata];
                return BadRequest(dd);
            }

        }
    }
  
}
