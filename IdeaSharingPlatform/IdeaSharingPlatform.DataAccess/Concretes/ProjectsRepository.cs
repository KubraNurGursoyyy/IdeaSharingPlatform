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
    public class ProjectsRepository : IRepository<Projects>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public ProjectsRepository()
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
                query.Append("FROM [dbo].[tbl_Projects] ");
                query.Append("WHERE ");
                query.Append("[ProjectID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [tbl_Projects] can't be null. ");

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
                            throw new Exception("Deleting Error for entity [tbl_Projects] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:ProjectsRepository::DeleteByID:Error occured.", ex);
            }
        }



        public IList<Projects> GetAll()
        {
            _rowsAffected = 0;
            _errorCode = 0;

            IList<Projects> projects = new List<Projects>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT [ProjectID], [ProjectName], [ProjectBlurb], [ProjectCreationDate], " +
                    "[UpdateTime], [Description], [ProjectsCategoryId], [ProjectOwnerID] FROM [dbo].[tbl_Projects] SELECT @intErrorCode=@@ERROR ");
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
                            throw new ArgumentNullException("dbCommand" + " The db GetAll command for entity [tbl_Projects] can't be null. ");

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
                                    var entity = new Projects();
                                    entity.ProjectID = reader.GetInt32(0);
                                    entity.ProjectName = reader.GetString(1);
                                    entity.ProjectBlurb = reader.GetString(2);
                                    entity.ProjectCreationDate = reader.GetDateTime(3);
                                    entity.UpdateTime = reader.GetDateTime(4);
                                    entity.Description = reader.GetString(5);
                                    entity.ProjectsCategoryId = reader.GetInt32(6);
                                    entity.ProjectOwnerID = reader.GetInt32(7);
                                    projects.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Getting All Error for entity [tbl_Projects] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                return projects;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:ProjectsRepository:GetAll::Error occured.", ex);
            }
        }

        public Projects GetByID(int id)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            Projects project = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(" [ProjectID], [ProjectName], [ProjectBlurb], [ProjectCreationDate], " +
                    "[UpdateTime], [Description], [ProjectsCategoryId], [ProjectOwnerID]");
                query.Append("FROM [dbo].[tbl_Projects] ");
                query.Append("WHERE ");
                query.Append("[ProjectID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db GetByID command for entity [tbl_Projects] can't be null. ");

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
                                    var entity = new Projects();
                                    entity.ProjectID = reader.GetInt32(0);
                                    entity.ProjectName = reader.GetString(1);
                                    entity.ProjectBlurb = reader.GetString(2);
                                    entity.ProjectCreationDate = reader.GetDateTime(3);
                                    entity.UpdateTime = reader.GetDateTime(4);
                                    entity.Description = reader.GetString(5);
                                    entity.ProjectsCategoryId = reader.GetInt32(6);
                                    entity.ProjectOwnerID = reader.GetInt32(7);

                                    project = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("GetByID Error for entity [tbl_Projects] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                 
                project.ProjectsLikedUsers = new UserLikesRepository().GetAll().Where(x => x.LikedProjectID.Equals(project.ProjectID)).ToList();
                project.ProjectsSavedUsers = new UserSavesRepository().GetAll().Where(x => x.SavedProjectID.Equals(project.ProjectID)).ToList();
                project.ProjectsComments = new CommentsRepository().GetAll().Where(x => x.CommentedProjectID.Equals(project.ProjectID)).ToList();

                return project;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                LogHelper.Log(LogTarget.Database, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IdeaSharingPlatform.DataAccess.Concretes:ProjectsRepository:GetByID::Error occured.", ex);
            }
        }

        public bool Insert(Projects entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;
            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Projects] ");
                query.Append("( [ProjectName], [ProjectBlurb], [ProjectCreationDate], " +
                    "[UpdateTime], [Description], [ProjectsCategoryId], [ProjectOwnerID])");
                query.Append(" VALUES ");
                query.Append("( @ProjectName, @ProjectBlurb,@ProjectCreationDate , @UpdateTime ,@Description ," +
                    "@ProjectsCategoryId  , @ProjectOwnerID) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Projects] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                       
                        DBHelper.AddParameter(dbCommand, "@ProjectName", CsType.String, ParameterDirection.Input, entity.ProjectName);
                        DBHelper.AddParameter(dbCommand, "@ProjectBlurb", CsType.String, ParameterDirection.Input, entity.ProjectBlurb);
                        DBHelper.AddParameter(dbCommand, "@ProjectCreationDate", CsType.DateTime, ParameterDirection.Input, entity.ProjectCreationDate);
                        DBHelper.AddParameter(dbCommand, "@UpdateTime", CsType.DateTime, ParameterDirection.Input, entity.UpdateTime);
                        DBHelper.AddParameter(dbCommand, "@Description", CsType.String, ParameterDirection.Input, entity.Description);
                        DBHelper.AddParameter(dbCommand, "@ProjectsCategoryId", CsType.Int, ParameterDirection.Input, entity.ProjectsCategoryId);
                        DBHelper.AddParameter(dbCommand, "@ProjectOwnerID", CsType.Int, ParameterDirection.Input, entity.ProjectOwnerID);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Projects] reported the Database ErrorCode: " + _errorCode);
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

        public bool Update(Projects entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("UPDATE [dbo].[tbl_Projects] ");
                query.Append("SET [ProjectName] = @ProjectName, [ProjectBlurb] = @ProjectBlurb ,[UpdateTime] = @UpdateTime , [Description] = @Description");
                query.Append(" WHERE ");
                query.Append("[ProjectID] = @ProjectID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [tbl_Projects] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@ProjectID", CsType.Int, ParameterDirection.Input, entity.ProjectID);
                        DBHelper.AddParameter(dbCommand, "@ProjectName", CsType.String, ParameterDirection.Input, entity.ProjectName);
                        DBHelper.AddParameter(dbCommand, "@ProjectBlurb", CsType.String, ParameterDirection.Input, entity.ProjectBlurb);
                        DBHelper.AddParameter(dbCommand, "@UpdateTime", CsType.DateTime, ParameterDirection.Input, entity.UpdateTime);
                        DBHelper.AddParameter(dbCommand, "@Description", CsType.String, ParameterDirection.Input, entity.Description);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Projects] reported the Database ErrorCode: " + _errorCode);
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
