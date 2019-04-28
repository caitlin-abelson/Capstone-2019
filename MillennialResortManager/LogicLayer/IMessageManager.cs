using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Austin Delaney" created="2019/04/03">
	/// Interface created for the specification of a manager class responsible for functionality related
	/// to the DataObjects.Message object.
	/// </summary>
	public interface IMessageManager
	{
		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Gets a collection of messages that belong to a thread, sorting by date.
		/// </summary>
		/// <param name="thread">The thread to retrieve the messages for.</param>
		/// <returns>A list of the messages, ordered by date.</returns>
		List<Message> RetrieveThreadMessages(IMessageThread thread);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Creates a new message in the data store and returns the thread ID of the message created.
		/// Should return a -1 to indicate failure.
		/// </summary>
		/// <param name="message">The new message to be sent.</param>
		/// <returns>The thread that the newly created message generated, or a -1
		/// if the operation was a failure.</returns>
		int CreateNewMessage(Message message);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Creates a reply to a thread.
		/// </summary>
		/// <param name="thread">The thread to reply to.</param>
		/// <param name="message">The message to be sent to the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool CreateNewReply(IMessageThread thread, Message message);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Gets the most recent message to be posted to any thread.
		/// </summary>
		/// <param name="thread">Thread to receive the message for.</param>
		/// <returns>The newest message of the thread.</returns>
		Message RetrieveNewestThreadMessage(IMessageThread thread);
	}
}