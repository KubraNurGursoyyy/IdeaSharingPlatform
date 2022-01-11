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
    public class CategoriesController : ApiController
    {
        //kategorilerin hepsini getir,
        //kategorilerin projelerini getir
        // GET: Categories


        // GET api/User/5
        //GetAllProjectByID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Categories>))]
        [System.Web.Http.Route("api/Categories/GetAllCategories")]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                using (var categoriesBusiness = new CategoriesBusiness())
                {
                    List<Categories> respondedcategories = categoriesBusiness.GetAll();
                    if (respondedcategories != null)
                    {
                        return Ok(respondedcategories);
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

        // GET api/UserProject/5
        //GetUserProjectByID
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Projects>))]
        [System.Web.Http.Route("api/Categories/GetCategoriesProjects/{id}")]
        public IHttpActionResult GetCategoriesProjects(int id)
        {
            try
            {
                using (var categoriesBusiness = new CategoriesBusiness())
                {
                    Categories responsedcategories = categoriesBusiness.GetByID(id);
                    if (responsedcategories != null)
                    {
                        if (responsedcategories.CategoriesProjects != null)
                        {
                            return Ok(responsedcategories.CategoriesProjects);
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
    }
}