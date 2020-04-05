
namespace Notes.Models
{
    /// <summary>
    /// Class with users's public data only
    /// </summary>
    public class PublicUser
    {
        /// <summary>
        /// Email max length
        /// </summary>
        public const int EmailLength = 255;

        public int Id { get; set; }
        public string Email { get; set; }
    }
}
