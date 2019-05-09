using DataObjects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using ExceptionLoggerLogic;

namespace Presentation
{
	/// <summary>
	/// Interaction logic for ctrlThreadParticipantAdder.xaml. When instantiating 
	/// </summary>
	public partial class CtrlThreadParticipantAdder : UserControl
	{
		public List<IMessagable> SelectedParticipants
		{
			get
			{
				//If you try to access this property without running the LoadControl
				//method, you're gonna have a bad time.
				if (null == _possibleRecipients)
				{ return null; }
				else
				{
					IEnumerable<string> selected = lstFinalSelection.Items.Cast<string>();
					return _possibleRecipients.Where(recip => selected.Contains(recip.Alias)).ToList();
				}
			}
		}

		/// <summary>
		/// All possible recipients retrieved from the data store at the time of loading the control.
		/// </summary>
		private List<IMessagable> _possibleRecipients;

		public CtrlThreadParticipantAdder()
		{
			InitializeComponent();
			_possibleRecipients = new List<IMessagable>();
		}

		/// <summary>
		/// This is the effective "startup method", since this is going to be a pretty
		/// memory hungry piece of software. This prevents us from doing a thicc chunk
		/// of work for no reason.
		/// </summary>
		public void LoadControl()
		{
			//From each manager, retrieve the list of possible recipients
			//Do this seperate so that if one manager fails, they don't necessarily all fail

			try
			{
				EmployeeManager employeeManager = new EmployeeManager();
				IEnumerable<IMessagable> employees = (new EmployeeManager()).SelectAllActiveEmployees();
				foreach (string alias in employees.Select(item => item.Alias))
				{ lstPossibleRecipientsEmployee.Items.Add(alias); }
				_possibleRecipients = _possibleRecipients.Concat(employees).ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}
			try
			{
				RoleManager roleManager = new RoleManager();
				IEnumerable<IMessagable> roles = (new RoleManager()).RetrieveAllRoles();
				foreach (string alias in roles.Select(item => item.Alias))
				{ lstPossibleRecipientsRoles.Items.Add(alias); }
				_possibleRecipients = _possibleRecipients.Concat(roles).ToList();

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}
			try
			{
				GuestManager guestManager = new GuestManager();
				IEnumerable<IMessagable> guests = (new GuestManager()).ReadAllGuests().Where(g => g.Active);
				foreach (string alias in guests.Select(item => item.Alias))
				{ lstPossibleRecipientsGuest.Items.Add(alias); }
				_possibleRecipients = _possibleRecipients.Concat(guests).ToList();

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}
			try
			{
				MemberManagerMSSQL memberManager = new MemberManagerMSSQL();
				IEnumerable<IMessagable> members = (new MemberManagerMSSQL()).RetrieveAllMembers().Where(m => m.Active);
				foreach (string alias in members.Select(item => item.Alias))
				{ lstPossibleRecipientsMember.Items.Add(alias); }
				_possibleRecipients = _possibleRecipients.Concat(members).ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}
			try
			{
				DepartmentManager departmentManager = new DepartmentManager();
				IEnumerable<IMessagable> departments = (new DepartmentManager()).GetAllDepartments();
				foreach (string alias in departments.Select(item => item.Alias))
				{ lstPossibleRecipientsDepartment.Items.Add(alias); }
				_possibleRecipients = _possibleRecipients.Concat(departments).ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}
		}

		private void MoveToSelectedParticipantList(object sender, MouseButtonEventArgs e)
		{
			//Throw out anything that might be a misclick or would invalidate the operation
			if (!(sender is ListBox))
			{ return; }

			ListBox thisList = sender as ListBox;

			lstFinalSelection.Items.Add(thisList.SelectedItem);
			thisList.Items.Remove(thisList.SelectedItem);
		}

		private void LstFinalSelection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			IMessagable selectedRecipient = _possibleRecipients.First(r => r.Alias.Equals(lstFinalSelection.SelectedItem.ToString()));
			ListBox thisList = lstFinalSelection;

			if (selectedRecipient is Role)
			{
				lstPossibleRecipientsRoles.Items.Add(thisList.SelectedItem);
				thisList.Items.Remove(thisList.SelectedItem);
			}
			else if (selectedRecipient is Employee)
			{
				lstPossibleRecipientsEmployee.Items.Add(thisList.SelectedItem);
				thisList.Items.Remove(thisList.SelectedItem);
			}
			else if (selectedRecipient is Department)
			{
				lstPossibleRecipientsDepartment.Items.Add(thisList.SelectedItem);
				thisList.Items.Remove(thisList.SelectedItem);
			}
			else if (selectedRecipient is Guest)
			{
				lstPossibleRecipientsGuest.Items.Add(thisList.SelectedItem);
				thisList.Items.Remove(thisList.SelectedItem);
			}
			else if (selectedRecipient is Member)
			{
				lstPossibleRecipientsMember.Items.Add(thisList.SelectedItem);
				thisList.Items.Remove(thisList.SelectedItem);
			}
		}
	}
}