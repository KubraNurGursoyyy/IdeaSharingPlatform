using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.Models.Concretes
{
    public class UserLikes: IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int UserLikesID { get; set; }
        public int LikedProjectID { get; set; }
        public int LikedProjectsUsersID { get; set; }
        public virtual Users LikedUsers { get; set; }
        public virtual Projects LikedProjects { get; set; }


    }
}
