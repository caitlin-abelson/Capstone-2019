using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Matt LaMarche" created="2019/01/24">
	/// MemberManagerMSSQL Is an implementation of the IMemberManager Interface meant to interact with the MSSQL MemberAccessor
	/// </summary>
	public class MemberManagerMSSQL : IMemberManager
	{
		private IMemberAccessor _memberAccessor;

		/// <summary author="Matt LaMarche" created="2019/01/24">
		/// Constructor allowing for non-static method calls
		/// </summary>
		public MemberManagerMSSQL()
		{
			_memberAccessor = new MemberAccessorMSSQL();
		}

		public MemberManagerMSSQL(MemberAccessorMock mockMemberAccessor)
		{
			_memberAccessor = mockMemberAccessor;
		}

		/// <summary author="Ramesh Adhikari" created="2019/01/30">
		/// </summary>
		public void CreateMember(Member member)
		{
			member.Validate();
			try
			{
				_memberAccessor.InsertMember(member);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Ramesh Adhikari" created="2019/01/30">
		/// </summary>
		public void DeleteMember(Member member)
		{
			try
			{
				if (member.Active)
				{
					_memberAccessor.DeactivateMember(member);
				}
				else
				{
					_memberAccessor.DeleteMember(member);
				}

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Ramesh Adhikari" created="2019/01/30">
		/// </summary>
		public void UpdateMember(Member oldMember, Member newMember)
		{
			newMember.Validate();
			oldMember.Validate();

			try
			{
				_memberAccessor.UpdateMember(newMember, oldMember);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Matt LaMarche" created="2019/01/24">
		/// SelectAllMembers asks our MemberAccessorMSSQL for a List<Member> containing all of the Active Members in our system
		/// </summary>
		/// <returns>A list of Members retrieved from the Member Accessor</returns>
		public List<Member> RetrieveAllMembers()
		{
			List<Member> members;
			try
			{
				members = _memberAccessor.SelectAllMembers();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return members;
		}

		/// <summary author="Ramesh Adhikari" created="2019/01/30">
		/// </summary>
		public Member RetrieveMember(int id)
		{
			Member member = null;
			try
			{
				member = _memberAccessor.SelectMember(id);
				if (member == null)
				{
					throw new NullReferenceException("Member not found");
				}

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return member;
		}

		/// <summary author="Ramesh Adhikari" created="2019/02/22">
		/// Deactivate the member from the member records
		/// </summary>
		public void DeactivateMember(Member selectedMember)
		{
			try
			{
				_memberAccessor.DeactivateMember(selectedMember);

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		public int RetrieveMemberByEmail(string email)
		{
			int id = 0;
			try
			{
				id = _memberAccessor.SelectMemberByEmail(email);
				if (id == 0)
				{
					throw new NullReferenceException("Member not found");
				}

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return id;
		}
	}
}