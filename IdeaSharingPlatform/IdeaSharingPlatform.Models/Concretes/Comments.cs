using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.Models.Concretes
{
    public class Comments: IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int CommentID { get; set; }

        [Required(ErrorMessage = "You must write something")]
        public string CommentDescription { get; set; }
        public DateTime CommentDate { get; set; }
        public int CommentedProjectID { get; set; }
        public int CommentedUsersID { get; set; }
        public virtual Users CommentedUser { get; set; }
        public virtual Projects CommentedProject { get; set; }

    }
}
