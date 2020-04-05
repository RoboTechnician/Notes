
namespace Notes.Models
{
    /// <summary>
    /// Class with note's public data only
    /// </summary>
    public class PublicNote
    {
        /// <summary>
        /// Max text length
        /// </summary>
        public const int TextLength = 255;
        /// <summary>
        /// Max title length
        /// </summary>
        public const int TitleLength = 64;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
