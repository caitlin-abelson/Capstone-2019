using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Austin Delaney" created="2019/04/03">
	/// Interface created for the specification of a manager class responsible for functionality related
	/// to the DataObjects.UserThread and DataObjects.UserThreadView objects.
	/// </summary>
	public interface IThreadManager
	{
		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Creates a new thread with a supplied message and then a supplied set of recipients, who will become the thread participants.
		/// Note that if a recipient is included by role/dept as well as by their specific username, the message will ignore their role/dept.
		/// </summary>
		/// <param name="message">The message used to initialize the thread.</param>
		/// <param name="recipients">The set of recipients to be included in the new thread.</param>
		/// <returns>The user specific thread view of the thread.</returns>
		UserThread CreateNewThread(Message message, List<IMessagable> recipients);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Gets a fully populated user specific view of a particular thread from the data store.
		/// </summary>
		/// <param name="thread">The thread to populate</param>
		/// <param name="user">The user who is viewing the thread.</param>
		/// <returns>The fully populated thread object representing the requested thread.</returns>
		UserThread GetUserThread(IMessageThread thread, ISender user);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Gets a fully populated user specific view of a particular thread from the data store
		/// by searching for its ID.
		/// </summary>
		/// <param name="threadID">The ID of the thread to get data on.</param>
		/// <param name="user">The user who is viewing the thread.</param>
		/// <returns>The fully populated thread object representing the requested thread.</returns>
		UserThread GetUserThread(int threadID, ISender user);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Gets a list of all user threads that a user is a participant in.
		/// </summary>
		/// <updates>
		/// <update author="Austin Delaney" date="2019/04/11">
		/// Added showArchived param, so the list can prematurely filter archived threads and save on
		/// data being transferred.
		/// </update>
		/// </updates>
		/// <param name="user">The user/participant to find threads for.</param>
		/// <param name="showArchived">Include archived threads in the retrieval from the data store.</param>
		/// <returns>A list of compact thread objects</returns>
		List<UserThreadView> GetUserThreadViewList(ISender user, bool showArchived);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Adds a list of participants to a thread.
		/// </summary>
		/// <param name="thread">The thread to add the participants to.</param>
		/// <param name="newParticipants">The list of participants to add to the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool AddThreadParticipants(IMessageThread thread, List<IMessagable> newParticipants);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Removes a set of participants from a thread. Will return true if after the operation,
		/// all participants that were asked to be removed are no longer listed in the thread. This
		/// should succeed even if the listed participants were not in the thread to begin with.
		/// When removing a role/dept, will remove by alias, and if a user was added that has since
		/// changed their alias they will not be removed.
		/// </summary>
		/// <param name="thread">The thread to remove participants from.</param>
		/// <param name="removeParticipants">The set of participants to remove from the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool RemoveThreadParticipants(IMessageThread thread, List<ISender> removeParticipants);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Replies to a thread by creating a new message within that thread.
		/// </summary>
		/// <param name="thread">The thread to add the message to.</param>
		/// <param name="reply">The message to add to the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool ReplyToThread(IMessageThread thread, Message reply);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Changes a user's visibile alias within a thread. Method should check if the alias
		/// is already set to <paramref name="newAlias"/>, and if it is, should return and not
		/// fail.
		/// </summary>
		/// <param name="thread">The thread to alter the alias in.</param>
		/// <param name="newAlias">The new alias the user would like to use for this particular thread.</param>
		/// <param name="participant">The user who is changing their alias.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadAlias(IMessageThread thread, string newAlias, ISender participant);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Changes the visibility level of a thread to that user.
		/// </summary>
		/// <param name="thread">The thread to change the status on.</param>
		/// <param name="status">Hidden or unhidden.</param>
		/// <param name="participant">The user who would like to have the thread hidden for them.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadHiddenStatus(IMessageThread thread, bool status, ISender participant);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Causes a thread not to notify a particular user of new messages to that thread.
		/// </summary>
		/// <param name="thread">The thread to change the notify status on.</param>
		/// <param name="status">The new status of the thread.</param>
		/// <param name="participant">The user for whom the status should change.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadSilentStatus(IMessageThread thread, bool status, ISender participant);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Checks if a thread has any new messages for the user in the data store.
		/// </summary>
		/// <param name="thread">The thread to check for new messages.</param>
		/// <param name="user">The user to check new messages for.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool ReadThreadNewMessages(IMessageThread thread, ISender user);

		/// <summary author="Austin Delaney" created="2019/04/03">
		/// Sets the archived status of a thread in the data store.
		/// </summary>
		/// <param name="thread">The thread to be set as archived.</param>
		/// <param name="archived">What the status of the thread should be.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadArchivedStatus(IMessageThread thread, bool archived);

		/// <summary author="Austin Delaney" created="2019/04/12">
		/// Marks a thread as read for a certain participant.
		/// </summary>
		/// <param name="thread">The thread that is being read.</param>
		/// <param name="participant">The person that is reading/opening the thread.</param>
		/// <returns>If the change was successfully made to the data store.</returns>
		bool MarkThreadAsReadByParticipant(IMessageThread thread, ISender participant);
	}
}