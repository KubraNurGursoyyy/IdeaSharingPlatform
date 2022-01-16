using IdeaSharingPlatform.BusinessLogic.Concretes;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IdeaSharingPlatform.WebApi.Controllers
{
    public class SavedProjectController : ApiController
    {
        // POST api/User
        //AddUSer
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SavedProject/SaveProject")]
        public IHttpActionResult SaveProject(UserSaves save)
        {
            try
            {
                using (var saveBusiness = new UserSavesBusiness())
                {
                    if (saveBusiness.Insert(save))
                        return Ok();
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        // DELETE api/User/5
        //DeleteUSerByID
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/SavedProject/RemoveSaveProject/{id}")]
        public IHttpActionResult RemoveSaveProject(int id)
        {
            try
            {
                using (var saveBusiness = new UserSavesBusiness())
                {
                    if (saveBusiness.DeleteByID(id))
                        return Ok();
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }
    }
}