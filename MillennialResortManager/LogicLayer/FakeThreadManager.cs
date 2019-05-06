using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Austin Delaney" created="2019/04/10">
	/// 
	/// </summary>
	public class FakeThreadManager : IThreadManager
	{
		/// <summary name="Austin Delaeny" created="2019/04/14">
		/// Dummy implementation that just returns true and performs no operations.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="newParticipants">Redundant.</param>
		/// <returns>True always.</returns>
		public bool AddThreadParticipants(IMessageThread thread, List<IMessagable> newParticipants)
		{
			return true;
		}

		/// <summary name="Austin Delaeny" created="2019/04/14">
		/// Generates mock user thread and sets the appropriate message/participants and returns it.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="recipients">Added participants</param>
		/// <returns></returns>
		public UserThread CreateNewThread(Message message, List<IMessagable> recipients)
		{
			var output = GetUserThread(1, new Guest { Email = "BillyJimBob@yahoo.com" });

			Dictionary<IMessagable, string> recipientsWithAlias = new Dictionary<IMessagable, string>();

			foreach (var item in recipients)
			{
				recipientsWithAlias[item] = item.Alias;
			}

			output.Messages.Clear();
			output.Messages.Add(message);
			output.ParticipantsWithAlias.Clear();
			output.ParticipantsWithAlias.Concat(recipientsWithAlias);

			return output;
		}

		/// <summary name="Austin Delaney" created="2019/04/14">
		/// Returns a mock UserThread with the thread ID and the user included.
		/// </summary>
		/// <param name="thread">Thread to rip the ID from.</param>
		/// <param name="user">User to throw in as a participant.</param>
		/// <returns></returns>
		public UserThread GetUserThread(IMessageThread thread, ISender user)
		{
			return GetUserThread(thread.ThreadID, user);
		}

		/// <summary name="Austin Delaney" created="2019/04/14">
		/// Returns a mock UserThread using the <paramref name="threadID"/> and with at least one message from the <paramref name="user"/>.
		/// </summary>
		/// <param name="threadID">Thread ID to use.</param>
		/// <param name="user">User to insert to message.</param>
		/// <returns>Mock UserThread.</returns>
		public UserThread GetUserThread(int threadID, ISender user)
		{
			List<Message> messages = new List<Message>
			{
				new Message
				{
					SenderAlias = "Austin b",
					Subject = "Some subject",
					Body = "Lots of text",
					DateTimeSent = DateTime.Today.AddDays(-3),
					MessageID = 1,
					Sender = new Member { Email = "Austin b" }
				},
				new Message
				{
					SenderAlias = "Austin",
					Subject = "Some subject",
					Body = "Lots of text",
					DateTimeSent = DateTime.Today.AddDays(-2),
					MessageID = 2,
					Sender = new Member { Email = user.Email }
				},
				new Message
				{
					SenderAlias = "Craig",
					Subject = "Some subject",
					Body = "Lots of text",
					DateTimeSent = DateTime.Today.AddDays(-1),
					MessageID = 3,
					Sender = new Member { Email = "Craig@craig.com" }
				},

			};

			Dictionary<IMessagable, string> messagables = new Dictionary<IMessagable, string>();
			messagables[new Role { RoleID = "Customer service" }] = "Customer service";
			messagables[new Department { DepartmentID = "Front desk" }] = "Front desk";
			messagables[new Employee { Email = user.Email }] = user.Email;

			return new UserThread
			{
				ThreadID = threadID,
				ParticipantsWithAlias = messagables,
				Messages = messages,
				Alias = user.Alias ?? user.Email,
				HasNewMessages = false,
				Archived = false,
				Hidden = false,
				Silenced = true
			};
		}

		/// <summary author="Austin Delaney" created="2019/04/14">
		/// Gets a list of made up UserThreadViews, including some archived and some hidden.
		/// </summary>
		/// <param name="user">User to insert as sender for thread messages.</param>
		/// <param name="showArchived">Show archived or not.</param>
		/// <returns>Mock UserThreadView list</returns>
		public List<UserThreadView> GetUserThreadViewList(ISender user, bool showArchived)
		{
			List<UserThreadView> output = new List<UserThreadView>();
			
			output.Add(new UserThreadView
			{
				NewestMessage = new Message
				{
					SenderAlias = "Austin",
					Subject = "Some subject",
					Body = "Lots of text",
					DateTimeSent = DateTime.Today.AddDays(-3),
					MessageID = 1,
					Sender = new Member { Email = user.Email }
				},
				ThreadID = 1,
				HasNewMessages = true,
				ThreadHidden = false,
				Archived = true,
				Alias = "Austin",
				ThreadOwner = "Austin",
				OpeningSubject = "Opening subject"
			});
			output.Add(new UserThreadView
			{
				NewestMessage = new Message
				{
					SenderAlias = "Austin",
					Subject = "Some subject",
					Body = "Lots of text",
					DateTimeSent = DateTime.Today.AddDays(-2),
					MessageID = 2,
					Sender = new Member { Email = user.Email }
				},
				ThreadID = 2,
				HasNewMessages = false,
				ThreadHidden = true,
				Archived = false,
				Alias = "Austin",
				ThreadOwner = user.Email,
				OpeningSubject = "Hidden subject"
			});
			output.Add(new UserThreadView
			{
				NewestMessage = new Message
				{
					SenderAlias = "Austin",
					Subject = "Some subject",
					Body = "Lots of text",
					DateTimeSent = DateTime.Today.AddDays(-1),
					MessageID = 3,
					Sender = new Member { Email = user.Email }
				},
				ThreadID = 3,
				HasNewMessages = false,
				ThreadHidden = false,
				Archived = false,
				Alias = "Austin",
				ThreadOwner = user.Email,
				OpeningSubject = "Hidden subject"
			});

			return output;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operations.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="participant">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool MarkThreadAsReadByParticipant(IMessageThread thread, ISender participant)
		{
			return true;
		}

		/// <summaryauthor="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operations.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="user">Redundant.</param>
		/// <returns></returns>
		public bool ReadThreadNewMessages(IMessageThread thread, ISender user)
		{
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="removeParticipants">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool RemoveThreadParticipants(IMessageThread thread, List<ISender> removeParticipants)
		{
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="reply">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool ReplyToThread(IMessageThread thread, Message reply)
		{
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="newAlias">Redundant.</param>
		/// <param name="participant">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool UpdateThreadAlias(IMessageThread thread, string newAlias, ISender participant)
		{
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="archived">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool UpdateThreadArchivedStatus(IMessageThread thread, bool archived)
		{
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="status">Redundant.</param>
		/// <param name="participant">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool UpdateThreadHiddenStatus(IMessageThread thread, bool status, ISender participant)
		{
			return true;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that just returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="status">Redundant.</param>
		/// <param name="participant">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool UpdateThreadSilentStatus(IMessageThread thread, bool status, ISender participant)
		{
			return true;
		}
	}
}