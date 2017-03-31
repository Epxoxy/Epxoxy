using System;
using System.Windows;

namespace NOFiveControls
{
    /// <summary>
    /// Alert Result
    /// </summary>
    public enum AlertResult
    {
        OK = 1,
        Cancel = 2,
        Click = 3,
    }

    public class Alert
    {

        #region Rewrite Alert

        /// <summary>
        /// Show AlertBox that will auto close
        /// </summary>
        /// <param name="content">Alert Content</param>
        public static void ShowOnly(object content, Window owner = null)
        {
            AlertBox alertbox = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox = new AlertBox();
            }));
            alertbox.Owner = owner == null ? Application.Current.MainWindow : owner;
            alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            alertbox.AlertContent = content;
            alertbox.OkCancelButtonVisibility = Visibility.Collapsed;
            alertbox.AutoClose = true;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox.ShowDialog();
            }));
        }


        /// <summary>
        /// Show AlertBox and get Ok/Cancel result
        /// </summary>
        /// <param name="content">Alert Content</param>
        /// <returns></returns>
        public static AlertResult Show(object content, Window owner = null)
        {
            AlertBox alertbox = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox = new AlertBox();
            }));
            alertbox.Owner = owner == null ? Application.Current.MainWindow : owner;
            alertbox.AlertContent = content;
            alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox.ShowDialog();
            }));
            return alertbox.Result;
        }
        /// <summary>
        /// Show AlertBox and get Ok/Cancel result
        /// </summary>
        /// <param name="title">Alert Title</param>
        /// <param name="content">Alert Content</param>
        /// <returns></returns>
        public static AlertResult Show(string title, object content, Window owner = null)
        {
            AlertBox alertbox = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox = new AlertBox();
            }));
            alertbox.Owner = owner == null ? Application.Current.MainWindow : owner;
            alertbox.AlertTitle = title;
            alertbox.AlertContent = content;
            alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox.ShowDialog();
            }));
            return alertbox.Result;
        }

        /// <summary>
        /// Show AlertBox with selection menu
        /// </summary>
        /// <param name="content">Alert Content</param>
        /// <param name="okMenus">When user click ok to show</param>
        /// <returns></returns>
        public static int Show(object content, System.Collections.Generic.List<string> okMenus, Window owner = null)
        {
            AlertBox alertbox = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox = new AlertBox();
            }));
            alertbox.Owner = owner == null ? Application.Current.MainWindow : owner;
            alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            alertbox.AlertContent = content;
            alertbox.OKMenus = okMenus;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox.ShowDialog();
            }));
            return alertbox.SelectedIndex;
        }
        /// <summary>
        /// Show AlertBox with selection menu
        /// </summary>
        /// <param name="title">Alert Title</param>
        /// <param name="content">Alert Content</param>
        /// <param name="okMenus">When user click ok to show</param>
        /// <returns></returns>
        public static int Show(string title, object content, System.Collections.Generic.List<string> okMenus, Window owner = null)
        {
            AlertBox alertbox = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox = new AlertBox();
            }));
            alertbox.Owner = owner == null ? Application.Current.MainWindow : owner;
            alertbox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            alertbox.AlertTitle = title;
            alertbox.AlertContent = content;
            alertbox.OKMenus = okMenus;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                alertbox.ShowDialog();
            }));
            return alertbox.SelectedIndex;
        }

        #endregion

    }
}
