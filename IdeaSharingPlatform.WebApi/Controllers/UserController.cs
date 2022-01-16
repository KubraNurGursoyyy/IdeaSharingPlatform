using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.BusinessLogic.Concretes;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using System.Web.Http.Description;
using System.Web.Http;

namespace IdeaSharingPlatform.WebApi.Controllers
{
    public class UserController : ApiController
    {
        // GET api/User/5
        //GetUserByID
        
        [ResponseType(typeof(Users))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/User/GetUser/{id}")]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                using (var userBusiness = new UsersBusiness())
                {
                    Users castedUser = null;
                    Users responsedusers = userBusiness.GetByID(id);
                    if (responsedusers != null)
                    {
                        castedUser = new Users()
                        {
                            UserAbout = responsedusers.UserAbout,
                            UserBirthDate = responsedusers.UserBirthDate,
                            UserEmail = responsedusers.UserEmail,
                            UserFirstName = responsedusers.UserFirstName,
                            UserJoinDate = responsedusers.UserJoinDate,
                            UserLastName = responsedusers.UserLastName,
                            UserLocation = responsedusers.UserLocation,
                            UserUsername = responsedusers.UserUsername,
                            UserPassword = responsedusers.UserPassword,
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
        [ResponseType(typeof(int))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/GetUserIDByEmail")]
        public IHttpActionResult GetUserIDByEmail([FromBody]string email)
        {
            try
            {
                using (var userBusiness = new UsersBusiness())
                {
                    int id = 0;
                    List<Users> responsedusers = userBusiness.GetAll();
                    if (responsedusers != null)
                    {
                        foreach (var user in responsedusers)
                        {
                            if (user.UserEmail == email)
                            {
                                id = user.UserID;
                                break;
                            }
                        }
                    }
                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }
        /*

              [ResponseType(typeof(IEnumerable<Users>))]
              [System.Web.Http.HttpGet]
              [System.Web.Http.Route("api/User/GetAllUsers")]
              public IHttpActionResult GetAllUsers()
              {
                  try
                  {
                      using (var userBusiness = new UsersBusiness())
                      {
                          IEnumerable<Users> respondedusers = userBusiness.GetAll();
                          if (respondedusers != null)
                          {
                              return Ok(respondedusers);
                          }
                          else
                          {
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

              */
        // DELETE api/User/5
        //DeleteUSerByID
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/User/DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                using (var userBusiness = new UsersBusiness())
                {
                    if (userBusiness.DeleteByID(id))
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
        [System.Web.Http.Route("api/User/PutUser/{id}")]
        public IHttpActionResult PutUser(int id, Users user)
        {
            try
            {
                if (id != user.UserID)
                {
                    return BadRequest();
                }
                using (var userBusiness = new UsersBusiness())
                {
                    Users tmp = userBusiness.GetByID(id);
                    if (tmp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (userBusiness.Update(user))
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

        // GET api/UserProject/5
        //GetUserProjectByID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Projects>))]
        [System.Web.Http.Route("api/User/GetUserProjects/{id}")]
        public IHttpActionResult GetUserProjects(int id)
        {
            try
            {
                using (var userBusiness = new UsersBusiness())
                {
                    Users responsedusers = userBusiness.GetByID(id);
                    if (responsedusers != null)
                    {
                        if (responsedusers.UsersProjects != null)
                        {
                            return Ok(responsedusers.UsersProjects);
                        }
                        else
                            return NotFound();
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        // GET api/UserProject/5
        //GetUserLikedProjectByID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Projects>))]
        [System.Web.Http.Route("api/User/GetUserLikedProjects/{id}")]
        public IHttpActionResult GetUserLikedProjects(int id)
        {
            try
            {
                using (var userBusiness = new UsersBusiness())
                {
                    Users responsedusers = userBusiness.GetByID(id);
                    if (responsedusers != null)
                    {
                        if (responsedusers.UsersLikedProjects != null)
                        {
                            return Ok(responsedusers.UsersLikedProjects);
                        }
                        else
                            return NotFound();
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        // GET api/UserProject/5
        //GetUserSavedProjectByID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Projects>))]
        [System.Web.Http.Route("api/User/GetUserSavedProjects/{id}")]
        public IHttpActionResult GetUserSavedProjects(int id)
        {
            try
            {
                using (var userBusiness = new UsersBusiness())
                {
                    Users responsedusers = userBusiness.GetByID(id);
                    if (responsedusers != null)
                    {
                        if (responsedusers.UsersSavedProjects != null)
                        {
                            return Ok(responsedusers.UsersSavedProjects);
                        }
                        else
                            return NotFound();
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }


        ////////////////////////////////////////////////////////////////77777
        ////*
        /*
        private Users GetProjectOwner(int projectid)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    Users castedUser = null;
                    Projects responsedproject = projectBusiness.GetByID(projectid);
                    if (responsedproject != null)
                    {
                        UsersBusiness usersBusiness = new UsersBusiness();
                        Users projectOwner = usersBusiness.GetByID(responsedproject.ProjectOwnerID);

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
                    return castedUser;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Owner didn't find.");
            }
        }

        private Categories GetProjectCategoryName(int projectid)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    Categories castedCategory = null;
                    Projects responsedproject = projectBusiness.GetByID(projectid);
                    if (responsedproject != null)
                    {
                        CategoriesBusiness categoryBusiness = new CategoriesBusiness();
                        Categories projectCategory = categoryBusiness.GetByID(responsedproject.ProjectsCategoryId);

                        castedCategory = new Categories()
                        {
                            CategoryName = projectCategory.CategoryName,
                            CategoryID = projectCategory.CategoryID,
                            CategoriesProjects = projectCategory.CategoriesProjects,
                        };
                    }
                    return castedCategory;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Category didn't find.");
            }
        }*/
    }
}