using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerDialog.xaml
    /// </summary>
    public partial class ColorPickerDialog : Window
    {
        List<Color> ColorList { get; set; }
        public Color SelectedColor { get; set; }
        public ColorPickerDialog()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
        }

        private void MouseLeftButtonDownDragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            var pickerpaneSelected = pickerPane.SelectedColor;
            if (pickerpaneSelected.HasValue)
            {
                SelectedColor = pickerpaneSelected.Value;
                DialogResult = true;
                this.Close();
            }
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
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
            if (OKBtn != null)
                OKBtn.IsEnabled = pickerPane.SelectedColor.HasValue;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            pickerPane.SelectedColor = null;
        }
    }
}
