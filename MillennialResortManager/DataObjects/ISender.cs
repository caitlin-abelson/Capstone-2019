using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary>
	/// Austin Delaney
	/// Created: 2019/04/07
	/// 
	/// An interface which allows data objects to be used to create and send
	/// messages in the messaging system.
	/// </summary>
	public interface ISender : IMessagable
	{
		string Email { get; }
	}
}