using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ExceptionLoggerLogic;

namespace DataAccessLayer
{
	/// <summary author="Austin Delaney" created="2019/04/18">
	/// Mock implementation of the interface <see cref="IMessageAccessor"/> and works together with the
	/// mock repo provided in the <see cref="ThreadAccessorMock"/> member.
	/// </summary>
	public class MessageAccessorMock : IMessageAccessor
	{
		/// <summary author="Austin Delaney" created="2019/04/18">
		/// Creates a new message and a new UserThread with no participants in the 
		/// </summary>
		/// <param name="message">The new first message of the thread.</param>
		/// <returns>The ID of the newly created thread.</returns>
		public int InsertNewMessage(Message message)
		{
			int highestThreadID = ThreadAccessorMock.mockThreadRepo.OrderBy(t => t.ThreadID).Select(t => t.ThreadID).First();
			UserThread newThread = new UserThread
			{
				ThreadID = highestThreadID + 1,
				Messages = new List<Message> { message },
				ParticipantsWithAlias = new List<IMessagable> { message.Sender }.ToDictionary(k => k, k => k.Alias),
				Alias = message.SenderAlias,
				Archived = false,
				HasNewMessages = true,
				Hidden = false,
				Silenced = false
			};

			ThreadAccessorMock.mockThreadRepo.Add(newThread);

			return newThread.ThreadID;
		}

		/// <summary author="Austin Delaney" created="2019/04/18">
		/// Adds a new message to each copy of a thread in the mock thread repo.
		/// </summary>
		/// <param name="threadID">The thread that is being replied to.</param>
		/// <param name="message">The reply.</param>
		/// <returns>If there were actually matching threads to reply to in the repo.</returns>
		public bool InsertNewReply(int threadID, Message message)
		{
			var threads = ThreadAccessorMock.mockThreadRepo.Where(t => t.ThreadID == threadID).ToList();

			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].Messages.Add(message);
			}

			return (threads.Count > 0);
		}

		/// <summary author="Austin Delaney" createdd="2019/04/18">
		/// Gets the first cluster of messages for a matching thread from the repo.
		/// </summary>
		/// <param name="threadID">Thread to get the messages for.</param>
		/// <returns></returns>
		public List<Message> SelectMessagesByThreadID(int threadID)
		{
			try
			{
				return ThreadAccessorMock.mockThreadRepo.FirstOrDefault(t => t.ThreadID == threadID).Messages;
			}
			catch(Exception ex)
			{
				throw new Exception("Unable to locate threads messages in mock repo", ex);
			}
		}

		/// <summary author="Austin Delaney" createdd="2019/04/18">
		/// Gets the most recent message to be posted to the specified thread.
		/// </summary>
		/// <param name="threadID">The target thread.</param>
		/// <returns>The newest message of the thread.</returns>
		public Message SelectNewestThreadMessage(int threadID)
		{
			try
			{
				return ThreadAccessorMock.mockThreadRepo.FirstOrDefault(t => t.ThreadID == threadID).NewestMessage;
			}
			catch(Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				return null;
			}
		}
	}
}