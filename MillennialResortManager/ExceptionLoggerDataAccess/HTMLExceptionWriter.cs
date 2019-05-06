using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace ExceptionLoggerDataAccess
{
	public class HTMLExceptionWriter : IExceptionWriter
	{
		private static string htmlLogFile = @"ExceptionLogs\log.html";

		public void WriteExceptionDataToLog(string message, string stackTrace, string targetSiteName, Exception fullException)
		{
			//Exception logger will not log if the startup assembly does not
			//tell it where it can log to.
			if (null == AppData.TopRuntimeDirectory)
			{ return; }

			StringBuilder sb = new StringBuilder();

			sb.Append("<tr>");
			sb.Append("<td>" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "</td>");
			sb.Append("<td>" + message + "</td>");
			sb.Append("<td>" + stackTrace + "</td>");
			sb.Append("<td>" + targetSiteName + "</td>");
			sb.Append("<td>" + fullException.ToString() + "</td>");
			sb.Append("</tr>\n");

			StreamWriter outWrite = null;

			try
			{
				outWrite = File.AppendText(AppData.TopRuntimeDirectory + htmlLogFile);

				outWrite.Write(sb.ToString());
			}
			catch (Exception)
			{
			}
			finally
			{
				outWrite.Close();
			}
		}
	}
}
