using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.Models.Concretes
{
    public class Projects: IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Projects()
        {
            ProjectsLikedUsers = new List<UserLikes>();
            ProjectsSavedUsers = new List<UserSaves>();
            ProjectsComments = new List<Comments>();
        }

        public int ProjectID { get; set; }

        [Required(ErrorMessage = "You must give your project a name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "You must enter your projects blurb for the short introduction")]
        public string ProjectBlurb { get; set; }
        public DateTime ProjectCreationDate { get; set; }
        public DateTime UpdateTime { get; set; }

        [Required(ErrorMessage = "You must enter at least few word for describing your project")]
        public string Description { get; set; }
        public int ProjectsCategoryId { get; set; }
        public int ProjectOwnerID { get; set; }
        public virtual Categories ProjectsCategory { get; set; }
        public virtual Users ProjectOwner { get; set; }
        public virtual List<UserLikes> ProjectsLikedUsers { get; set; }
        public virtual List<UserSaves> ProjectsSavedUsers { get; set; }
        public virtual List<Comments> ProjectsComments { get; set; }
    }
}
