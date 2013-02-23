using System;
using Caliburn.Micro;

namespace Client.Views
{
    public class ErrorDialogViewModel : IHaveDisplayName
    {
        public Exception Exception { get; set; }

        /// <summary>
        /// Used as the title of the window.
        /// </summary>
        public string DisplayName { get; set; }

        public string ErrorMessage 
        {
            get { return Exception.ToString(); }
        }
    }
}