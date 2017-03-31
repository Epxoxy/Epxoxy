using System;

namespace Epxoxy.Controls
{
    [Serializable]
    public class ToastItem : INotified
    {
        private string description;
        private object toastContent;
        private string toastTitle = "Title";
        private object thumb;
        private DateTime notifiedTime;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public object Thumb
        {
            get
            {
                return thumb;
            }

            set
            {
                thumb = value;
            }
        }
        public object ToastContent
        {
            get
            {
                return toastContent;
            }

            set
            {
                toastContent = value;
            }
        }
        public string ToastTitle
        {
            get
            {
                return toastTitle;
            }

            set
            {
                toastTitle = value;
            }
        }
        public System.Windows.Input.ICommand Command { get; set; }

        public DateTime NotifiedTime
        {
            get
            {
                return notifiedTime;
            }

            set
            {
                notifiedTime = value;
            }
        }

        public ToastItem()
        {
            notifiedTime = DateTime.Now;
        }
    }
}
