using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	public class RealThreadManager : IThreadManager
	{
		IThreadAccessor dao;
		AppData.DataStoreType daoType;

		/// <summary>
		/// Smart constructor that determines which data accessor class the manager will be calling from.
		/// </summary>
		/// <param name="dataStoreType">Data storage medium.</param>
		public RealThreadManager(AppData.DataStoreType dataStoreType)
		{
			switch (dataStoreType)
			{
				case AppData.DataStoreType.mock:
					dao = new ThreadAccessorMock();
					break;
				case AppData.DataStoreType.msssql:
					break;
			}
			daoType = dataStoreType;
		}

		/// <summary author="Austin Delaney" created="2019/04/20">
		/// Adds a list of participants to a thread.
		/// </summary>
		/// <param name="thread">The thread to add the participants to.</param>
		/// <param name="newParticipants">The list of participants to add to the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool AddThreadParticipants(IMessageThread thread, List<IMessagable> newParticipants)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to add participants to cannot be null."); }
			if (null == newParticipants)
			{ return true; }

			int remainingItems = newParticipants.Count;

			foreach (var participant in newParticipants)
			{
				try
				{
					bool success;

					if (participant is Role)
					{
						success = dao.AddRoleThreadParticipant(thread.ThreadID, participant.Alias);
					} else if (participant is Department)
					{
						success = dao.AddDepartmentThreadParticipant(thread.ThreadID, participant.Alias);
					} else if (participant is Employee)
					{
						success = dao.AddEmployeeThreadParticipant(thread.ThreadID, participant.Alias);
					} else if (participant is Guest)
					{
						success = dao.AddGuestThreadParticipant(thread.ThreadID, participant.Alias);
					} else if (participant is Member)
					{
						success = dao.AddMemberThreadParticipant(thread.ThreadID, participant.Alias);
					} else
					{
						success = false;
						ExceptionLogManager.getInstance().LogException(
							new NotSupportedException("Participant alias: " + participant.Alias + ". Unsupported participant type of " + participant.GetType().Name));
					}
					if (success) { remainingItems--; }
				}
				catch (Exception ex)
				{
					Exception exc = new Exception("Error encountered for participant " + participant.Alias + " during participant " +
						"add operation", ex);
					ExceptionLogManager.getInstance().LogException(exc);
				}
			}

			return (0 == remainingItems);
		}

		/// <summary author="Austin Delaney" created="2019/04/20">
		/// Creates a new thread with a supplied message and then a supplied set of recipients, who will become the thread participants.
		/// Note that if a recipient is included by role/dept as well as by their specific username, the message will ignore their role/dept.
		/// </summary>
		/// <param name="message">The message used to initialize the thread.</param>
		/// <param name="recipients">The set of recipients to be included in the new thread.</param>
		/// <returns>The user specific thread view of the thread.</returns>
		public UserThread CreateNewThread(Message message, List<IMessagable> recipients)
		{
			if (null == message)
			{ throw new ArgumentNullException("Message cannot be null"); }

			if(null == recipients)
			{ recipients = new List<IMessagable>(); }

			if (!message.IsValid())
			{ throw new ArgumentException("Message is invalid, thread cannot be created."); }

			recipients.Add(message.Sender);

			int newThreadID;

			try
			{
				RealMessageManager messageManager = new RealMessageManager(daoType);
				newThreadID = messageManager.CreateNewMessage(message);
			}
			catch(Exception ex)
			{
				Exception exception =
					new Exception("Error while creating a new thread using supplied message", ex);
				ExceptionLogManager.getInstance().LogException(exception);
				throw exception;
			}

			UserThread newThread = new UserThread { ThreadID = newThreadID };

			try
			{
				AddThreadParticipants(newThread, recipients);
			}
			catch (Exception ex)
			{
				Exception exception =
					new Exception("Error while adding recipients to new thread", ex);
				ExceptionLogManager.getInstance().LogException(exception);
			}

			return GetUserThread(newThread, message.Sender);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets a fully populated user specific view of a particular thread from the data store.
		/// </summary>
		/// <param name="thread">The thread to populate</param>
		/// <param name="user">The user who is viewing the thread.</param>
		/// <returns>The fully populated thread object representing the requested thread.</returns>
		public UserThread GetUserThread(IMessageThread thread, ISender user)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to retrieve cannot be null."); }
			if (null == user)
			{ throw new ArgumentNullException("A user must be provided that is viewing the thread, cannot be null."); }

			return GetUserThread(thread.ThreadID, user);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets a fully populated user specific view of a particular thread from the data store
		/// by searching for its ID.
		/// </summary>
		/// <param name="threadID">The ID of the thread to get data on.</param>
		/// <param name="user">The user who is viewing the thread.</param>
		/// <returns>The fully populated thread object representing the requested thread.</returns>
		public UserThread GetUserThread(int threadID, ISender user)
		{
			if (null == user)
			{ throw new ArgumentNullException("A user must be provided that is viewing the thread, cannot be null."); }

			UserThread result;

			try
			{
				result = dao.GetUserThreadData(threadID, user.Email);
			}
			catch (Exception ex)
			{
				Exception exc = new Exception("Error occurred retrieving thread " + threadID + " data for user " + user.Email, ex);
				ExceptionLogManager.getInstance().LogException(exc);
				throw exc;
			}

			//Set the participants of the thread.
			Dictionary<IMessagable, string> participantsWithAlias = new Dictionary<IMessagable, string>();

			//TRY and guess what this does
			try
			{
				//Get all the emails and aliases of the participants first
				Dictionary<string, string> emailsWithAlias = dao.GetThreadParticipantEmailAndAlias(threadID);

				//Setup the managers to get the properly formed data objects that those emails represent
				EmployeeManager employeeManager = new EmployeeManager();
				GuestManager guestManager = new GuestManager();
				MemberManagerMSSQL memberManager = new MemberManagerMSSQL();

				//Heres the tricky part
				//Using the list of all your emails, get an IEnumerable of all the objects that are found to match those emails
				//Why is this so ugly? It's not. Your eyes simply can't appreciate it's beauty.
				IEnumerable<ISender> foundParticipants = new List<ISender>().Concat(
					employeeManager.SelectAllEmployees().Where(e => emailsWithAlias.Select(kp => kp.Key).Contains(e.Email))).Concat(
					guestManager.ReadAllGuests().Where(g => emailsWithAlias.Select(kp => kp.Key).Contains(g.Email))).Concat(
					memberManager.RetrieveAllMembers().Where(m => emailsWithAlias.Select(kp => kp.Key).Contains(m.Email)));

				//Using the list of objects you obtained, cast those objects to an imessagable and assign them the value that is found for their email
				foreach (var participantObject in foundParticipants)
				{
					participantsWithAlias[participantObject as IMessagable] = emailsWithAlias[participantObject.Email];
				}
			}
			catch (Exception ex)
			{
				Exception exc = new Exception("Error while populating participants for thread " + threadID, ex);
				ExceptionLogManager.getInstance().LogException(exc);
			}

			result.ParticipantsWithAlias = participantsWithAlias;

			//Set the messages of the thread.
			IEnumerable<Message> messages = new List<Message>();

			//Try to properly retrieve them from data store. If unsuccessful, log exception and set 
			//thread messages to an empty list
			try
			{
				RealMessageManager manager = new RealMessageManager(daoType);

				messages = manager.RetrieveThreadMessages(new UserThread { ThreadID = threadID });
			}
			catch (Exception ex)
			{
				Exception exc = new Exception("Unable to retrieve messages for thread " + threadID, ex);
				ExceptionLogManager.getInstance().LogException(exc);
			}

			result.Messages = messages.ToList();

			//Return the (hopeuflly) fully formed UserThread object
			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets a list of all user threads that a user is a participant in.
		/// </summary>
		/// <param name="user">The user/participant to find threads for.</param>
		/// <param name="showArchived">Include archived threads in the retrieval from the data store.</param>
		/// <returns>A list of compact thread objects</returns>
		public List<UserThreadView> GetUserThreadViewList(ISender user, bool showArchived)
		{
			if (null == user)
			{ throw new ArgumentNullException("A user must be provided that is viewing the threads, cannot be null."); }

			List<UserThreadView> result = null;

			try
			{
				result = dao.GetUserThreadViews(user.Email, showArchived);
			}
			catch (Exception ex)
			{
				Exception exc = new Exception("Error while retrieving UserThreadView list for user: " + user.Email, ex);
				ExceptionLogManager.getInstance().LogException(exc);
				throw exc;
			}

			IMessageManager messageManager = new RealMessageManager(daoType);

			for (int i = 0; i < result.Count; i++)
			{
				try
				{
					result[i].NewestMessage = messageManager.RetrieveNewestThreadMessage(result[i]);
				}
				catch (Exception ex)
				{
					Exception exc = new Exception("Error while retrieving newest message for user: " + user.Email + " in thread " + result[i].ThreadID, ex);
					ExceptionLogManager.getInstance().LogException(exc);
				}
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Marks a thread as read for a certain participant.
		/// </summary>
		/// <param name="thread">The thread that is being read.</param>
		/// <param name="participant">The person that is reading/opening the thread.</param>
		/// <returns>If the change was successfully made to the data store.</returns>
		public bool MarkThreadAsReadByParticipant(IMessageThread thread, ISender participant)
		{
			if (null == thread)
			{ throw new ArgumentNullException("A thread must be provided to be read by a participant, cannot be null."); }
			if (null == participant)
			{ throw new ArgumentNullException("A participant must be provided that is reading the thread, cannot be null."); }

			bool result = false;

			try
			{
				result = dao.UpdateThreadParticipantNewMessageStatus(thread.ThreadID, participant.Email);				
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Removes a set of participants from a thread. Will return true if after the operation,
		/// all participants that were asked to be removed are no longer listed in the thread. This
		/// should succeed even if the listed participants were not in the thread to begin with.
		/// When removing a role/dept, will remove by alias, and if a user was added that has since
		/// changed their alias they will not be removed.
		/// </summary>
		/// <param name="thread">The thread to remove participants from.</param>
		/// <param name="removeParticipants">The set of participants to remove from the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool RemoveThreadParticipants(IMessageThread thread, List<ISender> removeParticipants)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to add participants to cannot be null."); }
			if (null == removeParticipants)
			{ removeParticipants = new List<ISender>(); }
			
			bool result = true;

			foreach (var participant in removeParticipants)
			{
				try
				{
					if (!dao.RemoveThreadParticipant(thread.ThreadID, participant.Email))
					{
						result = false;
					}
				}
				catch(Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(new Exception("Error encountered removing user " + participant.Email
						+ " from thread " + thread.ThreadID, ex));
					result = false;
				}
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Replies to a thread by passing the message manager a new message with that thread's ID.
		/// </summary>
		/// <param name="thread">The thread to add the message to.</param>
		/// <param name="reply">The message to add to the thread.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool ReplyToThread(IMessageThread thread, Message reply)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to add participants to cannot be null."); }
			if (null == reply)
			{ throw new ArgumentNullException("Message to use as reply cannot be null."); }

			bool result = false;
			IMessageManager messageManager = new RealMessageManager(daoType);

			try
			{
				result = messageManager.CreateNewReply(thread, reply);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Changes a user's visibile alias within a thread. Method should check if the alias
		/// is already set to <paramref name="newAlias"/>, and if it is, should return and not
		/// fail.
		/// </summary>
		/// <param name="thread">The thread to alter the alias in.</param>
		/// <param name="newAlias">The new alias the user would like to use for this particular thread.</param>
		/// <param name="participant">The user who is changing their alias.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadAlias(IMessageThread thread, string newAlias, ISender participant)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to set the alias for to cannot be null."); }
			if (null == participant)
			{ throw new ArgumentNullException("Participant to set the alias for cannot be null."); }
			if (!newAlias.IsValidMessengerAlias())
			{ throw new ArgumentException("New alias is invalid."); }
			if (null == newAlias)
			{ return true; }

			bool result = false;

			try
			{
				result = dao.UpdateUserThreadAlias(thread.ThreadID, participant.Email, newAlias);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Sets the archived status of a thread in the data store.
		/// </summary>
		/// <param name="thread">The thread to be set as archived.</param>
		/// <param name="archived">What the status of the thread should be.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadArchivedStatus(IMessageThread thread, bool archived)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to set the archived status for cannot be null."); }

			bool result = false;

			try
			{
				result = dao.UpdateThreadArchivedStatus(thread.ThreadID, archived);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Changes the visibility level of a thread to that user.
		/// </summary>
		/// <param name="thread">The thread to change the status on.</param>
		/// <param name="status">Hidden or unhidden.</param>
		/// <param name="participant">The user who would like to have the thread hidden for them.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadHiddenStatus(IMessageThread thread, bool status, ISender participant)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to set the hidden status for to cannot be null."); }
			if (null == participant)
			{ throw new ArgumentNullException("Participant to set the hidden status for cannot be null."); }

			bool result = false;

			try
			{
				result = dao.UpdateThreadHiddenStatus(thread.ThreadID, status, participant.Email);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Causes a thread not to notify a particular user of new messages to that thread.
		/// </summary>
		/// <param name="thread">The thread to change the notify status on.</param>
		/// <param name="status">The new status of the thread.</param>
		/// <param name="participant">The user for whom the status should change.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadSilentStatus(IMessageThread thread, bool status, ISender participant)
		{
			if (null == thread)
			{ throw new ArgumentNullException("Thread to set the notification status for to cannot be null."); }
			if (null == participant)
			{ throw new ArgumentNullException("Participant to set the notification status for cannot be null."); }

			bool result = false;

			try
			{
				result = dao.UpdateThreadSilentStatus(thread.ThreadID, status, participant.Email);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		//Low priority, same job is essentially done in the GetUserThreadViewList, may remove later
		public bool ReadThreadNewMessages(IMessageThread thread, ISender user)
		{
			throw new NotImplementedException();
		}
	}
}
