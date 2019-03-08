using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class SponsorAccessorMock : ISponsorAccessor
    {
        private List<Sponsor> _sponsor;
        private List<BrowseSponsor> _browseSponsor;
        private List<int> _allSponsors;
        private List<string> _states;
        private List<string> _statusID;

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Constructor for mock accessor
        /// </summary>
        public SponsorAccessorMock()
        {
            _sponsor = new List<Sponsor>();
            _sponsor.Add(new Sponsor() { SponsorID = 100000, Name = "ABC", Address = "123 Strong", City = "Marion", State = "IA", PhoneNumber = "13196661234", Email = "john@abc.com", ContactFirstName = "John", ContactLastName = "Smith", StatusID = "New", DateAdded = new DateTime(2019, 01, 19), Active = true });
            _sponsor.Add(new Sponsor() { SponsorID = 100001, Name = "DEF", Address = "234 Great", City = "Hiawatha", State = "IA", PhoneNumber = "13196661235", Email = "jane@def.com", ContactFirstName = "Jane", ContactLastName = "Lee", StatusID = "New", DateAdded = new DateTime(2019, 01, 10), Active = true });
            _sponsor.Add(new Sponsor() { SponsorID = 100002, Name = "GHI", Address = "345 Awesome", City = "Cedar Rapids", State = "IA", PhoneNumber = "13196661236", Email = "adam@ghi.com", ContactFirstName = "Adam", ContactLastName = "Now", StatusID = "New", DateAdded = new DateTime(2019, 02, 11), Active = true });
            _browseSponsor = new List<BrowseSponsor>();
            _allSponsors = new List<int>();
            foreach (var eSponsor in _sponsor)
            {
                _allSponsors.Add(eSponsor.SponsorID);
            }

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Inserting a sponsor for mock accessor
        /// </summary>
        /// <param name="newSponsor"></param>        
        public void InsertSponsor(Sponsor newSponsor)
        {
            _sponsor.Add(newSponsor);

        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Deactivate sponsor for mock accessor
        /// </summary>
        /// <param name="SponsorID"></param>
        public void DeactivateSponsor(int SponsorID)
        {
            bool foundSponsor = false;
            foreach (var eSponsor in _sponsor)
            {
                if (eSponsor.SponsorID == SponsorID)
                {
                    eSponsor.Active = false;
                    foundSponsor = true;
                    break;
                }
            }
            if (!foundSponsor)
            {
                throw new ArgumentException("No sponsor found with that ID in the system.");
            }
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Remove a sponsor for mock accessor
        /// </summary>
        /// <param name="SponsorID"></param>
        public void DeleteSponsor(int SponsorID)
        {
            try
            {
                SelectSponsor(SponsorID);
            }
            catch (Exception)
            {
                throw;
            }
            _sponsor.Remove(_sponsor.Find(x => x.SponsorID == SponsorID));
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/20
        /// 
        /// Select all sponsor for mock accessor
        /// </summary>
        public List<Sponsor> SelectAllSponsors()
        {
            return _sponsor;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Remove all sponsors for mock accessor
        /// </summary>
        /// <param name="SponsorID"></param>
        public List<BrowseSponsor> SelectAllVMSponsors()
        {

            return _browseSponsor;

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Select sponsor for mock accessor
        /// </summary>
        /// <param name="SponsorID"></param>
        /// <returns></returns>
        public Sponsor SelectSponsor(int SponsorID)
        {
            Sponsor eSponsor = new Sponsor();
            eSponsor = _sponsor.Find(x => x.SponsorID == SponsorID);
            if (eSponsor == null)
            {
                throw new ArgumentException("SponsorID did not match");
            }
            return eSponsor;

        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Select sposor status for mock accessor
        /// </summary>
        /// <returns></returns>
        public List<string> SelectAllSponsorStatus()
        {
            return _statusID;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Select all states for mock accessor
        /// </summary>
        /// <returns></returns>
        public List<string> SelectAllStates()
        {
            return _states;
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created Date: 2019/02/28
        /// 
        /// Update sponsor for mock accessor
        /// </summary>
        /// <param name="oldSponsor"></param>
        /// <param name="newSponsor"></param>
        public int UpdateSponsor(Sponsor oldSponsor, Sponsor newSponsor) { 


            int rowsAffected = 0;
            foreach (var eSponsor in _sponsor)
            {
                if (eSponsor.SponsorID == oldSponsor.SponsorID)
                {
                    ++rowsAffected;
                    eSponsor.Name = newSponsor.Name;
                    eSponsor.Address = newSponsor.Address;
                    eSponsor.City = newSponsor.City;
                    eSponsor.State = newSponsor.State;
                    eSponsor.PhoneNumber = newSponsor.PhoneNumber;
                    eSponsor.Email = newSponsor.Email;
                    eSponsor.ContactFirstName = newSponsor.ContactFirstName;
                    eSponsor.ContactLastName = newSponsor.ContactLastName;
                    eSponsor.StatusID = newSponsor.StatusID;
                    eSponsor.DateAdded = newSponsor.DateAdded;
                    eSponsor.Active = newSponsor.Active;
                }
            }
            return rowsAffected;
        }
    }
}
