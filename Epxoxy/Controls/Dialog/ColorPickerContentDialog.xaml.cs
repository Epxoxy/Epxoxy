using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Epxoxy.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerContentDialog.xaml
    /// </summary>
    public partial class ColorPickerContentDialog : ContentDialog
    {
        public Color SelectedColor { get; set; }
        public ColorPickerContentDialog()
        {
            InitializeComponent();
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            var pickerpaneSelected = pickerPane.SelectedColor;
            if (pickerpaneSelected.HasValue)
            {
                SelectedColor = pickerpaneSelected.Value;
            }
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
        }

        private void BasicModeCheck(object sender, RoutedEventArgs e)
        {
            pickerPane.Mode = ColorSelectMode.Basic;
        }

        private void CustomModeCheck(object sender, RoutedEventArgs e)
        {
            pickerPane.Mode = ColorSelectMode.Custom;
        }

        private void PickerPaneSelectedColorChanged(object sender, RoutedEventArgs e)
        {
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            pickerPane.SelectedColor = null;
        }
    }
}
