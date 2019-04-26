using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MemberAccessorMock : IMemberAccessor
    {
        List<Member> _members;

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created: 2019/02/14
        /// </summary>
        
        public MemberAccessorMock()
        {
            _members = new List<Member>();
        }

        public void InsertMember(Member newMember)
        {
            _members.Add(newMember);
        }

        public List<Member> SelectAllMembers()
        {
            return _members;
        }

        public Member SelectMember(int id)
        {
            return _members.Find(x => x.MemberID == id);
        }

        public void UpdateMember(Member newMember, Member oldMember)
        {
            var index = _members.FindIndex(x => x.MemberID == newMember.MemberID);
            _members[index] = newMember;
        }

        public void DeactivateMember(Member member)
        {
            member.Active = false;
            var index = _members.FindIndex(x => x.MemberID == member.MemberID);
            _members[index] = member;
        }

        public void DeleteMember(Member member)
        {
            var index = _members.FindIndex(x => x.MemberID == member.MemberID);
            _members.RemoveAt(index);
        }

        public int SelectMemberByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
