using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ExceptionLoggerLogic;

namespace DataAccessLayer
{
	/// <summary author="Austin Delaney" created="2019/04/25">
	/// MSSQL implementation of the <see cref="IThreadAccessor"/> interface for thread
	/// operations in the data store.
	/// </summary>
	public class ThreadAccessorMSSQL : IThreadAccessor
	{
		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Adds a every emloyee in a department to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the employees are participating in.</param>
		/// <param name="deptID">The ID of the department</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool AddDepartmentThreadParticipant(int threadID, string deptID)
		{
			if (!Validation.IsValidDepartmentID(deptID))
			{ throw new ArgumentException("Invalid department ID."); }

			int result = -1;
			
			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_add_employee_participants_by_department";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@DepartmentID", deptID);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (result > 0);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Adds a new employee to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the employee is participating in.</param>
		/// <param name="employeeEmail">The email of the employee</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool AddEmployeeThreadParticipant(int threadID, string employeeEmail)
		{
			if (!Validation.IsValidEmail(employeeEmail))
			{ throw new ArgumentException("Invalid employee email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_add_employee_to_thread";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@EmployeeEmail", employeeEmail);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Adds a new guest to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the guest is participating in.</param>
		/// <param name="guestEmail">The email of the guest</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool AddGuestThreadParticipant(int threadID, string guestEmail)
		{
			if (!Validation.IsValidEmail(guestEmail))
			{ throw new ArgumentException("Invalid employee email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_add_guest_to_thread";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@GuestEmail", guestEmail);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Adds a new member to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the member is participating in.</param>
		/// <param name="memberEmail">The email of the member</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool AddMemberThreadParticipant(int threadID, string memberEmail)
		{
			if (!Validation.IsValidEmail(memberEmail))
			{ throw new ArgumentException("Invalid member email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_add_member_to_thread";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@MemberEmail", memberEmail);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Adds a every emloyee with a role to the datastore as a participant in a specified thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread that the employees are participating in.</param>
		/// <param name="roleID">The ID of the role</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool AddRoleThreadParticipant(int threadID, string roleID)
		{
			if (!Validation.IsValidRoleID(roleID))
			{ throw new ArgumentException("Invalid role ID."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_add_employee_participants_by_role";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@RoleID", roleID);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (result > 0);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets the emails and aliases of the participants involved in a thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread to get the participant list for.</param>
		/// <returns>All the thread participants with the email as the key and the alias as the value</returns>
		public Dictionary<string, string> GetThreadParticipantEmailAndAlias(int threadID)
		{
			//[ParticipantEmail], [ParticipantAlias]

			Dictionary<string, string> result = new Dictionary<string, string>();

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_retrieve_all_thread_participants";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);

			try
			{
				conn.Open();

				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						result[reader.GetString(0)] = reader.GetString(1);
					}
				}
				else
				{
					throw new ApplicationException("Unable to find any participants in database for thread " + threadID);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Gets the details of a thread as seen by a particular user
		/// </summary>
		/// <param name="threadID">The ID of the thread to get the details on.</param>
		/// <param name="email">The name of the user who will be viewing the thread.</param>
		/// <returns>A UserThread obj, devoid of messages.</returns>
		public UserThread GetUserThreadData(int threadID, string email)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }

			UserThread result = null;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_retrieve_thread_data_by_participant_email";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@Email", email);

			try
			{
				conn.Open();

				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();

					result = new UserThread
					{
						Alias = reader.GetString(0),
						Silenced = reader.GetBoolean(1),
						HasNewMessages = reader.GetBoolean(2),
						Hidden = reader.GetBoolean(3)
					};
				}
				else
				{
					throw new ApplicationException("Unable to find thread " + threadID + " for user " + email);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return result;
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Retrieves a list of objects to hold small bits of easily readable thread data
		/// from the data store.
		/// </summary>
		/// <updates>
		/// <update author="Austin Delaney" date="2019/04/11">
		/// Added showArchived param, so the list can prematurely filter archived threads and save on
		/// data being transferred.
		/// </update>
		/// </updates>
		/// <param name="email">The email of the participant to whom the threads relate to.</param>
		/// <param name="showArchived">Include archived threads in the retrieval from the data store.</param>
		/// <returns>A list of compact easily human readable UserThreadView objects.</returns>
		public List<UserThreadView> GetUserThreadViews(string email, bool showArchived)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }
			List<UserThreadView> result = new List<UserThreadView>();

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_retrieve_small_threads_by_participant_email_and_archived";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@Email", email);
			cmd.Parameters.AddWithValue("@Archived", showArchived);

			try
			{
				conn.Open();

				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while(reader.Read())
					{
						result.Add(new UserThreadView
						{
							HasNewMessages = reader.GetBoolean(0),
							ThreadHidden = reader.GetBoolean(1),
							Archived = reader.GetBoolean(2),
							Alias = reader.GetString(3),
							ThreadOwner = reader.GetString(4),
							OpeningSubject = reader.GetString(5),
							DateOpened = reader.GetDateTime(6)
						});
					}
				}
				else
				{
					throw new ApplicationException("Unable to find any threads in database for user " + email);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return result;
		}
		
		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Removes a specified user from the thread participants in the data store.
		/// </summary>
		/// <param name="threadID">The ID of the specified thread to remove the user from.</param>
		/// <param name="email">The email of the user being removed.</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool RemoveThreadParticipant(int threadID, string email)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_delete_participant_from_thread";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@Email", threadID);
			cmd.Parameters.AddWithValue("@ThreadID", email);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Sets the status of a thread to archived in the data store.
		/// </summary>
		/// <param name="threadID">The ID of the thread to be set as archived.</param>
		/// <param name="archived">What the status of the thread should be.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadArchivedStatus(int threadID, bool archived)
		{
			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_update_thread_archived_state";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@Archived", archived);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Changes the visibility level of a thread to that user. Method should check if the
		/// visibility of the thread is already set to the <paramref name="status"/>, and if
		/// it is, should not fail.
		/// </summary>
		/// <param name="threadID">The thread to change the status on.</param>
		/// <param name="status">Hidden or unhidden.</param>
		/// <param name="email">The user who would like to have the thread hidden for them.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadHiddenStatus(int threadID, bool status, string email)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_update_participant_thread_hidden_state";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@Hidden", status);
			cmd.Parameters.AddWithValue("@ParticipantEmail", email);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Changes the status of a thread in the data store to 'no new messages' for a given participant of 
		/// that thread.
		/// </summary>
		/// <param name="threadID">The id of the thread to change.</param>
		/// <param name="email">The email of the participant who is reading the thread.</param>
		/// <returns>Whether the change was successfully made to the data store.</returns>
		public bool UpdateThreadParticipantNewMessageStatus(int threadID, string email)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_update_thread_new_messages_status_for_participant";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@Email", email);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Causes a threads notification of a particular user of new messages to that thread.
		/// </summary>
		/// <param name="threadID">The ID of the thread to change the notify status on.</param>
		/// <param name="status">The new status of the thread.</param>
		/// <param name="email">The name of the user for whom the status should change.</param>
		/// <returns>Whether the operation was a success or not.</returns>
		public bool UpdateThreadSilentStatus(int threadID, bool status, string email)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_update_participant_thread_alert_user_new_messages";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@AlertStatus", status);
			cmd.Parameters.AddWithValue("@ParticipantEmail", email);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Sets a users thread alias to a chosen value.
		/// </summary>
		/// <param name="threadID">The ID of the thread to change the alias for.</param>
		/// <param name="email">The email of the user changing their alias.</param>
		/// <param name="newAlias">The new alias for the user.</param>
		/// <returns>If the operation was a success or not.</returns>
		public bool UpdateUserThreadAlias(int threadID, string email, string newAlias)
		{
			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }
			if (!Validation.IsValidEmail(newAlias))
			{ throw new ArgumentException("Invalid alias."); }

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_update_thread_participant_alias";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@Email", email);
			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@NewAlias", newAlias);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (1 == result || 0 == result);
		}

		/// <summary author="Austin Delaney" created="2019/04/25">
		/// Low priority, may remove.
		/// </summary>
		/// <param name="threadID"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public bool ReadThreadNewMessages(int threadID, string email)
		{
			throw new NotImplementedException("Pending removal.");

			if (!Validation.IsValidEmail(email))
			{ throw new ArgumentException("Invalid user email."); }

			//@Email [nvarchar](250),@ThreadID[int]

			//[HasUnreadMessages] [bit]

			int result = -1;

			var conn = DBConnection.GetDbConnection();
			var procedure = @"sp_new_message";
			var cmd = new SqlCommand(procedure, conn)
			{ CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@ThreadID", threadID);
			cmd.Parameters.AddWithValue("@DepartmentID", email);

			try
			{
				conn.Open();

				result = (int)cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			finally
			{
				conn.Close();
			}

			return (result > 0);
		}
	}
}
