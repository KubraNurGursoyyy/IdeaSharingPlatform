using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.WebApi.ServiceAccess.Abstracts
{
    public interface ICategoryService:IDisposable
    {
        string GetCategory(string blurb);
    }
}
