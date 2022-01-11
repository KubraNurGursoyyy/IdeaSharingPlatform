using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.Models.Concretes
{
    public class UserSaves : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public int UserSavesID { get; set; }
        public int SavedProjectID { get; set; }
        public int SavedProjectsUsersID { get; set; }
        public virtual Users SavedUser { get; set; }
        public virtual Projects SavedProject { get; set; }

    }
}
