using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ExceptionLoggerLogic;

namespace DataAccessLayer
{
	/// <summary author="Austin Delaney" created="2019/04/25"> 
	/// MSSQL implementation of the <see cref="IMessageAccessor"/> interface for message related
	/// data store operations.
	/// </summary>
	public class MessageAccessorMSSQL : IMessageAccessor
	{
		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Inserts a new message to the datastore, which simultaneously creates a new
		/// thread as well, which it gets the ID of and returns.
		/// </summary>
		/// <param name="message">The message to be added to the datastore</param>
		/// <returns>The newly created thread ID, or -1 for a failure</returns>
		public int InsertNewMessage(Message message)
		{
			if (null == message)
			{ throw new ArgumentNullException("Message being added to database cannot be null."); }
			if (!message.IsValid())
			{ throw new ArgumentException("Message is invalid."); }

			//Return -1 if something goes wrong.
			int newThreadID = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_new_message";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@SenderEmail", message.SenderEmail);
			cmd.Parameters.AddWithValue("@DateTimeSent", message.DateTimeSent);
			cmd.Parameters.AddWithValue("@Subject", message.Subject);
			cmd.Parameters.AddWithValue("@Body", message.Body);

			try
			{
				conn.Open();

				newThreadID = (int) cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return newThreadID;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Inserts a new message as a reply to an existing thread.
		/// </summary>
		/// <param name="threadID">The thread that is being replied to.</param>
		/// <param name="message">The new message to be added to the thread.</param>
		/// <returns>If the operation was a success or failure.</returns>
		public bool InsertNewReply(int threadID, Message message)
		{
			if (null == message)
			{ throw new ArgumentNullException("Message being added to database cannot be null."); }
			if (!message.IsValid())
			{ throw new ArgumentException("Message is invalid."); }

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_new_reply";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@SenderEmail", message.SenderEmail);
			cmd.Parameters.AddWithValue("@DateTimeSent", message.DateTimeSent);
			cmd.Parameters.AddWithValue("@Subject", message.Subject);
			cmd.Parameters.AddWithValue("@Body", message.Body);

			//Return -1 if something goes wrong.
			int result = -1;

			try
			{
				conn.Open();

				result = (int) cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			throw new NotImplementedException();
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Requests messages from the data store that are a part of a specified
		/// thread, requesting either all messages or just messages that are not
		/// archived.
		/// </summary>
		/// <param name="threadID">The thread that the messages belong to.</param>
		/// <returns>A list of the messages that belong to the thread, ordered by date
		/// from newest to oldest.</returns>
		public List<Message> SelectMessagesByThreadID(int threadID)
		{
			List<Message> result = new List<Message>();

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_get_thread_messages";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			try
			{
				conn.Open();

				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					while(reader.Read())
					{
						result.Add(new Message
						{
							MessageID = reader.GetInt32(0),
							SenderEmail = reader.GetString(1),
							DateTimeSent = reader.GetDateTime(2),
							Subject = reader.GetString(3),
							Body = reader.GetString(4)
						});
					}
				}
				else
				{
					ExceptionLogManager.getInstance().LogException(
						new ApplicationException("No messages found in database for thread " + threadID));
				}
			}
			catch(Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return result.OrderByDescending(m => m.DateTimeSent).ToList();
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets the most recent message to be posted to the specified thread.
		/// </summary>
		/// <param name="threadID">The target thread.</param>
		/// <returns>The newest message of the thread.</returns>
		public Message SelectNewestThreadMessage(int threadID)
		{
			var conn = DBConnection.GetDbConnection();
			var procedure = @"";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure};

			cmd.Parameters.AddWithValue("@ThreadID", threadID);

			Message result = new Message();

			try
			{
				conn.Open();

				var reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					reader.Read();

					result = new Message
					{
						MessageID = reader.GetInt32(0),
						SenderEmail = reader.GetString(1),
						DateTimeSent = reader.GetDateTime(2),
						Subject = reader.GetString(3),
						Body = reader.GetString(4)
					};
				}
				else
				{
					ExceptionLogManager.getInstance().LogException(
						new ApplicationException("No messages found in database for thread " + threadID));
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return result;
		}
	}
}