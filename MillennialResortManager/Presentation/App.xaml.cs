using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataObjects;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			string topFolder = "";
			string[] pathToDebug = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
			for (int i = 0; i < pathToDebug.Length - 4; i++)
			{topFolder += pathToDebug[i] + '\\';}
			DataObjects.AppData.TopRuntimeDirectory = topFolder;
		}
	}
}
