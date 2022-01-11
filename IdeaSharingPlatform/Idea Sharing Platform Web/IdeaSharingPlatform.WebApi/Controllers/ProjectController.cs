using IdeaSharingPlatform.BusinessLogic.Concretes;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.WebApi.ServiceAccess.Abstracts;
using IdeaSharingPlatform.WebApi.ServiceAccess.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace IdeaSharingPlatform.WebApi.Controllers
{
    public class ProjectController : ApiController
    {
        // GET api/User/5
        //GetProjectsOwner
        [ResponseType(typeof(Users))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetProjectOwner/{id}")]
        public IHttpActionResult GetProjectOwner(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    Users castedUser = null;
                    Projects responsedproject = projectBusiness.GetByID(id);
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
                    return Ok(castedUser);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        //GetProjectCategory
        [ResponseType(typeof(Categories))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetProjectCategory/{id}")]
        public IHttpActionResult GetProjectCategory(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    Categories castedCategory = null;
                    Projects responsedproject = projectBusiness.GetByID(id);
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
                    return Ok(castedCategory);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        //GetProjectLikeNumber
        [ResponseType(typeof(int))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetProjectLikeNumber/{id}")]
        public IHttpActionResult GetProjectLikeNumber(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    int projectLike = 0;
                    Projects responsedproject = projectBusiness.GetByID(id);
                    if (responsedproject != null)
                    {
                        projectLike = responsedproject.ProjectsLikedUsers.Count;
                    }
                    return Ok(projectLike);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        //GetProjectSaveNumber
        [ResponseType(typeof(int))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetProjectSaveNumber/{id}")]
        public IHttpActionResult GetProjectSaveNumber(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    int projectSave = 0;
                    Projects responsedproject = projectBusiness.GetByID(id);
                    if (responsedproject != null)
                    {
                        projectSave = responsedproject.ProjectsSavedUsers.Count;
                    }
                    return Ok(projectSave);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }
        //GetProjectComments
        [ResponseType(typeof(Comments))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetProjectComments/{id}")]
        public IHttpActionResult GetProjectComments(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    List<Comments> comments = null;
                    Projects responsedproject = projectBusiness.GetByID(id);
                    if (responsedproject != null)
                    {
                        comments = responsedproject.ProjectsComments;
                    }
                    return Ok(comments);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }

        // GET api/User/5
        //GetProjectByID
        [ResponseType(typeof(Projects))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetProject/{id}")]
        public IHttpActionResult GetProject(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    Projects castedProject = null;
                    Projects responsedproject = projectBusiness.GetByID(id);
                    if (responsedproject != null)
                    {
                        castedProject = new Projects()
                        {
                            ProjectID = responsedproject.ProjectID,
                            ProjectsComments = responsedproject.ProjectsComments,
                            Description = responsedproject.Description,
                            ProjectBlurb = responsedproject.ProjectBlurb,  
                            ProjectCreationDate = responsedproject.ProjectCreationDate,
                            ProjectName = responsedproject.ProjectName,
                            UpdateTime = responsedproject.UpdateTime,
                        };
                    }
                    return Ok(castedProject);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return NotFound();
            }
        }
        // GET api/User/5
        //GetAllProjectByID
        [ResponseType(typeof(IEnumerable<Projects>))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Project/GetAllProject")]
        public IHttpActionResult GetAllProject()
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    IEnumerable<Projects> respondedprojects = projectBusiness.GetAll();
                    if (respondedprojects != null)
                    {
                        return Ok(respondedprojects);
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

        //BUNDA DAHA KATEGORİ DE OLACAK BU ÇOK AYRI BİR OLAY O YÜZDEN
        // POST api/User
        //kategroi bilgisi
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Project/CreateProject")]
        public IHttpActionResult CreateProject(Projects project)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    int categoryId = 1;
                    using (var categoryBusiness = new CategoriesBusiness())
                    {
                        List<Categories> categories = categoryBusiness.GetAll();     

                        using (ICategoryService categorySevice = new CategoryService())
                        {
                            string categoryName = categorySevice.GetCategory(project.ProjectBlurb);
                            foreach (Categories category in categories)
                            {
                                if (category.CategoryName == categoryName)
                                {
                                     categoryId = category.CategoryID;
                                }
                                 
                            }
                        } 
                    }
                    project.ProjectsCategoryId = categoryId;
                    if (projectBusiness.Insert(project))
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
        ///BU ÇOK AYRI BİR OLAY


        // DELETE api/User/5
        //DeleteProjectByID
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Project/DeleteProject/{id}")]
        public IHttpActionResult DeleteProject(int id)
        {
            try
            {
                using (var projectBusiness = new ProjectsBusiness())
                {
                    if (projectBusiness.DeleteByID(id))
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
        //UpdateProject
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Project/UpdateProject/{id}")]
        public IHttpActionResult UpdateProject(int id, Projects project)
        {
            try
            {
                if (id != project.ProjectID)
                {
                    return BadRequest();
                }
                using (var projectBusiness = new ProjectsBusiness())
                {
                    Projects tmp = projectBusiness.GetByID(id);
                    if (tmp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (projectBusiness.Update(project))
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