using IdeaSharingPlatform.Commons.Concretes.Data;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.DataAccess.Abstarcts;
using IdeaSharingPlatform.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace IdeaSharingPlatform.DataAccess.Concretes
{
    public class UserLikesRepository : IRepository<UserLikes>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public UserLikesRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }

        public bool DeleteByID(int id)
        {
            _rowsAffected = 0;
            _errorCode = 0;
            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_UserLikes] ");
                query.Append(" WHERE ");
                query.Append("[UserLikesID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");


                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [tbl_UserLikes] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Deleting Error for entity [tbl_UserLikes] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:UserLikesRepository::DeleteByID:Error occured.", ex);
            }
        }



        public IList<UserLikes> GetAll()
        {
            _rowsAffected = 0;
            _errorCode = 0;

            IList<UserLikes> userlikes = new List<UserLikes>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT [UserLikesID], [LikedProjectID], [LikedProjectsUsersID] FROM [dbo].[tbl_UserLikes] SELECT @intErrorCode=@@ERROR ");
                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db GetAll command for entity [tbl_UserLikes] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new UserLikes();
                                    entity.UserLikesID = reader.GetInt32(0);
                                    entity.LikedProjectID = reader.GetInt32(1);
                                    entity.LikedProjectsUsersID = reader.GetInt32(2);
                                    userlikes.Add(entity);
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Getting All Error for entity [tbl_UserLikes] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return userlikes;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:UserLikesRepository:GetAll::Error occured.", ex);
            }
        }

        public UserLikes GetByID(int id)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            UserLikes userlike = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("[UserLikesID],[LikedProjectID], [LikedProjectsUsersID]");
                query.Append(" FROM [dbo].[tbl_UserLikes] ");
                query.Append("WHERE ");
                query.Append("[UserLikesID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db GetByID command for entity [tbl_UserLikes] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new UserLikes();
                                    entity.UserLikesID = reader.GetInt32(0);
                                    entity.LikedProjectID = reader.GetInt32(1);
                                    entity.LikedProjectsUsersID = reader.GetInt32(2);

                                    userlike = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("GetByID Error for entity [tbl_UserLikes] reported the Database ErrorCode: " + _errorCode);
                    }
                }

                return userlike;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:UserLikesRepository:GetByID::GetByID:Error occured.", ex);
            }
        }

        public bool Insert(UserLikes entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;
            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_UserLikes] ");
                query.Append("([LikedProjectID], [LikedProjectsUsersID]) ");
                query.Append("VALUES ");
                query.Append("( @LikedProjectID, @LikedProjectsUsersID ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_UserLikes] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@LikedProjectID", CsType.Int, ParameterDirection.Input, entity.LikedProjectID);
                        DBHelper.AddParameter(dbCommand, "@LikedProjectsUsersID", CsType.Int, ParameterDirection.Input, entity.LikedProjectsUsersID);
                        
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_UserLikes] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes: " + entity.GetType().ToString() + "::Insert:Error occured.", ex);
            }
        }

        public bool Update(UserLikes entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("UPDATE [dbo].[tbl_UserLikes] ");
                query.Append("SET [LikedProjectID] = @LikedProjectID , [LikedProjectsUsersID] = @LikedProjectsUsersID");
                query.Append(" WHERE ");
                query.Append("[UserLikesID] = @UserLikesID ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [tbl_UserLikes] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@UserLikesID", CsType.Int, ParameterDirection.Input, entity.UserLikesID);
                        DBHelper.AddParameter(dbCommand, "@LikedProjectID", CsType.Int, ParameterDirection.Input, entity.LikedProjectID);
                        DBHelper.AddParameter(dbCommand, "@LikedProjectsUsersID", CsType.Int, ParameterDirection.Input, entity.LikedProjectsUsersID);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_UserLikes] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes: " + entity.GetType().ToString() + "::Update:Error occured.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }
    }
}
