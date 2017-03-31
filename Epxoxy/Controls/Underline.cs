//The MIT License(MIT)
//
//Copyright(c) James Willock, Mulholland Software and Contributors
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

//See https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/blob/14f60a4b99dcb3f369f6f0900fb57a3339b13c78/MaterialDesignThemes.Wpf/Underline.cs

using System.Windows;
using System.Windows.Controls;

namespace Epxoxy.Controls
{
    /// <summary>
    /// Underline
    /// </summary>
    [TemplateVisualState(GroupName = "ActivationStates", Name = ActiveStateName)]
    [TemplateVisualState(GroupName = "ActivationStates", Name = InactiveStateName)]
    public class Underline : Control
    {
        public const string ActiveStateName = "Active";
        public const string InactiveStateName = "Inactive";
        private string SelectStateName => IsActive ? ActiveStateName : InactiveStateName;

        static Underline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Underline), new FrameworkPropertyMetadata(typeof(Underline)));
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive), typeof(bool), typeof(Underline),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, IsActivePropertyChangedCallback));

        private static void IsActivePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((Underline)dependencyObject).GotoVisualState(true);//!TransitionAssist.GetDisableTransitions(dependencyObject));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GotoVisualState(false);
        }

        private void GotoVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, SelectStateName, useTransitions);
        }

    }
}
