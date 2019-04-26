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
		/// <summary>
		/// How this sender appears to other people who will read the message.
		/// </summary>
		public string SenderAlias { get; set; }
		/// <summary>
		/// Message subject line
		/// </summary>
		/// 
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
			return (MessageValidator.IsValidSenderAlias(SenderAlias) &&
				MessageValidator.IsValidBody(Body) &&
				MessageValidator.IsValidSubject(Subject) && 
				Validation.IsValidEmail(SenderEmail));
		}
	}

	/// <summary>
	/// Validation logic for Message objects. Will be moved.
	/// </summary>
	public class MessageValidator
	{
		public static readonly int SENDER_ALIAS_MAX_LENGTH = 250;
		public static readonly int SUBJECT_MAX_LENGTH = 100;
		public static readonly int BODY_MAX_LENGTH = 1000;
		/// <summary author="Austin Delaney" created="2019/04/19">
		/// Confirms if an alias is valid or not.
		/// </summary>
		/// <param name="senderAlias"></param>
		/// <returns></returns>
		public static bool IsValidSenderAlias(string senderAlias)
		{
			try
			{
				ValidateSenderAlias(senderAlias);
				return true;
			}
			catch
			{
				return false;
			}
		}
		internal static void ValidateSenderAlias(string senderAlias)
		{
			if (senderAlias.Length > SENDER_ALIAS_MAX_LENGTH)
			{
				throw new Exception("Sender alias cannot exceed maximum character count of " + SENDER_ALIAS_MAX_LENGTH);
			}
			if (senderAlias == null)
			{
				throw new Exception("Sender alias cannot be null");
			}
		}
		/// <summary author="Austin Delaney" created="2019/04/19">
		/// Confirms if a string is a valid Subject for a message object.
		/// </summary>
		/// <param name="subject">The subject to validate.</param>
		/// <returns>Boolean if the subject is valid or not.</returns>
		public static bool IsValidSubject(string subject)
		{
			try
			{
				ValidateSubject(subject);
				return true;
			}
			catch
			{
				return false;
			}
		}
		internal static void ValidateSubject(string subject)
		{
			if (subject.Length > SUBJECT_MAX_LENGTH)
			{
				throw new Exception("Subject cannot exceed maximum character length of " + SUBJECT_MAX_LENGTH);
			}
			if (subject == null)
			{
				throw new Exception("Subject cannot be null.");
			}
		}
		/// <summary author="Austin Delaney" created="2019/04/19">
		/// Confirms if a string is a valid Body for a Message object.
		/// </summary>
		/// <param name="body">The body to confirm is valid or not.</param>
		/// <returns>Boolean if the body is valid.</returns>
		public static bool IsValidBody(string body)
		{
			try
			{
				ValidateBody(body);
				return true;
			}
			catch
			{
				return false;
			}
		}
		internal static void ValidateBody(string body)
		{
			if (body.Length > BODY_MAX_LENGTH)
			{
				throw new Exception("Body cannot exceed maximum character length of " + BODY_MAX_LENGTH);
			}
			if (body == null)
			{
				throw new Exception("Body cannot be null");
			}
		}
	}
}