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
using System.Windows.Shapes;
using LogicLayer;
using ExceptionLoggerLogic;

namespace Presentation
{
	/// <summary>
	/// Represents the final outcome of this window and the message it contains.
	/// </summary>
	public enum MessageDestination
	{
		newThread,
		existingThread
	}

	/// <summary>
	/// Window for creating a new message or a reply to an existing thread of messages.
	/// </summary>
	public partial class FrmNewMessage : Window
	{
		readonly MessageDestination _messageDestination;
		readonly Employee _sender;
		readonly string _defaultSubject;
		readonly IThreadManager _threadManager;
		readonly IMessageThread _thread;

		public FrmNewMessage(MessageDestination state, Employee sender, IMessageThread thread = null)
		{
			if (null == sender)
			{
				MessageBox.Show("Invalid employee, please relog and try again.");
				Close();
				return;
			}

			InitializeComponent();

			//Cannot switch on object types, so here we are using a classic ElIf chain
			if (thread is UserThreadView)
			{
				_defaultSubject = (thread as UserThreadView).OpeningSubject;
			}
			else if (thread is UserThread)
			{
				//get the subject from the oldest message sent
				_defaultSubject = (thread as UserThread).Messages.OrderBy(t => t.DateTimeSent).First().Subject;
			}
			else
			{
				_defaultSubject = "";
			}

			_thread = thread;
			_messageDestination = state;
			_sender = sender;
			_threadManager = new RealThreadManager(AppData.DataStoreType.msssql);

			SetupWindow();
		}

		private void SetupWindow()
		{
			cboMessageAliasPicker.ItemsSource = _sender.Aliases;
			txtNewMessageSubject.Text = _defaultSubject;

			switch (_messageDestination)
			{
				case MessageDestination.newThread:
					ctrlParticipantSelector.LoadControl();
					break;
				case MessageDestination.existingThread:
					colParticipants.Width = new GridLength(0);
					ctrlParticipantSelector.IsEnabled = false;
					break;
				default:
					ExceptionLogManager.getInstance().LogException(new ApplicationException
						("Message destination was not set properly, app was unable to determine if message" +
						" was for a new thread or an existing one."));
					MessageBox.Show("There was an error, please contact IT or review your error logs.");
					return;
			}
		}

		private void BtnCancelNewMessage_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void BtnSendNewMessage_Click(object sender, RoutedEventArgs e)
		{
			Message message = GetMessageInput();

			if (message.IsValid() && _sender.Aliases.Contains(cboMessageAliasPicker.SelectedItem.ToString()))
			{
				try
				{
					switch (_messageDestination)
					{

						case MessageDestination.newThread:
							_threadManager.CreateNewThread(message, ctrlParticipantSelector.SelectedParticipants);
							break;
						case MessageDestination.existingThread:
							_threadManager.ReplyToThread(_thread, message);
							break;
						default:
							throw new ApplicationException("Message destination was not set properly, app was unable to determine if message" +
								" was for a new thread or an existing one.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("There was an error, please contact IT or review your error logs.");
					ExceptionLogManager.getInstance().LogException(ex);
					return;
				}
				//You win, close the window buddy
				Close();
			}
			else
			{
				MessageBox.Show("One or more fields are invalid.");
			}
		}

		private Message GetMessageInput()
		{
			Message message = new Message
			{
				Sender = _sender,
				Body = txtNewMessageBody.Text,
				DateTimeSent = DateTime.Now,
				SenderEmail = _sender.Email,
				SenderAlias = cboMessageAliasPicker.SelectedItem.ToString(),
				Subject = txtNewMessageSubject.Text
			};

			return message;
		}
	}
}