using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Xml;
using Common;

namespace Dao
{
    /// <summary>
    /// Sqlite数据库帮助类
    /// </summary>
    public static class SQLiteHelper
    {
        #region 数据库信息
        /// <summary>
        /// 数据库所在文件夹
        /// </summary>
        private static string FileName
        {
            get { return Util.GetConfig("db.datafile"); }
        }
        /// <summary>
        /// 数据库文件名
        /// </summary>
        private static string DataName
        {
            get { return Util.GetConfig("db.dataname"); }
        }
        /// <summary>
        /// 数据库所在文件夹
        /// </summary>
        public static string DataFullPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + FileName + "\\"; }
        }
        /// <summary>
        /// 数据库所在文件夹
        /// </summary>
        public static string DataFilePath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + FileName + "\\" + DataName; }
        }
        /// <summary>
        /// SQLite数据库连接字符串
        /// </summary>
        public static string DataSource
        {
            get { return "Data Source=" + DataFilePath; }
        }
        #endregion 

        /// <summary>  
        /// 创建SQLite数据库文件  
        /// </summary>  
        /// <param name="sqlitePath">要创建的SQLite数据库文件路径</param>  
        public static void CreateDataBase()
        {
            SQLiteConnection.CreateFile(DataFilePath);
        }
        /// <summary>  
        /// 创建SQLite数据库文件  
        /// </summary>  
        /// <param name="sqlitePath">要创建的SQLite数据库文件路径</param>  
        public static void CreateDataBase(string dataFilePath)
        {
            SQLiteConnection.CreateFile(dataFilePath);
        }

        #region private utility methods & constructors

		/// <summary>
		/// This method is used to attach array of SqlParameters to a SQLiteCommand.
		/// 
		/// This method will assign a value of DbNull to any parameter with a direction of
		/// InputOutput and a value of null.  
		/// 
		/// This behavior will prevent default values from being used, but
		/// this will be the less common case than an intended pure output parameter (derived as InputOutput)
		/// where the user provided no input value.
		/// </summary>
		/// <param name="command">The command to which the parameters will be added</param>
		/// <param name="commandParameters">an array of SqlParameters tho be added to command</param>
		private static void AttachParameters(SQLiteCommand command, SQLiteParameter[] commandParameters)
		{
			foreach (SQLiteParameter p in commandParameters)
			{
				//check for derived output value with no value assigned
				if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
				{
					p.Value = DBNull.Value;
				}
				
				command.Parameters.Add(p);
			}
		}

		/// <summary>
		/// This method assigns an array of values to an array of SqlParameters.
		/// </summary>
		/// <param name="commandParameters">array of SqlParameters to be assigned values</param>
		/// <param name="parameterValues">array of objects holding the values to be assigned</param>
		private static void AssignParameterValues(SQLiteParameter[] commandParameters, object[] parameterValues)
		{
			if ((commandParameters == null) || (parameterValues == null)) 
			{
				//do nothing if we get no data
				return;
			}

			// we must have the same number of values as we pave parameters to put them in
			if (commandParameters.Length != parameterValues.Length)
			{
				throw new ArgumentException("Parameter count does not match Parameter Value count.");
			}

			//iterate through the SqlParameters, assigning the values from the corresponding position in the 
			//value array
			for (int i = 0, j = commandParameters.Length; i < j; i++)
			{
				commandParameters[i].Value = parameterValues[i];
			}
		}

		/// <summary>
		/// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
		/// to the provided command.
		/// </summary>
		/// <param name="command">the SQLiteCommand to be prepared</param>
		/// <param name="connection">a valid SQLiteConnection, on which to execute this command</param>
		/// <param name="transaction">a valid SQLiteTransaction, or 'null'</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
		private static void PrepareCommand(SQLiteCommand command, SQLiteConnection connection, SQLiteTransaction transaction, CommandType commandType, string commandText, SQLiteParameter[] commandParameters)
		{
			//if the provided connection is not open, we will open it
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			//associate the connection with the command
			command.Connection = connection;

			//set the command text (stored procedure name or SQL statement)
			command.CommandText = commandText;

			//if we were provided a transaction, assign it.
			if (transaction != null)
			{
				command.Transaction = transaction;
			}

			//set the command type
			command.CommandType = commandType;

			//attach the command parameters if they are provided
			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}

			return;
		}


		#endregion private utility methods & constructors

		#region ExecuteNonQuery
		/// <summary>
		/// Execute a SQLiteCommand (that returns no resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteNonQuery(connectionString, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns no resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create & open a SQLiteConnection, and dispose of it after we are done.
			using (SQLiteConnection cn = new SQLiteConnection(connectionString))
			{
				cn.Open();

				//call the overload that takes a connection in place of the connection string
				return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
			}
		}
       

		/// <summary>
		/// Execute a SQLiteCommand (that returns no resultset and takes no parameters) against the provided SQLiteConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SQLiteConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteNonQuery(connection, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns no resultset) against the specified SQLiteConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SQLiteConnection connection, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{	
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, connection, (SQLiteTransaction)null, commandType, commandText, commandParameters);
			
			//finally, execute the command.
			int retval = cmd.ExecuteNonQuery();
			
			// detach the SqlParameters from the command object, so they can be used again.
			cmd.Parameters.Clear();
			return retval;
		}       

		/// <summary>
		/// Execute a SQLiteCommand (that returns no resultset and takes no parameters) against the provided SQLiteTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SQLiteTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteNonQuery(transaction, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns no resultset) against the specified SQLiteTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SQLiteTransaction transaction, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			
			//finally, execute the command.
			int retval = cmd.ExecuteNonQuery();
			
			// detach the SqlParameters from the command object, so they can be used again.
			cmd.Parameters.Clear();
			return retval;
		}  
		#endregion ExecuteNonQuery

		#region ExecuteDataSet

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteDataset(connectionString, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create & open a SQLiteConnection, and dispose of it after we are done.
			using (SQLiteConnection cn = new SQLiteConnection(connectionString))
			{
				cn.Open();

				//call the overload that takes a connection in place of the connection string
				return ExecuteDataset(cn, commandType, commandText, commandParameters);
			}
		}        

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the provided SQLiteConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(SQLiteConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteDataset(connection, commandType, commandText, (SQLiteParameter[])null);
		}
		
		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset) against the specified SQLiteConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(SQLiteConnection connection, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, connection, (SQLiteTransaction)null, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
			DataSet ds = new DataSet();

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(ds);
			
			// detach the SqlParameters from the command object, so they can be used again.			
			cmd.Parameters.Clear();
			
			//return the dataset
			return ds;						
		}		       

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the provided SQLiteTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(SQLiteTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteDataset(transaction, commandType, commandText, (SQLiteParameter[])null);
		}
		
		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset) against the specified SQLiteTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(SQLiteTransaction transaction, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
			DataSet ds = new DataSet();

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(ds);
			
			// detach the SqlParameters from the command object, so they can be used again.
			cmd.Parameters.Clear();
			
			//return the dataset
			return ds;
		}		        

		#endregion ExecuteDataSet
		
		#region ExecuteReader

		/// <summary>
		/// this enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
		/// we can set the appropriate CommandBehavior when calling ExecuteReader()
		/// </summary>
		private enum SqlConnectionOwnership	
		{
			/// <summary>Connection is owned and managed by SqlHelper</summary>
			Internal, 
			/// <summary>Connection is owned and managed by the caller</summary>
			External
		}

		/// <summary>
		/// Create and prepare a SQLiteCommand, and call ExecuteReader with the appropriate CommandBehavior.
		/// </summary>
		/// <remarks>
		/// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
		/// 
		/// If the caller provided the connection, we want to leave it to them to manage.
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection, on which to execute this command</param>
		/// <param name="transaction">a valid SQLiteTransaction, or 'null'</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
		/// <param name="connectionOwnership">indicates whether the connection parameter was provided by the caller, or created by SqlHelper</param>
		/// <returns>SQLiteDataReader containing the results of the command</returns>
		private static SQLiteDataReader ExecuteReader(SQLiteConnection connection, SQLiteTransaction transaction, CommandType commandType, string commandText, SQLiteParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
		{	
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//create a reader
			SQLiteDataReader dr;

			// call ExecuteReader with the appropriate CommandBehavior
			if (connectionOwnership == SqlConnectionOwnership.External)
			{
				dr = cmd.ExecuteReader();
			}
			else
			{
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			
			// detach the SqlParameters from the command object, so they can be used again.
			cmd.Parameters.Clear();
			
			return dr;
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SQLiteDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a SQLiteDataReader containing the resultset generated by the command</returns>
		public static SQLiteDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteReader(connectionString, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SQLiteDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a SQLiteDataReader containing the resultset generated by the command</returns>
		public static SQLiteDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create & open a SQLiteConnection
			SQLiteConnection cn = new SQLiteConnection(connectionString);
			cn.Open();

			try
			{
				//call the private overload that takes an internally owned connection in place of the connection string
				return ExecuteReader(cn, null, commandType, commandText, commandParameters,SqlConnectionOwnership.Internal);
			}
			catch
			{
				//if we fail to return the SqlDatReader, we need to close the connection ourselves
				cn.Close();
				throw;
			}
		}
        
		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the provided SQLiteConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SQLiteDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a SQLiteDataReader containing the resultset generated by the command</returns>
		public static SQLiteDataReader ExecuteReader(SQLiteConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteReader(connection, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset) against the specified SQLiteConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SQLiteDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a SQLiteDataReader containing the resultset generated by the command</returns>
		public static SQLiteDataReader ExecuteReader(SQLiteConnection connection, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//pass through the call to the private overload using a null transaction value and an externally owned connection
			return ExecuteReader(connection, (SQLiteTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
		}
        
		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the provided SQLiteTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SQLiteDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a SQLiteDataReader containing the resultset generated by the command</returns>
		public static SQLiteDataReader ExecuteReader(SQLiteTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteReader(transaction, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a resultset) against the specified SQLiteTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///   SQLiteDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a SQLiteDataReader containing the resultset generated by the command</returns>
		public static SQLiteDataReader ExecuteReader(SQLiteTransaction transaction, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//pass through to private overload, indicating that the connection is owned by the caller
			return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
		}       

		#endregion ExecuteReader

		#region ExecuteScalar
		/// <summary>
		/// Execute a SQLiteCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteScalar(connectionString, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a 1x1 resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create & open a SQLiteConnection, and dispose of it after we are done.
			using (SQLiteConnection cn = new SQLiteConnection(connectionString))
			{
				cn.Open();

				//call the overload that takes a connection in place of the connection string
				return ExecuteScalar(cn, commandType, commandText, commandParameters);
			}
		}       

		/// <summary>
		/// Execute a SQLiteCommand (that returns a 1x1 resultset and takes no parameters) against the provided SQLiteConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SQLiteConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteScalar(connection, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a 1x1 resultset) against the specified SQLiteConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SQLiteConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SQLiteConnection connection, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, connection, (SQLiteTransaction)null, commandType, commandText, commandParameters);
			
			//execute the command & return the results
			object retval = cmd.ExecuteScalar();
			
			// detach the SqlParameters from the command object, so they can be used again.
			cmd.Parameters.Clear();
			return retval;
			
		}       

		/// <summary>
		/// Execute a SQLiteCommand (that returns a 1x1 resultset and takes no parameters) against the provided SQLiteTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SQLiteTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteScalar(transaction, commandType, commandText, (SQLiteParameter[])null);
		}

		/// <summary>
		/// Execute a SQLiteCommand (that returns a 1x1 resultset) against the specified SQLiteTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SQLiteParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid SQLiteTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SQLiteTransaction transaction, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SQLiteCommand cmd = new SQLiteCommand();
			PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			
			//execute the command & return the results
			object retval = cmd.ExecuteScalar();
			
			// detach the SqlParameters from the command object, so they can be used again.
			cmd.Parameters.Clear();
			return retval;
		}        

		#endregion ExecuteScalar	

        #region ExecuteXmlReader

        /// <summary>
        /// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the provided SQLiteConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">a valid SQLiteConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SQLiteConnection connection, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(connection, commandType, commandText, (SQLiteParameter[])null);
        }

        /// <summary>
        /// Execute a SQLiteCommand (that returns a resultset) against the specified SQLiteConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">a valid SQLiteConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SQLiteConnection connection, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, connection, (SQLiteTransaction)null, commandType, commandText, commandParameters);

            // get a data adapter  
            SQLiteDataAdapter da = new SQLiteDataAdapter((SQLiteCommand)cmd);
            DataSet ds = new DataSet();
            // fill the data set, and return the schema information
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds);
            // convert our dataset to XML
            StringReader stream = new StringReader(ds.GetXml());
            // convert our stream of text to an XmlReader
            XmlReader retval = new XmlTextReader(stream);

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;
        }
       
        /// <summary>
        /// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the provided SQLiteTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SQLiteTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SQLiteTransaction transaction, CommandType commandType, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(transaction, commandType, commandText, (SQLiteParameter[])null);
        }

        /// <summary>
        /// Execute a SQLiteCommand (that returns a resultset) against the specified SQLiteTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SQLiteParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SQLiteTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public static XmlReader ExecuteXmlReader(SQLiteTransaction transaction, CommandType commandType, string commandText, params SQLiteParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

            // get a data adapter  
            SQLiteDataAdapter da = new SQLiteDataAdapter((SQLiteCommand)cmd);
            DataSet ds = new DataSet();
            // fill the data set, and return the schema information
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds);
            // convert our dataset to XML
            StringReader stream = new StringReader(ds.GetXml());
            // convert our stream of text to an XmlReader
            XmlReader retval = new XmlTextReader(stream);

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;
        }       

        #endregion ExecuteXmlReader

    }
}