using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary>
	/// Austin Delaney
	/// Created: 2019/04/03
	/// 
	/// A class that denotes informational messages that can be exchanged between
	/// customers/guests/employees
	/// </summary>
	public class Message
	{
		public const int SUBJECT_MIN_LENGTH = 0;
		public const int SUBJECT_MAX_LENGTH = 100;
		public const int BODY_MIN_LENGTH = 0;
		public const int BODY_MAX_LENGTH = 1000;

		/// <summary>
		/// How this sender appears to other people who will read the message.
		/// </summary>
		public string SenderAlias { get; set; }
		/// <summary>
		/// Message subject line
		/// </summary>
		public string Subject { get; set; }
		/// <summary>
		/// The main text body of the message
		/// </summary>
		public string Body { get; set; }
		/// <summary>
		/// Exact time stamp the message was created at
		/// </summary>
		public DateTime DateTimeSent { get; set; }
		/// <summary>
		/// Data store assigned MessageID
		/// </summary>
		public int MessageID { get; set; }
		/// <summary>
		/// User responsible for sending the message
		/// </summary>
		public string SenderEmail { get; set; }
		/// <summary>
		/// Object of the user responsible for sending the message.
		/// </summary>
		public ISender Sender { get; set; }		
		/// <summary>
		/// Returns the validity of Message object.
		/// </summary>
		public bool IsValid()
		{
			return (SenderAlias.IsValidMessengerAlias() &&
				Body.IsValidMessageBody() &&
				Subject.IsValidMessageSubject() && 
				SenderEmail.IsValidEmail());
		}
	}
}