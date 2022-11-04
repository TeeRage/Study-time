using Microsoft.AspNetCore.Mvc; //Controllerbase

namespace API.Controllers
{
    //Base controller for all other controllers. Less repetition inside controllers this way.
    [ApiController]
    [Route("api/[controller]")]    
    public class BaseAPIController : ControllerBase 
    {
        
    }
}