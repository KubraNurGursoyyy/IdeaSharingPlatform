using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaSharingPlatform.BusinessLogic.Abstracts
{
    public interface ILogInBusinessLogic
    {
        bool LogIn(string Email, string Password);
    }
}
