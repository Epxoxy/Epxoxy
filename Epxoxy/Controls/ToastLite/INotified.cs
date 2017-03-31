using System;
namespace Epxoxy.Controls
{
    public interface INotified
    {
        object ToastContent { get; set; }
        string ToastTitle { get; set; }
        string Description { get; set; }
        object Thumb { get; set; }
        DateTime NotifiedTime { get; set; }
        System.Windows.Input.ICommand Command { get; set; }
    }
}
