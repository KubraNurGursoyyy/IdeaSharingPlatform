using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.Models.Concretes
{
    public class Categories:IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Categories()
        {
            CategoriesProjects = new List<Projects>();

        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual List<Projects> CategoriesProjects { get; set; }

    }
}
