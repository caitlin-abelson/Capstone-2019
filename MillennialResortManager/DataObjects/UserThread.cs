using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary author="Austin Delaney" created="2019/04/03">
	/// A DTO representing a thread of messages as seen by a particular user.
	/// </summary>
	/// <updates>
	/// <update author="Austin Delaney" date="2019-04-25">
	/// Changed Participants field from a List(IMessagables) to Dictionary(IMessagable, string) ParticipantsWithAlias
	/// </update>
	/// </updates>
	public class UserThread : IMessageThread
	{
		/// <summary>
		/// List of participants that can view the thread and their alias within the thread.
		/// </summary>
		public Dictionary<IMessagable, string> ParticipantsWithAlias { get; set; }
		/// <summary>
		/// List of messages belonging to the thread.
		/// </summary>
		public List<Message> Messages { get; set; }
		/// <summary>
		/// The alias that the user is currently visible as to this thread. Can be either
		/// the user's email/username, one of their roles, or their department. For a
		/// customer this should always be the username.
		/// </summary>
		public string Alias { get; set; }
		/// <summary>
		/// Data store assigned thread ID
		/// </summary>
		public int ThreadID { get; set; }
		/// <summary>
		/// If the user has any unread messages in the thread
		/// </summary>
		public bool HasNewMessages { get; set; }
		/// <summary>
		/// If the thread is marked as archived.
		/// </summary>
		public bool Archived { get; set; }
		/// <summary>
		/// If the thread is regularly hidden from view
		/// </summary>
		public bool Hidden { get; set; }
		/// <summary>
		/// If the user viewing this thread is receiving alerts for new messages
		/// from this thread.
		/// </summary>
		public bool Silenced { get; set; }
		/// <summary>
		/// The newest message that was posted to this thread
		/// </summary>
		public Message NewestMessage
		{
			get
			{
				return Messages.OrderByDescending(m => m.DateTimeSent).First();
			}
		}
	}

	/// <summary>
	/// Responsible for validation of UserThread related members.
	/// </summary>
	public class UserThreadValidator
	{
		public static readonly int ALIAS_MAX_LENGTH = 250;

		/// <summary>
		/// Checks if an 
		/// </summary>
		/// <param name="alias"></param>
		/// <returns></returns>
		public bool IsValidUserThreadAlias(string alias)
		{
			try
			{
				ValidateThreadAlias(alias);
				return true;
			}
			catch
			{
				return false;
			}
		}
		internal void ValidateThreadAlias(string alias)
		{
			if (alias.Length > ALIAS_MAX_LENGTH)
			{
				throw new Exception("Alias length cannot exceed maximum character count of " + ALIAS_MAX_LENGTH);
			}
		}
	}
}