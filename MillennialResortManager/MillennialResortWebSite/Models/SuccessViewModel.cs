using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MillennialResortWebSite.Models
{
    public class SuccessViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        public string Time { get; set; }

        public string Type { get; set; }

        public string ButtonMessage { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }

        public SuccessViewModel (string Title, string dateTime, string time, string type, string ButtonMessage, string ReturnController, string ReturnAction)
        {
            this.Title = Title;
            this.StartDate = dateTime;
            this.Time = time;
            this.Type = type;
            this.ButtonMessage = ButtonMessage;
            this.ReturnController = ReturnController;
            this.ReturnAction = ReturnAction;
        }
    }
}