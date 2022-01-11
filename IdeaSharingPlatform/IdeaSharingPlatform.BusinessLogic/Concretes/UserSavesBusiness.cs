using IdeaSharingPlatform.BusinessLogic.Abstracts;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.DataAccess.Concretes;
using System;
using System.Collections.Generic;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;

namespace IdeaSharingPlatform.BusinessLogic.Concretes
{
    public class UserSavesBusiness : IDisposable
    {
        public bool Insert(UserSaves entity)
        {
            try
            {
                bool isSuccess;
                using (var usersavesRepo = new UserSavesRepository())
                {
                    isSuccess = usersavesRepo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(UserSaves entity)
        {
            try
            {
                bool isSuccess;
                using (var usersavesRepo = new UserSavesRepository())
                {
                    isSuccess = usersavesRepo.Update(entity);
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
                using (var usersavesRepo = new UserSavesRepository())
                {
                    isSuccess = usersavesRepo.DeleteByID(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes::UserSavesBusiness::Delete:Error occured.", ex);
            }
        }

        public UserSaves GetByID(int id)
        {
            try
            {
                UserSaves ResponseEntity;
                using (var usersavesRepo = new UserSavesRepository())
                {
                    ResponseEntity = usersavesRepo.GetByID(id);
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
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:UserSavesBusiness::GetByID::Error occured.", ex);
            }
        }

        public List<UserSaves> GetAll()
        {
            var ResponseEntity = new List<UserSaves>();
            try
            {
                using (var usersavesRepo = new UserSavesRepository())
                {
                    foreach (var entity in usersavesRepo.GetAll())
                    {
                        ResponseEntity.Add(entity);
                    }
                }
                return ResponseEntity;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:UserSavesBusiness::GetAll::Error occured.", ex);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
