using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class MessagePost : Post
    {
        [StringLength(256),DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
