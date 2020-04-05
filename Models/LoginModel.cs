using System.Threading.Tasks;

namespace Notes.Models
{
    /// <summary>
    /// Class with Log in data
    /// </summary>
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Try load user by email and validate password, returns user if succed and null if not
        /// </summary>
        public async Task<User> CheckLogin(NotesContext context)
        {
            var user = await context.GetUserByEmail(Email);

            return user != null && user.ValidatePassword(Password) ? user : null;
        }
    }
}