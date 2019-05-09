using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ExceptionLoggerLogic;
using DataAccessLayer;

namespace LogicLayer
{
	public class RealMessageManager : IMessageManager
	{
		private IMessageAccessor dao;
		private AppData.DataStoreType daoType;

		public RealMessageManager(AppData.DataStoreType dataStoreType)
		{
			switch (dataStoreType)
			{
				case AppData.DataStoreType.mock:
					dao = new MessageAccessorMock();
					break;
				case AppData.DataStoreType.msssql:
					dao = new MessageAccessorMSSQL();
					break;
			}

			daoType = dataStoreType;
		}

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Creates a new message in the data store and returns the thread ID of the message created.
		/// Should return a -1 to indicate failure.
		/// </summary>
		/// <param name="message">The new message to be sent.</param>
		/// <returns>The thread that the newly created message generated, or a -1
		/// if the operation was a failure.</returns>
		public int CreateNewMessage(Message message)
		{
			if (null == message)
			{ throw new ArgumentNullException("Message used to create thread cannot be null."); }
			if (!message.IsValid())
			{ throw new ArgumentException("Message is invalid"); }

			int result;

			try
			{
				result = dao.InsertNewMessage(message);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			if (-1 == result)
			{ throw new Exception("Error encountered while inserting a new message to the database."); }

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Creates a reply to a thread.
		/// </summary>
		/// <param name="thread">The thread to reply to.</param>
		/// <param name="message">The message to be sent to the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool CreateNewReply(IMessageThread thread, Message message)
		{
			if (null == message)
			{ throw new ArgumentNullException("Message used to create thread cannot be null."); }
			if (!message.IsValid())
			{ throw new ArgumentException("Message is invalid"); }

			bool result = false;

			try
			{
				dao.InsertNewReply(thread.ThreadID, message);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets the most recent message to be posted to any thread.
		/// </summary>
		/// <param name="thread">Thread to receive the message for.</param>
		/// <returns>The newest message of the thread.</returns>
		public Message RetrieveNewestThreadMessage(IMessageThread thread)
		{
			try
			{
				return dao.SelectNewestThreadMessage(thread.ThreadID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets a collection of messages that belong to a thread, sorting by date.
		/// </summary>
		/// <param name="thread">The thread to retrieve the messages for.</param>
		/// <returns>A list of the messages, ordered by date.</returns>
		public List<Message> RetrieveThreadMessages(IMessageThread thread)
		{
			try
			{
				return dao.SelectMessagesByThreadID(thread.ThreadID).OrderByDescending(m => m.DateTimeSent).ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}
	}
}
