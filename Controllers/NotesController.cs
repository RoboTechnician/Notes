using Microsoft.AspNetCore.Mvc;

namespace Notes.Controllers
{
    [Route("/")]
    public class NotesController : Controller
    {
        /// <summary>
        /// Return main page index for all available url's
        /// </summary>
        [HttpGet("/")]
        [HttpGet("login")]
        [HttpGet("register")]
        public IActionResult Index()
        {
            return View("index");
        }
    }
}