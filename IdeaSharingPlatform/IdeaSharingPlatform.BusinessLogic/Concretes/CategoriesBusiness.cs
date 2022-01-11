using IdeaSharingPlatform.BusinessLogic.Abstracts;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.DataAccess.Concretes;
using System;
using System.Collections.Generic;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;


namespace IdeaSharingPlatform.BusinessLogic.Concretes
{
    public class CategoriesBusiness  : IDisposable
    {
        public bool Insert(Categories entity)
        {
            try
            {
                bool isSuccess;
                using (var catRepo = new CategoriesRepository())
                {
                    isSuccess = catRepo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(Categories entity)
        {
            try
            {
                bool isSuccess;
                using (var catRepo = new CategoriesRepository())
                {
                    isSuccess = catRepo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Update:Error occured.", ex);
            }
        }

        public bool DeleteByID(int id)
        {
            try
            {
                bool isSuccess;
                using (var catRepo = new CategoriesRepository())
                {
                    isSuccess = catRepo.DeleteByID(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes::CategoriesBusiness::Delete:Error occured.", ex);
            }
        }

        public Categories GetByID(int id)
        {
            try
            {
                Categories ResponseEntity;
                using (var catRepo = new CategoriesRepository())
                {
                    ResponseEntity = catRepo.GetByID(id);
                    if (ResponseEntity == null)
                    {
                        throw new NullReferenceException("Entity doesnt exists!");
                    }
                    return ResponseEntity;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:CategoriesBusinessLogic::GetByID::Error occured.", ex);
            }
        }

        public List<Categories> GetAll()
        {
            var ResponseEntity = new List<Categories>();
            try
            {
                using (var catRepo = new CategoriesRepository())
                {
                    foreach (var entity in catRepo.GetAll())
                    {
                        ResponseEntity.Add(entity);
                    }
                }
                return ResponseEntity;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:CategoriesBusinessLogic::GetAll::Error occured.", ex);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
