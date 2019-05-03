using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary author="Austin Delaney" created="2019/04/04">
	/// Class for creating a list view of user threads without large
	/// amounts of unnecessary data transfer
	/// </summary>
	/// <updates>
	/// <update author="Austin Delaney" date="2019/04/12">
	/// Added ThreadOwner, OpeningSubject properties.
	/// </update>
	/// <update author="Austin Delaney" date="2019/04/23">
	/// Added DateOpened field.
	/// </update>
	/// </updates>
	public class UserThreadView : IMessageThread
	{
		/// <summary>
		/// Data store assigned thread ID
		/// </summary>
		public int ThreadID { get; set; }
		/// <summary>
		/// The newest message that was posted to this thread
		/// </summary>
		public Message NewestMessage { get; set; }
		/// <summary>
		/// If the user has any unread messages in the thread
		/// </summary>
		public bool HasNewMessages { get; set; }
		/// <summary>
		/// Whether the thread should be visibile by default or remain hidden from view
		/// </summary>
		public bool ThreadHidden { get; set; }
		/// <summary>
		/// If the thread is marked as archived.
		/// </summary>
		public bool Archived { get; set; }
		/// <summary>
		/// The alias that the user is currently visible as to this thread. Can be either
		/// the user's email/username, one of their roles, or their department. For a
		/// customer this should always be the username.
		/// </summary>
		public string Alias { get; set; }
		/// <summary>
		/// The alias of the person who started the thread.
		/// </summary>
		public string ThreadOwner { get; set; }
		/// <summary>
		/// The subject line of the message that started the thread.
		/// </summary>
		public string OpeningSubject { get; set; }
		/// <summary>
		/// The date that the thread was opened.
		/// </summary>
		public DateTime DateOpened { get; set; }
	}
}