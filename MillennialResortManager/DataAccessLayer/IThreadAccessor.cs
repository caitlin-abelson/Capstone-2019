using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
	/// <summary author="Austin Delaney" created="2019/04/07">
	/// Specifies the data accessor methods for the UserThread object/class family.
	/// </summary>
	public interface IThreadAccessor
	{
		/// <summary author="Austin Delaney" created="2019/04/07">
		/// Retrieves a list of objects to hold small bits of easily readable thread data
		/// from the data store.
		/// </summary>
		/// <updates>
		/// <update author="Austin Delaney" date="2019/04/11">
		/// Added showArchived param, so the list can prematurely filter archived threads and save on
		/// data being transferred.
		/// </update>
		/// </updates>
		/// <param name="email">The email of the participant to whom the threads relate to.</param>
		/// <param name="showArchived">Include archived threads in the retrieval from the data store.</param>
		/// <returns>A list of compact easily human readable UserThreadView objects.</returns>
		List<UserThreadView> GetUserThreadViews(string email, bool showArchived);

		/// <summary author="Austin Delaney" created="2019/04/07">
		/// Gets the details of a thread as seen by a particular user
		/// </summary>
		/// <param name="threadID">The ID of the thread to get the details on.</param>
		/// <param name="email">The name of the user who will be viewing the thread.</param>
		/// <returns>A UserThread obj, devoid of messages.</returns>
		UserThread GetUserThreadData(int threadID, string email);

		/// <summary author="Austin Delaney" created="2019/04/07">
		/// Changes the visibility level of a thread to that user. Method should check if the
		/// visibility of the thread is already set to the <paramref name="status"/>, and if
		/// it is, should not fail.
		/// </summary>
		/// <param name="threadID">The thread to change the status on.</param>
		/// <param name="status">Hidden or unhidden.</param>
		/// <param name="email">The user who would like to have the thread hidden for them.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadHiddenStatus(int threadID, bool status, string email);

		/// <summary author="Austin Delaney" created="2019/04/07">
		/// Causes a threads notification of a particular user of new messages to that thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread to change the notify status on.</param>
		/// <param name="status">The new status of the thread.</param>
		/// <param name="email">The name of the user for whom the status should change.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadSilentStatus(int threadID, bool status, string email);

		/// <summary author="Austin Delaney" created="2019/04/07">
		/// Checks if a thread has any new messages for the user in the data store.
		/// </summary>
		/// <notes>
		/// <notes author="Austin Delaney" date="2019/04/25">Low priority, pending removal.</notes>
		/// </notes>
		/// <param name="threadID">The id of the thread to check for new messages.</param>
		/// <param name="email">The user to check new messages for.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool ReadThreadNewMessages(int threadID, string email);

		/// <summary author="Austin Delaney" created="2019/04/07">
		/// Sets the status of a thread to archived in the data store.
		/// </summary>
		/// <param name="threadID">The ID of the thread to be set as archived.</param>
		/// <param name="archived">What the status of the thread should be.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		bool UpdateThreadArchivedStatus(int threadID, bool archived);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Sets a users thread alias to a chosen value.
		/// </summary>
		/// <param name="threadID">The ID of the thread to change the alias for.</param>
		/// <param name="email">The email of the user changing their alias.</param>
		/// <param name="newAlias">The new alias for the user.</param>
		/// <returns>If the operation was a success or not.</returns>
		bool UpdateUserThreadAlias(int threadID, string email, string newAlias);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Gets the emails and aliases of the participants involved in a thread.
		/// </summary>
		/// <updates>
		/// <update author="Austin Delaney" date="2019/04/24">
		/// Added the alias return.
		/// </update>
		/// </updates>
		/// <param name="threadID">The ID of the thread to get the participant list for.</param>
		/// <returns>All the thread participants with the email as the key and the alias as the value</returns>
		Dictionary<string, string> GetThreadParticipantEmailAndAlias(int threadID);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Adds a new member to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the member is participating in.</param>
		/// <param name="memberEmail">The email of the member</param>
		/// <returns>If the operation was a success or not.</returns>
		bool AddMemberThreadParticipant(int threadID, string memberEmail);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Adds a new guest to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the guest is participating in.</param>
		/// <param name="guestEmail">The email of the guest</param>
		/// <returns>If the operation was a success or not.</returns>
		bool AddGuestThreadParticipant(int threadID, string guestEmail);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Adds a new employee to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the employee is participating in.</param>
		/// <param name="employeeEmail">The email of the employee</param>
		/// <returns>If the operation was a success or not.</returns>
		bool AddEmployeeThreadParticipant(int threadID, string employeeEmail);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Adds a every emloyee with a role to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the employees are participating in.</param>
		/// <param name="roleID">The ID of the role</param>
		/// <returns>If the operation was a success or not.</returns>
		bool AddRoleThreadParticipant(int threadID, string roleID);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Adds a every emloyee in a department to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the employees are participating in.</param>
		/// <param name="deptID">The ID of the department</param>
		/// <returns>If the operation was a success or not.</returns>
		bool AddDepartmentThreadParticipant(int threadID, string deptID);

		/// <summary author="Austin Delaney" created="2019/04/11">
		/// Removes a specified user from the thread participants in the data store.
		/// </summary>
		/// <param name="threadID">The ID of the specified thread to remove the user from.</param>
		/// <param name="email">The email of the user being removed.</param>
		/// <returns>If the operation was a success or not.</returns>
		bool RemoveThreadParticipant(int threadID, string email);

		/// <summary author="Austin Delaney" created="2019/04/12">
		/// Changes the status of a thread in the data store to 'no new messages' for a given participant of 
		/// that thread.
		/// </summary>
		/// <param name="threadID">The id of the thread to change.</param>
		/// <param name="email">The email of the participant who is reading the thread.</param>
		/// <returns>Whether the change was successfully made to the data store.</returns>
		bool UpdateThreadParticipantNewMessageStatus(int threadID, string email);
	}
}