using IdeaSharingPlatform.BusinessLogic.Abstracts;
using IdeaSharingPlatform.Models.Concretes;
using IdeaSharingPlatform.DataAccess.Concretes;
using System;
using System.Collections.Generic;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;


namespace IdeaSharingPlatform.BusinessLogic.Concretes
{
    public class CommentsBusiness : IDisposable
    {
        public bool Insert(Comments entity)
        {
            try
            {
                bool isSuccess;
                using (var comRepo = new CommentsRepository())
                {
                    isSuccess = comRepo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(Comments entity)
        {
            try
            {
                bool isSuccess;
                using (var comRepo = new CommentsRepository())
                {
                    isSuccess = comRepo.Update(entity);
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
                using (var comRepo = new CommentsRepository())
                {
                    isSuccess = comRepo.DeleteByID(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes::CommentsBusiness::Delete:Error occured.", ex);
            }
        }

        public Comments GetByID(int id)
        {
            try
            {
                Comments ResponseEntity;
                using (var comRepo = new CommentsRepository())
                {
                    ResponseEntity = comRepo.GetByID(id);
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
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:CommentsBusiness::GetByID::Error occured.", ex);
            }
        }

        public List<Comments> GetAll()
        {
            var ResponseEntity = new List<Comments>();
            try
            {
                using (var comRepo = new CommentsRepository())
                {
                    foreach (var entity in comRepo.GetAll())
                    {
                        ResponseEntity.Add(entity);
                    }
                }
                return ResponseEntity;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.BusinessLogic.Concretes:CommentsBusiness::GetAll::Error occured.", ex);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
