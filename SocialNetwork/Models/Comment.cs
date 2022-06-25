using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Comment Text")]
        public string Text { get; set; }

        public virtual Post Post { get; set; }
    }
}
