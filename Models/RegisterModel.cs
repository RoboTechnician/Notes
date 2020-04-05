using System.Net.Mail;
using System.Threading.Tasks;

namespace Notes.Models
{
    /// <summary>
    /// Class with Register data
    /// </summary>
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Validate email with mail standarts
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate user's email, write errors in err and return false if validation failed
        /// </summary>
        public async Task<bool> CheckRegEmail(RegisterModel err, NotesContext context)
        {
            if (Email == "")
            {
                err.Email = "Email is required";
                return false;
            }
            else if (Email.Length > User.EmailLength)
            {
                err.Email = "Email is too long";
                return false;
            }
            else if (!IsValidEmail(Email))
            {
                err.Email = "Email is invalid";
                return false;
            }
            else if ((await context.GetUserByEmail(Email)) != null)
            {
                err.Email = "Email is already in use";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate user's password, write errors in err and return false if validation failed
        /// </summary>
        public bool CheckRegPassword(RegisterModel err)
        {
            if (Password == "")
            {
                err.Password = "Password is required";
                return false;
            }
            else if (Password.Length >= User.PasswordLength)
            {
                err.Password = "Password is too long";
                return false;
            }
            else if (Password.Length < 6)
            {
                err.Password = "Password is too short";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate user's email and password, write errors in err and return false if validation failed
        /// </summary>
        public async Task<bool> CheckRegData(RegisterModel err, NotesContext context)
        {
            var checkEmail = await CheckRegEmail(err, context);
            var checkPassword = CheckRegPassword(err);

            return checkEmail && checkPassword;
        }
    }
}
