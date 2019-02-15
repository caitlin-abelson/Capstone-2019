using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionLoggerDataAccess;

namespace ExceptionLoggerLogic
{
    public class ExceptionLogManager
	{
		public static ExceptionLogManager getInstance()
		{
			if (null == instance)
			{
				instance = new ExceptionLogManager();
			}
			return instance;
		}

		private static ExceptionLogManager instance;

		IExceptionWriter exceptionWriter;

		private ExceptionLogManager()
		{
			//Switch the below string to change where your exceptions are logged.
			string logOutputFormat = "html";

			switch (logOutputFormat)
			{
				case "html":
					exceptionWriter = new HTMLExceptionWriter();
					break;
				default:
					exceptionWriter = new HTMLExceptionWriter();
					break;
			}
		}

		public void LogException(Exception exception)
		{
			//Null check everything but the message
			string message = exception.Message;
			string stackTrace = exception.StackTrace ?? "(NA)";
			string targetSite = (null != exception.TargetSite ? exception.TargetSite.Name : "(NA)");

			exceptionWriter.WriteExceptionDataToLog(message, stackTrace, targetSite, exception);
		}
	}
}
