using IdeaSharingPlatform.BusinessLogic.Abstracts;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.DataAccess.Concretes;
using System;
using System.Collections.Generic;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;


namespace IdeaSharingPlatform.BusinessLogic.Concretes
{
    public class UserLikesBusiness : IDisposable
    {
        public bool Insert(UserLikes entity)
        {
            try
            {
                bool isSuccess;
                using (var userlikesRepo = new UserLikesRepository())
                {
                    isSuccess = userlikesRepo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(UserLikes entity)
        {
            try
            {
                bool isSuccess;
                using (var userlikesRepo = new UserLikesRepository())
                {
                    isSuccess = userlikesRepo.Update(entity);
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
                using (var userlikesRepo = new UserLikesRepository())
                {
                    isSuccess = userlikesRepo.DeleteByID(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes::UserLikesBusiness::Delete:Error occured.", ex);
            }
        }

        public UserLikes GetByID(int id)
        {
            try
            {
                UserLikes ResponseEntity;
                using (var userlikesRepo = new UserLikesRepository())
                {
                    ResponseEntity = userlikesRepo.GetByID(id);
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
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:UserLikesBusiness::GetByID::Error occured.", ex);
            }
        }

        public List<UserLikes> GetAll()
        {
            var ResponseEntity = new List<UserLikes>();
            try
            {
                using (var userlikesRepo = new UserLikesRepository())
                {
                    foreach (var entity in userlikesRepo.GetAll())
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
