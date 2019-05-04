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
	/// Interaction logic for ctrlThreadParticipantAdder.xaml
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
		private IEnumerable<IMessagable> _possibleRecipients;

		public CtrlThreadParticipantAdder()
		{
			InitializeComponent();
		}

		/// <summary>
		/// This is the effective "startup method", since this is going to be a pretty
		/// memory hungry piece of software. This prevents us from doing a thicc chunk
		/// of work for no reason.
		/// </summary>
		public void LoadControl()
		{
			try
			{
				RoleManager roleManager = new RoleManager();
				EmployeeManager employeeManager = new EmployeeManager();
				GuestManager guestManager = new GuestManager();
				MemberManagerMSSQL memberManager = new MemberManagerMSSQL();
				DepartmentManager departmentManager = new DepartmentManager();

				//Get the lists of possible recipients from each manager
				IEnumerable<IMessagable> roles = (new RoleManager()).RetrieveAllActiveRoles();
				IEnumerable<IMessagable> employees = (new EmployeeManager()).SelectAllActiveEmployees();
				IEnumerable<IMessagable> guests = (new GuestManager()).ReadAllGuests().Where(g => g.Active);
				IEnumerable<IMessagable> members = (new MemberManagerMSSQL()).RetrieveAllMembers().Where(m => m.Active);
				IEnumerable<IMessagable> departments = (new DepartmentManager()).GetAllDepartments();

				//Populate list controls
				lstPossibleRecipientsEmployee.ItemsSource = employees.Select(item => item.Alias);
				lstPossibleRecipientsDepartment.ItemsSource = departments.Select(item => item.Alias);
				lstPossibleRecipientsRoles.ItemsSource = roles.Select(item => item.Alias);
				lstPossibleRecipientsMember.ItemsSource = members.Select(item => item.Alias);
				lstPossibleRecipientsGuest.ItemsSource = guests.Select(item => item.Alias);

				_possibleRecipients = employees.Concat(roles).Concat(guests).Concat(members).Concat(departments);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
			}



		}
	}
}
