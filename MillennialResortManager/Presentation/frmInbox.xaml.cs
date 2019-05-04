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
			IRoleManager roleManager = new FakeRoleManager();

			InitializeComponent();
			_threadManager = new FakeThreadManager();
			_employee = new Employee
			{
				Email = "jeff@gmail.com",
				EmployeeRoles = roleManager.RetrieveAllActiveRoles()
			};
			SetupThreadList();
			DisableThreadFields();
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

		private void PopulateMainThreadArea(IMessageThread selectedThread)
		{
			try
			{
				var thread = _threadManager.GetUserThread(selectedThread, _employee);
				_userThread = thread ?? throw new ApplicationException("Unable to obtain thread data from database, please relog and try again.");
			}
			catch (Exception ex)
			{
				DisableThreadFields();
				ExceptionLogManager.getInstance().LogException(ex);
				MessageBox.Show(ex.Message + "\nPlease relog your account or contact IT.");
			}

			if (!IsWindowValidForThreadOperations())
			{
				DisableThreadFields();
				return;
			}

			EnableThreadFields();
			//alias dropdown
			cboAliasPicker.ItemsSource = null;
			cboAliasPicker.ItemsSource = _employee.Aliases.Where(a => a != null);
			cboAliasPicker.SelectedValue = _userThread.Alias;

			//participants list
			//If the participant is an object that implements ISender (this will always be true in live scenarios) then cast as such and get the email to display.
			lstThreadParticipants.ItemsSource = null;
			lstThreadParticipants.ItemsSource = _userThread.ParticipantsWithAlias.ToDictionary(kp => (kp.Key is ISender) ? (kp.Key as ISender).Email : kp.Key.Alias, kp => kp.Value);

			//message list
			lstThreadMessages.ItemsSource = null;
			lstThreadMessages.ItemsSource = _userThread.Messages;

			//bottom check boxes
			chkThreadHide.IsChecked = _userThread.Hidden;
			chkThreadSilence.IsChecked = _userThread.Silenced;
		}

		//Clicks

		private void ChkShowArchived_Click(object sender, RoutedEventArgs e)
		{
			SetupThreadList();
		}

		private void BtnThreadListButton_Click(object sender, RoutedEventArgs e)
		{
			if (null == dgMessageThreadList.SelectedItem || !(dgMessageThreadList.SelectedItem is IMessageThread))
			{
				MessageBox.Show("Invalid message thread selected, please reselect.");
				return;
			}

			PopulateMainThreadArea(dgMessageThreadList.SelectedItem as IMessageThread);
		}

		private void ChkShowHidden_Click(object sender, RoutedEventArgs e)
		{
			SetupThreadList();
		}

		private void ChkThreadSilence_Click(object sender, RoutedEventArgs e)
		{
			if (!IsWindowValidForThreadOperations())
			{ return; }

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
				PopulateMainThreadArea(_userThread);
			}
		}

		/// <summary>
		/// Disable all the fields related to a targeted thread, in the event of window loading and
		/// a failed attempt at a thread load.
		/// </summary>
		private void DisableThreadFields()
		{
			cboAliasPicker.IsEnabled = false;
			cboAliasPicker.ItemsSource = null;
			btnThreadParticipantAdd.IsEnabled = false;
			btnThreadParticipantRemove.IsEnabled = false;
			btnThreadReply.IsEnabled = false;
			chkThreadHide.IsEnabled = false;
			chkThreadSilence.IsEnabled = false;
			lstThreadMessages.IsEnabled = false;
			lstThreadMessages.ItemsSource = null;
			lstThreadParticipants.IsEnabled = false;
			lstThreadParticipants.ItemsSource = null;
		}

		/// <summary>
		/// Enable all fields related to a targeted thread in the event a thread is successfully loaded.
		/// </summary>
		private void EnableThreadFields()
		{
			cboAliasPicker.IsEnabled = true;
			btnThreadParticipantAdd.IsEnabled = true;
			btnThreadParticipantRemove.IsEnabled = true;
			btnThreadReply.IsEnabled = true;
			chkThreadHide.IsEnabled = true;
			chkThreadSilence.IsEnabled = true;
			lstThreadMessages.IsEnabled = true;
			lstThreadParticipants.IsEnabled = true;
		}

		private void CboAliasPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//Following statement filters out changes that would typically be made by a method call
			if (0 == e.RemovedItems.Count ||
				null == e.RemovedItems ||
				0 == e.AddedItems.Count ||
				null == e.AddedItems)
			{ return; }

			if (!IsWindowValidForThreadOperations())
			{ return; }

			string newAlias = cboAliasPicker.SelectedItem.ToString();

			if (!newAlias.IsValidMessengerAlias())
			{
				MessageBox.Show("Something went wrong when populating your alias, now refreshing thread.");
				PopulateMainThreadArea(_userThread);
			}

			var windowResult = MessageBox.Show("Change alias in this thread to " + newAlias + "?\n", "Change thread alias", MessageBoxButton.OKCancel);

			switch (windowResult)
			{
				case MessageBoxResult.OK:
					try
					{
						if (_threadManager.UpdateThreadAlias(_userThread, newAlias, _employee))
						{ MessageBox.Show("Alias change successful."); }
						else
						{ throw new ApplicationException("Something went wrong changing your alias, please try again"); }
					}
					catch (Exception ex)
					{
						Exception exc = new Exception("Thread: " + _userThread.ThreadID +
														", New alias: " + newAlias +
														", Old alias: " + _userThread.Alias +
														", User Email: " + _employee.Email, ex);
						ExceptionLogManager.getInstance().LogException(exc);
						MessageBox.Show(ex.Message);
					}
					return;
				case MessageBoxResult.None:
				case MessageBoxResult.Cancel:
				default:
					cboAliasPicker.SelectedItem = _userThread.Alias;
					return;
			}
		}

		/// <summary>
		/// Confirms the validity of the local thread item and the local target employee.
		/// </summary>
		/// <returns>True, or false if the employee is null or the thread is null.</returns>
		private bool IsWindowValidForThreadOperations()
		{
			bool result = true;

			if (null == _userThread)
			{
				MessageBox.Show("Please select Thread.");
				result = false;
			}
			if (null == _employee)
			{
				MessageBox.Show("Invalid user detected. Please relog.");
				result = false;
			}

			return result;
		}

		private void ChkThreadHide_Click(object sender, RoutedEventArgs e)
		{
			if (!IsWindowValidForThreadOperations())
			{ return; }

			try
			{
				if (_threadManager.UpdateThreadHiddenStatus(_userThread, chkThreadSilence.IsChecked.Value, _employee))
				{
					throw new ApplicationException("Unable to change thread hidden status for user " + _employee.Email + " in thread " + _userThread.ThreadID);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				MessageBox.Show(ex.Message);
				PopulateMainThreadArea(_userThread);
			}
		}

		private void BtnNewThread_Click(object sender, RoutedEventArgs e)
		{
			var newMessageForm = new frmNewMessage(MessageDestination.newThread, _employee);
			try
			{
				var result = newMessageForm.ShowDialog();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}

		}
	}
}