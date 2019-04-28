using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Austin Delaney" created="2019/04/15">
	/// Mock implementation of the IMessageManager for the purpose of UI testing. No data consistency.
	/// </summary>
	public class FakeMessageManager : IMessageManager
	{
		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Returns a number simulating the creation of a new message/threadID.
		/// </summary>
		/// <param name="message">Redundant.</param>
		/// <returns>Always 1.</returns>
		public int CreateNewMessage(Message message)
		{
			return 1;
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Dummy implementation that returns true and performs no operation.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="message">Redundant.</param>
		/// <returns>Always true.</returns>
		public bool CreateNewReply(IMessageThread thread, Message message)
		{
			return true;
		}

		/// <summary>
		/// Returns a mock Message object.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <returns></returns>
		public Message RetrieveNewestThreadMessage(IMessageThread thread)
		{
			return new Message
			{
				SenderAlias = "Manager",
				Subject = "Cats infeting the bathroom",
				Body = "Another customer was killed by the rabid felines in the bathroom, this is getting to be a real problem and management wants it taken care of now.",
				DateTimeSent = DateTime.Now.AddDays(-1),
				MessageID = 23,
				Sender = new Employee { Email = "jimbob@xxx.com", DepartmentID = "Cat hunters" }
			};
		}

		/// <summary author="Austin Delaney" created="2019/04/15">
		/// Returns a short list of mock Message objects.
		/// </summary>
		/// <param name="thread">Redundant.</param>
		/// <param name="includeArchived">Redundant.</param>
		/// <returns>Short list of mock Messages.</returns>
		public List<Message> RetrieveThreadMessages(IMessageThread thread)
		{
			List<Message> output = new List<Message>
			{
				new Message
				{
					SenderAlias = "Manager",
					Subject = "Cats infeting the bathroom",
					Body = "Another customer was killed by the rabid felines in the bathroom, this is getting to be a real problem and management wants it taken care of now.",
					DateTimeSent = DateTime.Now.AddDays(-1),
					MessageID = 23,
					Sender = new Employee { Email = "jimbob@xxx.com", DepartmentID = "Cat hunters" }
				},
				new Message
				{
					SenderAlias = "Cat Terminator",
					Subject = "Cats infeting the bathroom",
					Body = "I'll take care of it.",
					DateTimeSent = DateTime.Now.AddDays(-.5),
					MessageID = 24,
					Sender = new Employee { Email = "cat_terminator@xxx.com", DepartmentID = "Cat hunters" }
				}
			};

			return output;
		}
	}
}