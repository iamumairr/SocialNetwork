using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class PhotoPost : Post
    {
        [StringLength(128), Required]
        [Display(Name ="Filename")]
        public string FileName { get; set; }

        [StringLength(30), Required]
        public string Caption { get; set; }
    }
}
