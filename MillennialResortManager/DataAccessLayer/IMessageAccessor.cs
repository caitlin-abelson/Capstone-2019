using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
	/// <summary>
	/// Austin Delaney
	/// Created: 2019/04/04
	/// 
	/// Interface created for the specification of the Message Data Accessor.
	/// </summary>
	public interface IMessageAccessor
	{
		/// <summary author="Austin Delaney" created="2019/04/04">
		/// Requests messages from the data store that are a part of a specified
		/// thread, requesting either all messages or just messages that are not
		/// archived.
		/// </summary>
		/// <param name="threadID">The thread that the messages belong to.</param>
		/// <returns>A list of the messages that belong to the thread, ordered by date
		/// from newest to oldest.</returns>
		List<Message> SelectMessagesByThreadID(int threadID);

		/// <summary author="Austin Delaney" created="2019/04/04">
		/// Inserts a new message to the datastore, which simultaneously creates a new
		/// thread as well, which it gets the ID of and returns.
		/// </summary>
		/// <param name="message">The message to be added to the datastore</param>
		/// <returns>The newly created thread ID, or -1 for a failure</returns>
		int InsertNewMessage(Message message);

		/// <summary author="Austin Delaney" created="2019/04/04">
		/// Inserts a new message as a reply to an existing thread.
		/// </summary>
		/// <param name="threadID">The thread that is being replied to.</param>
		/// <param name="message">The new message to be added to the thread.</param>
		/// <returns>If the operation was a success or failure.</returns>
		bool InsertNewReply(int threadID, Message message);

		/// <summary author="Austin Delaney" created="2019/04/04">
		/// Gets the most recent message to be posted to the specified thread.
		/// </summary>
		/// <param name="threadID">The target thread.</param>
		/// <returns>The newest message of the thread.</returns>
		Message SelectNewestThreadMessage(int threadID);
	}
}