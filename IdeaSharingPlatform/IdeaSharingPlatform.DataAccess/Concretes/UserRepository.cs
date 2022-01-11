using IdeaSharingPlatform.Commons.Concretes.Data;
using IdeaSharingPlatform.Commons.Concretes.Helper;
using IdeaSharingPlatform.Commons.Concretes.Logger;
using IdeaSharingPlatform.DataAccess.Abstarcts;
using IdeaSharingPlatform.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace IdeaSharingPlatform.DataAccess.Concretes
{
    public class UserRepository : IRepository<Users>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public UserRepository()
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
                query.Append("FROM [dbo].[tbl_Users] ");
                query.Append("WHERE ");
                query.Append("[UserID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [tbl_Users] can't be null. ");

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
                            throw new Exception("Deleting Error for entity [tbl_Users] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:UsersRepository::DeleteByID:Error occured.", ex);
            }
        }



        public IList<Users> GetAll()
        {
            _rowsAffected = 0;
            _errorCode = 0;

            IList<Users> users = new List<Users>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT [UserID], [UserFirstName], [UserLastName], [UserBirthDate], " +
                    "[UserEmail], [UserUsername], [UserPassword], [UserAbout], [UserLocation], [UserJoinDate] FROM [dbo].[tbl_Users] SELECT @intErrorCode=@@ERROR ");
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
                            throw new ArgumentNullException("dbCommand" + " The db GetAll command for entity [tbl_Users] can't be null. ");

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
                                    var entity = new Users();
                                    entity.UserID = reader.GetInt32(0);
                                    entity.UserFirstName = reader.GetString(1);
                                    entity.UserLastName = reader.GetString(2);
                                    entity.UserBirthDate = reader.GetDateTime(3);
                                    entity.UserEmail = reader.GetString(4);
                                    entity.UserUsername = reader.GetString(5);
                                    entity.UserPassword = reader.GetString(6);
                                    entity.UserAbout = reader.GetString(7);
                                    entity.UserLocation = reader.GetString(8);
                                    entity.UserJoinDate = reader.GetDateTime(9);

                                    users.Add(entity);
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Getting All Error for entity [tbl_Users] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:UsersRepository:GetAll::Error occured.", ex);
            }
        }

        public Users GetByID(int id)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            Users user = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("[UserID], [UserFirstName], [UserLastName], [UserBirthDate], " +
                    "[UserEmail], [UserUsername], [UserPassword], [UserAbout], [UserLocation], [UserJoinDate]");
                query.Append("FROM [dbo].[tbl_Users]");
                query.Append("WHERE ");
                query.Append("[UserID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db GetByID command for entity [tbl_Users] can't be null. ");

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
                                    var entity = new Users();
                                    entity.UserID = reader.GetInt32(0);
                                    entity.UserFirstName = reader.GetString(1);
                                    entity.UserLastName = reader.GetString(2);
                                    entity.UserBirthDate = reader.GetDateTime(3);
                                    entity.UserEmail = reader.GetString(4);
                                    entity.UserUsername = reader.GetString(5);
                                    entity.UserPassword = reader.GetString(6);
                                    entity.UserAbout = reader.GetString(7);
                                    entity.UserLocation = reader.GetString(8);
                                    entity.UserJoinDate = reader.GetDateTime(9);

                                    user = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("GetByID Error for entity [tbl_Users] reported the Database ErrorCode: " + _errorCode);
                    }
                }

                user.UsersProjects = new ProjectsRepository().GetAll().Where(x => x.ProjectOwnerID.Equals(user.UserID)).ToList();
                user.UsersLikedProjects = new UserLikesRepository().GetAll().Where(x => x.LikedProjectsUsersID.Equals(user.UserID)).ToList();
                user.UsersSavedProjects = new UserSavesRepository().GetAll().Where(x => x.SavedProjectsUsersID.Equals(user.UserID)).ToList();
                user.UsersComments = new CommentsRepository().GetAll().Where(x => x.CommentedUsersID.Equals(user.UserID)).ToList();
               
                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:UsersRepository:GetByID::Error occured.", ex);
            }
        }

        public bool Insert(Users entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;
            try
            {
                var query = new StringBuilder();
                query.Append("INSERT INTO [dbo].[tbl_Users] ");
                query.Append("([UserFirstName], [UserLastName], [UserBirthDate], " +
                    "[UserEmail],  [UserUsername], [UserPassword] ,[UserAbout], [UserLocation], [UserJoinDate])");
                query.Append(" VALUES ");
                query.Append("( @UserFirstName, @UserLastName,@UserBirthDate , @UserEmail ,@UserUsername ," +
                    "@UserPassword  , @UserAbout, @UserLocation , @UserJoinDate ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@UserFirstName", CsType.String, ParameterDirection.Input, entity.UserFirstName);
                        DBHelper.AddParameter(dbCommand, "@UserLastName", CsType.String, ParameterDirection.Input, entity.UserLastName);
                        DBHelper.AddParameter(dbCommand, "@UserBirthDate", CsType.DateTime, ParameterDirection.Input, entity.UserBirthDate);
                        DBHelper.AddParameter(dbCommand, "@UserEmail", CsType.String, ParameterDirection.Input, entity.UserEmail);
                        DBHelper.AddParameter(dbCommand, "@UserUsername", CsType.String, ParameterDirection.Input, entity.UserUsername);
                        DBHelper.AddParameter(dbCommand, "@UserPassword", CsType.String, ParameterDirection.Input, entity.UserPassword);
                        DBHelper.AddParameter(dbCommand, "@UserAbout", CsType.String, ParameterDirection.Input, entity.UserAbout);
                        DBHelper.AddParameter(dbCommand, "@UserLocation", CsType.String, ParameterDirection.Input, entity.UserLocation);
                        DBHelper.AddParameter(dbCommand, "@UserJoinDate", CsType.DateTime, ParameterDirection.Input, entity.UserJoinDate);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Users] reported the Database ErrorCode: " + _errorCode);
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

        public bool Update(Users entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("UPDATE [dbo].[tbl_Users] ");
                query.Append("SET  [UserFirstName] = @UserFirstName, [UserLastName] = @UserLastName, [UserEmail] = @UserEmail ," +
                    "[UserUsername] = @UserUsername , [UserAbout] =  @UserAbout, [UserLocation] = @UserLocation");
                query.Append(" WHERE ");
                query.Append("[UserID] = @UserID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [tbl_Users] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;
                       
                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@UserID", CsType.Int, ParameterDirection.Input, entity.UserID);
                        DBHelper.AddParameter(dbCommand, "@UserFirstName", CsType.String, ParameterDirection.Input, entity.UserFirstName);
                        DBHelper.AddParameter(dbCommand, "@UserLastName", CsType.String, ParameterDirection.Input, entity.UserLastName);
                        DBHelper.AddParameter(dbCommand, "@UserEmail", CsType.String, ParameterDirection.Input, entity.UserEmail);
                        DBHelper.AddParameter(dbCommand, "@UserUsername", CsType.String, ParameterDirection.Input, entity.UserUsername);
                        DBHelper.AddParameter(dbCommand, "@UserAbout", CsType.String, ParameterDirection.Input, entity.UserAbout);
                        DBHelper.AddParameter(dbCommand, "@UserLocation", CsType.String, ParameterDirection.Input, entity.UserLocation);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Users] reported the Database ErrorCode: " + _errorCode);
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
