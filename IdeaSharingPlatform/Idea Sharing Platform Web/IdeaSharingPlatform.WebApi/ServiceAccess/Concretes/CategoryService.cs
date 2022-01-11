using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.WebApi.IdeaClassificationServiceReference;
using IdeaSharingPlatform.WebApi.ServiceAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaSharingPlatform.WebApi.ServiceAccess.Concretes
{
    public class CategoryService:ICategoryService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
 
        public string GetCategory(string blurb)
        {
            string categoryName = " ";
            try
            {
                using (var IdeaClassificationService = new IdeaClassificationSoapClient())
                {
                    categoryName = IdeaClassificationService.GetCategory(blurb);
                }       
            }
            catch (Exception ex)           
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
            }
            return categoryName;
        }
    }
}