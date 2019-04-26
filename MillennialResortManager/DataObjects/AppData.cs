using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	/// <summary>
	/// Class to store variables accessible to all pieces of the project
	/// </summary>
	public static class AppData
	{
		/// <summary>
		/// This is the top most level of the application, and is set from the App.xaml.cs file at runtime.
		/// </summary>
		public static string TopRuntimeDirectory { get; set; }

		public enum DataStoreType
		{
			mock,
			msssql
		}
	}
}