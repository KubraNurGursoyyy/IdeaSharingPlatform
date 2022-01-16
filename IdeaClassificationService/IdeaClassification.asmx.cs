using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IdeaClassificationService
{
    /// <summary>
    /// Summary description for IdeaClassification
    /// </summary>
    

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IdeaClassification : System.Web.Services.WebService
    {
        private readonly object _lockObject = new object();

        [WebMethod]
        public string GetCategory(string blurb)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "D:/PythonFiles/venv/Scripts/python.exe";
            startInfo.Arguments = String.Format("D:/PythonFiles/IdeaClassification.py {0}", blurb);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true; 
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true; 
            startInfo.LoadUserProfile = true;
            lock (_lockObject)
            {
                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); 
                        string result = reader.ReadToEnd();
                        result = result.Replace("\r\n", string.Empty);
                        return result;
                    }
                }

            }
        }
    }
}

