using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Notes.Models;

namespace Notes.Controllers
{
    [Route("api/")]
    [ApiController]
    public class NotesApiController : ControllerBase
    {
        /// <summary>
        /// Notes DB context
        /// </summary>
        private readonly NotesContext _context;
        /// <summary>
        /// App settings with secret key for authentication
        /// </summary>
        private readonly AppSettings _appSettings;

        public NotesApiController(NotesContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// For authorize check, return Ok if authorize
        /// </summary>
        [HttpGet("authorize")]
        [Authorize]
        public IActionResult AuthorizeCheck()
        {
            return Ok();
        }

        /// <summary>
        /// Get safe user data without password
        /// </summary>
        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<PublicUser>> GetPublicUser()
        {
            var user = await _context.GetUserById(int.Parse(User.Identity.Name));
            return new PublicUser
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        /// <summary>
        /// Get current user notes
        /// </summary>
        [HttpGet("notes")]
        [Authorize]
        public async Task<ActionResult<List<PublicNote>>> GetNotes([FromQuery] string search = null)
        {
            var notes = new List<Note>();
            if (search == null || search == "")
            {
                notes = await _context.GetNotes(int.Parse(User.Identity.Name));
            }
            else
            {
                notes = await _context.GetNotes(int.Parse(User.Identity.Name), search);
            }

            return notes.ConvertAll(n => new PublicNote
            {
                Id = n.Id,
                Title = n.Title,
                Text = n.Text
            });

        }

        /// <summary>
        /// Log in and authorize with jwt
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel data)
        {
            var user = await data.CheckLogin(_context);

            if (user != null)
            {
                var token = new TokenModel(user, _appSettings.Secret);
                return Ok(token);
            }

            return BadRequest("Email or Password are incorrect");
        }

        /// <summary>
        /// Registrate and authorize with jwt
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel data)
        {
            var err = new RegisterModel();

            if (await data.CheckRegData(err, _context))
            {
                var user = new User()
                {
                    Email = data.Email
                };

                user.CreateCryptPassword(data.Password);
                if (await _context.AddUser(user))
                {
                    var token = new TokenModel(user, _appSettings.Secret);
                    return Ok(token);
                }
                else
                    return StatusCode(500);
            }

            return BadRequest(err);
        }

        /// <summary>
        /// Add user's note
        /// </summary>
        [HttpPost("note/add")]
        [Authorize]
        public async Task<IActionResult> AddNote(PublicNote note)
        {
            var n = new Note
            {
                Title = note.Title,
                Text = note.Text,
                Userid = int.Parse(User.Identity.Name)
            };

            var err = new PublicNote();
            if (n.CheckNoteData(err))
            {
                if (await _context.AddNote(n))
                    return Ok();
                else
                    return StatusCode(500);
            }
            else
                return BadRequest(err);
        }

        /// <summary>
        /// Delete user's note by id
        /// </summary>
        [HttpPost("note/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteNote(PublicNote n)
        {
            var note = await _context.GetNote(n.Id);

            if (note != null)
            {
                if (note.Userid == int.Parse(User.Identity.Name))
                {
                    if (await _context.DeleteNote(n.Id))
                        return Ok();
                    else
                        return
                            StatusCode(500);
                }
            }

            return Forbid();
        }
    }
}
