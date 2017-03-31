using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Epxoxy.Controls
{
    public class ComboBoxHelper : DependencyObject
    {
        public static Thickness GetPopupThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PopupThicknessProperty);
        }

        public static void SetPopupThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PopupThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for PopupThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupThicknessProperty =
            DependencyProperty.RegisterAttached("PopupThickness", typeof(Thickness), typeof(ComboBoxHelper), new PropertyMetadata(new Thickness(1, 0, 1, 1)));


        public static PlacementMode GetPopupPlacement(DependencyObject obj)
        {
            return (PlacementMode)obj.GetValue(PopupPlacementProperty);
        }

        public static void SetPopupPlacement(DependencyObject obj, PlacementMode value)
        {
            obj.SetValue(PopupPlacementProperty, value);
        }

        // Using a DependencyProperty as the backing store for PopupPlacement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupPlacementProperty =
            DependencyProperty.RegisterAttached("PopupPlacement", typeof(PlacementMode), typeof(ComboBoxHelper), new PropertyMetadata(PlacementMode.Bottom));

        public static bool GetDisableShadow(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableShadowProperty);
        }

        public static void SetDisableShadow(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableShadowProperty, value);
        }

        public static readonly DependencyProperty DisableShadowProperty =
            DependencyProperty.RegisterAttached("DisableShadow", typeof(bool), typeof(ComboBoxHelper), new PropertyMetadata(false));

        public static Style GetEditTextBoxStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(EditTextBoxStyleProperty);
        }

        public static void SetEditTextBoxStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(EditTextBoxStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for EditTextBoxStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTextBoxStyleProperty =
            DependencyProperty.RegisterAttached("EditTextBoxStyle", typeof(Style), typeof(ComboBoxHelper), new PropertyMetadata(null));


    }
}
