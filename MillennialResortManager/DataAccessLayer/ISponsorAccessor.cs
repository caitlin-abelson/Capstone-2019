using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Gunardi Saputra
    /// Created : 2019/02/20
    /// The ISponsorAccessor is an interface for talking to the data
    /// </summary>
    public interface ISponsorAccessor
    {
        void InsertSponsor(Sponsor newSponsor);
        Sponsor SelectSponsor(int SponsorID);
        List<Sponsor> SelectAllSponsors();
        List<string> SelectAllStates();
        List<string> SelectAllSponsorStatus();

        List<BrowseSponsor> SelectAllVMSponsors();
        int UpdateSponsor(Sponsor oldSponsor, Sponsor newSponsor);
        void DeactivateSponsor(int SponsorID);//run
        void DeleteSponsor(int SponsorID);//run
    }
}
