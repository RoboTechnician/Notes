
namespace Notes.Models
{
    /// <summary>
    /// Full note class with Check methods, inherit class with note's public data only
    /// </summary>
    public class Note : PublicNote
    {
        /// <summary>
        /// User id with this note
        /// </summary>
        public int? Userid { get; set; }

        /// <summary>
        /// Validate note's title, write errors in err and return false if validation failed
        /// </summary>
        public bool CheckTitle(PublicNote err)
        {
            if (Title.Length > TitleLength)
            {
                err.Title = "Title is too long, maximum " + TitleLength.ToString();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate note's text, write errors in err and return false if validation failed
        /// </summary>
        public bool CheckText(PublicNote err)
        {
            if (Text == "")
            {
                err.Text = "Text is required";
                return false;
            }
            else if (Text.Length > TextLength)
            {
                err.Text = "Text is too long, maximum " + TextLength.ToString();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate note's title and text, write errors in err and return false if validation failed
        /// </summary>
        public bool CheckNoteData(PublicNote err)
        {
            return CheckTitle(err) && CheckText(err);
        }
    }
}
