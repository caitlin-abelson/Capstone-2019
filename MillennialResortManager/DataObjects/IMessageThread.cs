using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary name="Austin Delaney" created="2019/04/04">
	/// Interface that defines the requirements for any DTO relating
	/// to a thread of messages. Does not specify if the view is for
	/// a particular user.
	/// </summary>
	public interface IMessageThread
	{
		/// <summary>
		/// The data store assigned ID of the specific thread.
		/// </summary>
		int ThreadID { get; set; }
		/// <summary>
		/// The newest message to be posted to this particular thread.
		/// </summary>
		Message NewestMessage { get; }
		/// <summary>
		/// If the thread has been archived or not.
		/// </summary>
		bool Archived { get; set; }
	}
}