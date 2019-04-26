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
	/// An interface to be applied to all groups that can be included in
	/// messages and threads. Acts as a marker and also as a supplier of
	/// aliases to use when sending messages
	/// </summary>
	public interface IMessagable
	{
		/// <summary>
		/// The available aliases that can be used for sending messages
		/// </summary>
		List<string> Aliases { get; }
		/// <summary>
		/// The primary alias to be used for this object
		/// </summary>
		string Alias { get; }
	}
}