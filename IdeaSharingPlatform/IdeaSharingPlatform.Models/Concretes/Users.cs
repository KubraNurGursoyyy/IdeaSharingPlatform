using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace IdeaSharingPlatform.Models.Concretes
{
    public class Users : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Users()
        {
            UsersProjects = new List<Projects>();
            UsersLikedProjects = new List<UserLikes>();
            UsersSavedProjects = new List<UserSaves>();
            UsersComments = new List<Comments>();
        }

        public int UserID { get; set; }
       
        [Required(ErrorMessage = "You must enter your name")]
        public string UserFirstName { get; set; }

        [Required(ErrorMessage = "You must enter last name")]
        public string UserLastName { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage = "You must enter your birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime UserBirthDate { get; set; }

        [Required(ErrorMessage = "You must enter your email")]
        public string UserEmail { get; set; }


        [Required(ErrorMessage = "You must enter your username")]
        public string UserUsername { get; set; }

        [Required(ErrorMessage = "You must enter your password")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "You must enter at least a few words about you")]

        [StringLength(1000, MinimumLength = 3)]
        public string UserAbout { get; set; }

        [Required(ErrorMessage = "You must enter your location")]
        public string UserLocation { get; set; }

        public DateTime UserJoinDate { get; set; }

        public virtual List<Projects> UsersProjects { get; set; }
        public virtual List<UserLikes> UsersLikedProjects { get; set; }
        public virtual List<UserSaves> UsersSavedProjects { get; set; }
        public virtual List<Comments> UsersComments { get; set; }





    }
}
