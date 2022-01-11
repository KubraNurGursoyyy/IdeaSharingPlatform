using IdeaSharingPlatform.BusinessLogic.Concretes;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace IdeaSharingPlatform.WebApi.Controllers
{
    public class CommentsController : ApiController
    {
        //yorum yap, yorum sil, yorum güncelle
        // GET: Comments

        // GET api/User/5
        //GetProjectsOwner
        [ResponseType(typeof(Users))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Comments/GetCommentOwner/{id}")]
        public IHttpActionResult GetCommentOwner(int id)
        {
            try
            {
                using (var commentBusiness = new CommentsBusiness())
                {
                    Users castedUser = null;
                    Comments responsedcomment = commentBusiness.GetByID(id);
                    if (responsedcomment != null)
                    {
                        UsersBusiness usersBusiness = new UsersBusiness();
                        Users projectOwner = usersBusiness.GetByID(responsedcomment.CommentedUsersID);

                        castedUser = new Users()
                        {
                            UserAbout = projectOwner.UserAbout,
                            UserBirthDate = projectOwner.UserBirthDate,
                            UserEmail = projectOwner.UserEmail,
                            UserFirstName = projectOwner.UserFirstName,
                            UserJoinDate = projectOwner.UserJoinDate,
                            UserLastName = projectOwner.UserLastName,
                            UserLocation = projectOwner.UserLocation,
                            UserUsername = projectOwner.UserUsername,
                        };
                    }
                    return Ok(castedUser);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        // POST api/User
        //AddUSer
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Comments/CreateComment")]
        public IHttpActionResult CreateComment(Comments comment)
        {
            try
            {
                using (var commentBusiness = new CommentsBusiness())
                {
                    if (commentBusiness.Insert(comment))
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
        [System.Web.Http.Route("api/Comments/DeleteComment/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            try
            {
                using (var commentsBusiness = new CommentsBusiness())
                {
                    if (commentsBusiness.DeleteByID(id))
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

        // PUT api/User/5
        //UpdateUser
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Comments/UpdateComment/{id}")]
        public IHttpActionResult UpdateComment(int id, Comments comment)
        {
            try
            {
                if (id != comment.CommentID)
                {
                    return BadRequest();
                }
                using (var commentsBusiness = new CommentsBusiness())
                {
                    Comments tmp = commentsBusiness.GetByID(id);
                    if (tmp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (commentsBusiness.Update(comment))
                            return Ok();
                        else
                            return NotFound();
                    }
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