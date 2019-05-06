using DataObjects;
using ExceptionLoggerLogic;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation
{
	/// <summary>
	/// Interaction logic for FrmAddMessageThreadParticipants.xaml
	/// </summary>
	public partial class FrmAddMessageThreadParticipants : Window
	{
		readonly IMessageThread _thread;

		public FrmAddMessageThreadParticipants(IMessageThread thread)
		{
			InitializeComponent();
			_thread = thread;
			ctrlParticipantSelector.LoadControl();
		}

		private void BtnThreadParticipantAddCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void BtnThreadParticipantAddSubmit_Click(object sender, RoutedEventArgs e)
		{
			RealThreadManager manager = new RealThreadManager(AppData.DataStoreType.msssql);

			try
			{
				if (!manager.AddThreadParticipants(_thread, ctrlParticipantSelector.SelectedParticipants))
				{ throw new ApplicationException("Error while adding thread participants."); }

				DialogResult = true;
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				DialogResult = false;
			}

			Close();
		}
	}
}