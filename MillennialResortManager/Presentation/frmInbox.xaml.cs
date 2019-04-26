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
using LogicLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace Presentation
{
	/// <summary>
	/// Interaction logic for frmInbox.xaml
	/// </summary>
	public partial class frmInbox : Window
	{
		IThreadManager _threadManager;
		IMessageManager _messageManager;
		Employee _employee;
		UserThread _userThread;

		public frmInbox()
		{
			InitializeComponent();
			_threadManager = new FakeThreadManager();
			_employee = new Employee { Email = "big_dick_rick@gmail.com" };
			SetupThreadList();
		}

		/// <summary author="Austin Delaney" created="2019/04/12">
		/// Sets the thread list to the list found for local employee based on if the 
		/// hidden or archived checks are checked.
		/// </summary>
		private void SetupThreadList()
		{
			List<UserThreadView> threads;

			try
			{
				if (chkShowHidden.IsChecked.Value)
				{
					threads = _threadManager.GetUserThreadViewList(_employee, chkShowArchived.IsChecked.Value);
				}
				else
				{
					List<UserThreadView> unfilteredList = _threadManager.GetUserThreadViewList(_employee, chkShowArchived.IsChecked.Value);
					threads = unfilteredList.Where(t => !t.ThreadHidden).ToList();
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				MessageBox.Show(ex.Message);
				threads = new List<UserThreadView>();
			}

			dgMessageThreadList.ItemsSource = threads;
		}

		private void ChkShowHidden_Click(object sender, RoutedEventArgs e)
		{
			SetupThreadList();
		}

		private void ChkThreadSilence_Click(object sender, RoutedEventArgs e)
		{
			if (null != _userThread && null != _employee)
			{
				try
				{
					if (_threadManager.UpdateThreadSilentStatus(_userThread, chkThreadSilence.IsChecked.Value, _employee))
					{
						throw new ApplicationException("Unable to change thread silent status for user " + _employee.Email + " in thread " + _userThread.ThreadID);
					}
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void PopulateMainThreadArea(IMessageThread selectedThread)
		{
			if (null != selectedThread && null != _employee)
			{
				UserThread thread = null;

				try
				{
					thread = _threadManager.GetUserThread(selectedThread, _employee);

					if (thread == null)
					{
						throw new ApplicationException("Unable to change thread silent status for user " + _employee.Email + " in thread " + _userThread.ThreadID);
					}
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					MessageBox.Show(ex.Message);
				}

				//alias dropdown
				cboAliasPicker.ItemsSource = _employee.Aliases;
				cboAliasPicker.SelectedValue = thread.Alias;

				//participants list
				lstThreadParticipants.ItemsSource = thread.ParticipantsWithAlias;

				//message list
				lstThreadMessages.ItemsSource = thread.Messages;

				//bottom check boxes
				chkThreadHide.IsChecked = thread.Hidden;
				chkThreadSilence.IsChecked = thread.Silenced;
			}
		}

		private void ChkShowArchived_Click(object sender, RoutedEventArgs e)
		{
			SetupThreadList();
		}

		private void BtnThreadListButton_Click(object sender, RoutedEventArgs e)
		{
			PopulateMainThreadArea(dgMessageThreadList.SelectedItem as IMessageThread);
		}
	}
}
