using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MillennialResortWebSite.Models
{
    /// <summary>
    /// Added by Matt H. on 4/26/19
    /// View Model class comprised of both MemberTab and MeberTabLine data objects needed to 
    /// populate the view with the needed data.
    /// </summary>
    public class ViewTabMixer
    {
        public MemberTab MemberTab { get; set; }

        public List<MemberTabLine> MemberTabLines { get; set; }
    }
}