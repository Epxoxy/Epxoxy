using System;
namespace Epxoxy.Controls
{
    public interface INotifyItem
    {
        object ToastContent { get; set; }
        string ToastTitle { get; set; }
        string Description { get; set; }
        object Thumb { get; set; }
        System.Windows.Input.ICommand Command { get; set; }
    }
}
