using IdeaSharingPlatform.BusinessLogic.Abstracts;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.DataAccess.Concretes;
using System;
using System.Collections.Generic;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;

namespace IdeaSharingPlatform.BusinessLogic.Concretes
{
    public class UsersBusiness : ILogInBusinessLogic, IDisposable
    {
        public bool Insert(Users entity)
        {
            try
            {
                bool isSuccess;
                using (var userRepo = new UserRepository())
                {
                    isSuccess = userRepo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(Users entity)
        {
            try
            {
                bool isSuccess;
                using (var userRepo = new UserRepository())
                {
                    isSuccess = userRepo.Update(entity);
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
                using (var userRepo = new UserRepository())
                {
                    isSuccess = userRepo.DeleteByID(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes::Delete:Error occured.", ex);
            }
        }

        public Users GetByID(int id)
        {
            try
            {
                Users ResponseEntity;
                using (var userRepo = new UserRepository())
                {
                    ResponseEntity = userRepo.GetByID(id);
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
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:GenericBusinessLogic::GetByID::Error occured.", ex);
            }
        }

        public List<Users> GetAll()
        {
            var ResponseEntity = new List<Users>();
            try
            {
                using (var userRepo = new UserRepository())
                {
                    foreach (var entity in userRepo.GetAll())
                    {
                        ResponseEntity.Add(entity);
                    }
                }
                return ResponseEntity;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:GenericBusinessLogic::GetAll::Error occured.", ex);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
        public bool LogIn(string Email, string Password)
        {
            bool login = false;
            try
            {
                using (var userRepo = new UserRepository())
                {
                    foreach (var entity in userRepo.GetAll())
                    {
                        if (entity.UserEmail == Email && entity.UserPassword == Password)
                        {
                            login = true;
                        }
                    }
                }
                return login;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:UserBusiness:LogIn::Error occured.", ex);
            }
        }
    }
}
