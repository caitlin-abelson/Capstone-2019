using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLoggerDataAccess
{
    public interface IExceptionWriter
    {
		void WriteExceptionDataToLog(string message, string stackTrace, string targetSiteName, Exception fullException);
	}
}
