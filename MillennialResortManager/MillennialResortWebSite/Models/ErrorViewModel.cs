using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MillennialResortWebSite.Models
{
    /// <summary>
    /// Wes Richardson
    /// Created: 2019/04/25
    /// 
    /// To be used for the Error Page for the Website
    /// </summary>
    public class ErrorViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ImageLocation { get; set; }
        public string ImageAlt { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public string ButtonMessage { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/25
        /// 
        /// Error View Model to be used without a Image
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="ExceptionMessage"></param>
        /// <param name="ButtonMessage"></param>
        /// <param name="ReturnController"></param>
        /// <param name="ReturnAction"></param>
        //public ErrorViewModel(string Title, string Message, string ExceptionMessage, string ButtonMessage, string ReturnController, string ReturnAction)
        //{
        //    this.Title = Title;
        //    this.Message = Message;
        //    this.ExceptionMessage = ExceptionMessage;
        //    ImageLocation = "";
        //    ImageAlt = "";
        //    ImageHeight = 0;
        //    ImageWidth = 0;
        //    this.ButtonMessage = ButtonMessage;
        //    this.ReturnController = ReturnController;
        //    this.ReturnAction = ReturnAction;
        //}

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/04/25
        /// 
        /// Error View Model to be used with a Image
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="ExceptionMessage"></param>
        /// <param name="ImageLocation"></param>
        /// <param name="ImageAlt"></param>
        /// <param name="ImageHeight"></param>
        /// <param name="ImageWidth"></param>
        /// <param name="ButtonMessage"></param>
        /// <param name="ReturnController"></param>
        /// <param name="ReturnAction"></param>
        public ErrorViewModel(string Title, string Message, string ExceptionMessage, string ImageLocation, string ImageAlt,
            int ImageHeight, int ImageWidth, string ButtonMessage, string ReturnController, string ReturnAction)
        {
            this.Title = Title;
            this.Message = Message;
            this.ExceptionMessage = ExceptionMessage;
            this.ImageLocation = ImageLocation;
            this.ImageAlt = ImageAlt;
            this.ImageHeight = ImageHeight;
            this.ImageWidth = ImageWidth;
            this.ButtonMessage = ButtonMessage;
            this.ReturnController = ReturnController;
            this.ReturnAction = ReturnAction;
        }
    }
}