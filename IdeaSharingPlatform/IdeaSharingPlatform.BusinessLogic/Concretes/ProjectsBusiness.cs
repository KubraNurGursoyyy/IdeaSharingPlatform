using IdeaSharingPlatform.BusinessLogic.Abstracts;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.DataAccess.Concretes;
using System;
using System.Collections.Generic;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;


namespace IdeaSharingPlatform.BusinessLogic.Concretes
{
    public class ProjectsBusiness :  IDisposable
    {
        public bool Insert(Projects entity)
        {
            try
            {
                bool isSuccess;
                using (var proRepo = new ProjectsRepository())
                {
                    isSuccess = proRepo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(Projects entity)
        {
            try
            {
                bool isSuccess;
                using (var proRepo = new ProjectsRepository())
                {
                    isSuccess = proRepo.Update(entity);
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
                using (var proRepo = new ProjectsRepository())
                {
                    isSuccess = proRepo.DeleteByID(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes::ProjectsBusiness::Delete:Error occured.", ex);
            }
        }

        public Projects GetByID(int id)
        {
            try
            {
                Projects ResponseEntity;
                using (var proRepo = new ProjectsRepository())
                {
                    ResponseEntity = proRepo.GetByID(id);
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
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:ProjectsBusiness::GetByID::Error occured.", ex);
            }
        }

        public List<Projects> GetAll()
        {
            var ResponseEntity = new List<Projects>();
            try
            {
                using (var proRepo = new ProjectsRepository())
                {
                    foreach (var entity in proRepo.GetAll())
                    {
                        ResponseEntity.Add(entity);
                    }
                }
                return ResponseEntity;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:ProjectsBusiness::GetAll::Error occured.", ex);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
