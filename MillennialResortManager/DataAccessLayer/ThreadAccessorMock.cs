using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
	/// <summary author="Austin Delaney" created="2019/04/15">
	/// Mock implementation of the IThreadAccessor which does simulate data consistency throughout operations. Is inconsistent with
	/// the database implementation because from this class there is currently no way to gather employee emails from Department or
	/// Role objects.
	/// </summary>
	public class ThreadAccessorMock : IThreadAccessor
	{
		internal static List<UserThread> mockThreadRepo;

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Looks for the thread in the mock repo and then appends a new department to that threads participants.
		/// </summary>
		/// <param name="threadID">Thread ID to look for in mock repo.</param>
		/// <param name="deptID">Name to use to add to the thread.</param>
		/// <returns>If the operation is successful.</returns>
		public bool AddDepartmentThreadParticipant(int threadID, string deptID)
		{
			var threads = FindThread(threadID).ToList();
			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].ParticipantsWithAlias[(new Department { DepartmentID = deptID })] = deptID;
			}
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Looks for the thread in the mock repo and then appends a new employee to the participants.
		/// </summary>
		/// <param name="threadID">Thread ID to look for in mock repo.</param>
		/// <param name="employeeEmail">Employee to add.</param>
		/// <returns>If the operation is successful.</returns>
		public bool AddEmployeeThreadParticipant(int threadID, string employeeEmail)
		{
			var threads = FindThread(threadID).ToList();
			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].ParticipantsWithAlias[(new Employee { Email = employeeEmail })] = employeeEmail;
			}
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Looks for the thread in the mock repo then appends a new guest to the participants.
		/// </summary>
		/// <param name="threadID">Thread to look for.</param>
		/// <param name="guestEmail">Guest to add.</param>
		/// <returns>If the operation is successful.</returns>
		public bool AddGuestThreadParticipant(int threadID, string guestEmail)
		{
			var threads = FindThread(threadID).ToList();
			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].ParticipantsWithAlias[(new Guest { Email = guestEmail })] = guestEmail;
			}
			return true;
		}

		/// <summary author="Austin Delaney" Created="2019/04/15">
		/// Looks for the thread in the mock repo then appends a new member to the participants.
		/// </summary>
		/// <param name="threadID">Thread to look for.</param>
		/// <param name="memberEmail">Member to add.</param>
		/// <returns>If the operation is successful.</returns>
		public bool AddMemberThreadParticipant(int threadID, string memberEmail)
		{
			var threads = FindThread(threadID).ToList();
			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].ParticipantsWithAlias[(new Member { Email = memberEmail })] = memberEmail;
			}
			return true;
		}

		/// <summary author="Austin Delaney" Created="2019/04/15">
		/// Looks for the thread in the mock repo then appends a new role to the participants.
		/// </summary>
		/// <param name="threadID"></param>
		/// <param name="roleID"></param>
		/// <returns>If the operation is successful.</returns>
		public bool AddRoleThreadParticipant(int threadID, string roleID)
		{
			var threads = FindThread(threadID).ToList();
			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].ParticipantsWithAlias[(new Role { RoleID = roleID })] = roleID;
			}
			return true;
		}

		/// <summary author="Austin Delaney" Created="2019/04/15">
		/// Gets a list of the participants of a thread, returning their emails and ID's
		/// </summary>
		/// <param name="threadID">Thread to retrieve participants of.</param>
		/// <returns>Emails and ID's of involved participants.</returns>
		public Dictionary<string, string> GetThreadParticipantEmailAndAlias(int threadID)
		{
			return FindThread(threadID).First().ParticipantsWithAlias.ToDictionary(y => y.Key.Alias, v => v.Value);
		}

		/// <summary author="Austin Delaney" Created="2019/04/15">
		/// Returns a UserThread object that matches the requested ID and userName is listed as a participant in the thread.
		/// If no such object previously exists, it will create one with default values and add it to the repo.
		/// </summary>
		/// <param name="threadID">Thread to retrieve.</param>
		/// <param name="userName">Name of the user viewing it.</param>
		/// <returns>Full UserThread object of the thread as viewed by the user.</returns>
		public UserThread GetUserThreadData(int threadID, string userName)
		{
			var thread = FindThread(threadID).FirstOrDefault(t => t.Alias == userName);

			if (thread == null)
			{
				thread = FindThread(threadID).First(u => GetThreadParticipantEmailAndAlias(u.ThreadID).Select(kp => kp.Key).Contains(userName));

				thread = new UserThread
				{
					ThreadID = thread.ThreadID,
					ParticipantsWithAlias = thread.ParticipantsWithAlias,
					Messages = thread.Messages,
					Alias = userName,
					HasNewMessages = true,
					Archived = thread.Archived,
					Hidden = false,
					Silenced = false
				};

				mockThreadRepo.Add(thread);
			}

			return thread;
		}

		/// <summary author="Austin Delaney" Created="2019/04/18">
		/// Goes to the thread repo and gets all the threads that a user is involved in.
		/// </summary>
		/// <param name="userName">The user to get the views for.</param>
		/// <param name="showArchived">Whether to reveal archived threads or not.</param>
		/// <returns>List of UserThreadView objects.</returns>
		public List<UserThreadView> GetUserThreadViews(string userName, bool showArchived)
		{
			var threads = mockThreadRepo.Where(t => GetThreadParticipantEmailAndAlias(t.ThreadID).Select(kp => kp.Key).Contains(userName));

			List<UserThreadView> output = new List<UserThreadView>();

			foreach (var thread in threads)
			{
				//Skip this item if it is archived and we aren't showing archived items
				if (thread.Archived && !showArchived)
				{ continue; }

				var firstMessage = thread.Messages.OrderByDescending(m => m.DateTimeSent).First();

				output.Add(
					new UserThreadView
					{
						Alias = userName,
						NewestMessage = thread.NewestMessage,
						Archived = thread.Archived,
						//If the user getting the view
						ThreadHidden = (thread.Alias == userName) ? thread.Hidden : false,
						HasNewMessages = (thread.Alias == userName) ? thread.HasNewMessages : true,
						ThreadOwner = firstMessage.SenderAlias,
						OpeningSubject = firstMessage.Subject
					});
			}

			return output;
		}

		/// <summary author="Austin Delaney" Created="2019/04/18">
		/// Returns if a thread has new messages for a user. Will return true if the only threads available in the repo list the
		/// user as a participant.
		/// </summary>
		/// <param name="threadID">The thread to look for.</param>
		/// <param name="userName">The user viewing the thread.</param>
		/// <returns>If there are new messages.</returns>
		public bool ReadThreadNewMessages(int threadID, string userName)
		{
			bool output = true;

			var threads = FindThread(threadID);

			if (threads.Where(t => t.Alias == userName).Count() == 1)
			{
				output = threads.First().HasNewMessages;
			}

			return output;
		}

		/// <summary author="Austin Delaney" Created="2019/04/18">
		/// Removes UserThreads that belong to a particular user and removes that user from the participant list
		/// of all remaining UserThreads.
		/// </summary>
		/// <param name="threadID">Thread to modify.</param>
		/// <param name="userName">Participant to remove.</param>
		/// <returns>If the operation is successful.</returns>
		public bool RemoveThreadParticipant(int threadID, string userName)
		{
			bool success = true;

			mockThreadRepo.RemoveAll(t => t.Alias == userName);

			var threads = FindThread(threadID).ToList();

			for (int i = 0; i < threads.Count; i++)
			{
							
			}

			return success;
		}

		/// <summary author="Austin Delaney" created="2018/04/18">
		/// Sets all instances of a thread in the repo to a passed in archived status.
		/// </summary>
		/// <param name="threadID">The id of the thread.</param>
		/// <param name="archived">The new status of the threads.</param>
		/// <returns>If the operation is successful.</returns>
		public bool UpdateThreadArchivedStatus(int threadID, bool archived)
		{
			var threads = FindThread(threadID).ToList();

			for (int i = 0; i < threads.Count; i++)
			{
				threads[i].Archived = archived;
			}

			return true;
		}

		/// <summary author="Austin Delaney" created="2018/04/18">
		/// Looks for a thread in the repo that belongs to a certain user and changes that thread hidden
		/// status to a passed in param.
		/// </summary>
		/// <param name="threadID">The id of the thread.</param>
		/// <param name="status">The new hidden status.</param>
		/// <param name="userName">The name of the user.</param>
		/// <returns>If the operation is successful.</returns>
		public bool UpdateThreadHiddenStatus(int threadID, bool status, string userName)
		{
			var thread = FindThread(threadID).FirstOrDefault(t => t.Alias == userName);

			if (null == thread)
			{
				thread = GetUserThreadData(threadID, userName);
			}

			thread.Hidden = status;

			return true;
		}

		/// <summary author="Austin Delaney" created="2018/04/18">
		/// Looks for a thread in the repo that belongs to a certain user and changes that thread status to "Read".
		/// </summary>
		/// <param name="threadID">The id of the thread.</param>
		/// <param name="userName">The name of the user.</param>
		/// <returns>If the operation is successful.</returns>
		public bool UpdateThreadParticipantNewMessageStatus(int threadID, string userName)
		{
			var thread = FindThread(threadID).FirstOrDefault(t => t.Alias == userName);

			if (null == thread)
			{
				thread = GetUserThreadData(threadID, userName);
			}

			thread.HasNewMessages = false;

			return true;
		}

		/// <summary author="Austin Delaney" created="2018/04/18">
		/// Looks for a thread in the repo that belongs to a certain user and changes that thread notification status
		/// to a passed in param.
		/// </summary>
		/// <param name="threadID">The id of the thread.</param>
		/// <param name="status">The new notification status.</param>
		/// <param name="userName">The name of the user.</param>
		/// <returns>If the operation is successful.</returns>
		public bool UpdateThreadSilentStatus(int threadID, bool status, string userName)
		{
			var thread = FindThread(threadID).FirstOrDefault(t => t.Alias == userName);

			if (null == thread)
			{
				thread = GetUserThreadData(threadID, userName);
			}

			thread.Silenced = status;

			return true;
		}

		/// <summary author="Austin Delaney" created="2018/04/18">
		/// Changes any reference to a users alias to a different alias in all copies of a thread in
		/// the repo.
		/// </summary>
		/// <param name="threadID">The id of the thread.</param>
		/// <param name="userName">The presumed old alias.</param>
		/// <param name="newAlias">The new alias.</param>
		/// <returns>If the operation is successful.</returns>
		public bool UpdateUserThreadAlias(int threadID, string userName, string newAlias)
		{
			//Get all of the matching threads.
			var threads = FindThread(threadID).ToList();

			//Loop through each thread and just remove the old instance
			for (int i = 0; i < threads.Count; i++)
			{
				if (threads[i].Alias == userName)
				{
					threads[i].Alias = newAlias;
				}

				foreach (var item in threads[i].ParticipantsWithAlias.Select(kp => kp.Key))
				{
					if (userName == threads[i].ParticipantsWithAlias[item])
					{
						threads[i].ParticipantsWithAlias.Remove(item);
					}
				}
			}

			//Add the new instance of alias back to the thread
			AddRoleThreadParticipant(threadID, newAlias);

			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Gets a thread and throws error if it can't or gets too many.
		/// </summary>
		/// <param name="threadID">Thread to look for.</param>
		/// <returns>Thread.</returns>
		private IEnumerable<UserThread> FindThread(int threadID)
		{
			var thread = mockThreadRepo.Where(t => t.ThreadID == threadID);
			if (thread.Count() == 0)
			{
				throw new InvalidOperationException("Thread " + threadID + " not found in mock thread repository.");
			}
			else
			{
				return thread;
			}
		}
	}
}