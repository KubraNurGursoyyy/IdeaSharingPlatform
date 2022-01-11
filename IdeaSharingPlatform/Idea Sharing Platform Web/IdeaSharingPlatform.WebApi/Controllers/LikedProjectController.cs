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
    public class LikedProjectController : ApiController
    {
        //proje beğen, beğeniden kaldır

        // POST api/User
        //AddUSer
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LikedProject/LikeProject")]
        public IHttpActionResult LikeProject(UserLikes like)
        {
            try
            {
                using (var likeBusiness = new UserLikesBusiness())
                {
                    if (likeBusiness.Insert(like))
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
        [System.Web.Http.Route("api/LikedProject/RemoveLikeProject/{id}")]
        public IHttpActionResult RemoveLikeProject(int id)
        {
            try
            {
                using (var likeBusiness = new UserLikesBusiness())
                {
                    if (likeBusiness.DeleteByID(id))
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