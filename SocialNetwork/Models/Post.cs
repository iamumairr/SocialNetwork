using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    [Serializable]
    public class Post
    {
        [Display(Name = "ID")]
        public int PostId { get; set; }

        [StringLength(20), Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Likes")]
        public int likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            TimeStamp = DateTime.Now;
            likes = 0;
        }

        public void Like()
        {
            likes++;
        }

        public void Unlike()
        {
            if (likes > 0)
            {
                likes--;
            }
        }
        public string FormatElapsedTime()
        {
            DateTime current = DateTime.Now;
            TimeSpan timePast = current - TimeStamp;

            long seconds = (long)timePast.TotalSeconds;
            long minutes = seconds / 60;
            long hours = minutes / 60;

            if (hours > 0)
            {
                return hours + " hours ago";
            }
            else
            {
                if (minutes > 0)
                {
                    return minutes + " minutes ago";
                }
                else
                {
                    return seconds + " seconds ago";
                }
            }
        }
    }
}
