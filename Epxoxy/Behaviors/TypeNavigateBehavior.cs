using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

namespace Epxoxy.Behaviors
{
    public class TypeNavigateBehavior : Behavior<System.Windows.Controls.Frame>
    {
        public Type NavigateTarget
        {
            get { return (Type)GetValue(NavigateTargetProperty); }
            set { SetValue(NavigateTargetProperty, value); }
        }
        public static readonly DependencyProperty NavigateTargetProperty =
            DependencyProperty.Register("NavigateTarget", typeof(Type), typeof(TypeNavigateBehavior), new PropertyMetadata(null, OnNavigateTargetChanged));

        private Dictionary<Type, object> navigatedList = new Dictionary<Type, object>();
        private System.Windows.Controls.Frame AssociatedFrame => AssociatedObject;
        private bool keepAlive;

        private static void OnNavigateTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviors = d as TypeNavigateBehavior;
            if (behaviors != null) behaviors.Navigate();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedFrame.Navigated += OnAssociatedFrameNavigated;
            keepAlive = System.Windows.Navigation.JournalEntry.GetKeepAlive(AssociatedFrame);
            Navigate();
        }

        protected override void OnDetaching()
        {
            navigatedList.Clear();
            navigatedList = null;
            AssociatedFrame.Navigated -= OnAssociatedFrameNavigated;
            base.OnDetaching();
        }

        private void Navigate()
        {
            if (NavigateTarget == null) return;
            if (AssociatedFrame == null) return;
            if (keepAlive)
            {
                if (!navigatedList.ContainsKey(NavigateTarget))
                {
                    object targetObj = Activator.CreateInstance(NavigateTarget);
                    navigatedList.Add(NavigateTarget, targetObj);
                    AssociatedFrame.Navigate(targetObj);
                }
                else
                {
                    AssociatedFrame.Navigate(navigatedList[NavigateTarget]);
                    Helpers.DebugHelper.debugWrite(this, "TypeNavigateMsg : Exist object!");
                }
            }
            else
            {
                AssociatedFrame.Navigate(Activator.CreateInstance(NavigateTarget));
            }
        }

        private void OnAssociatedFrameNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var obj = e.Content;
            if (keepAlive)
            {
                var key = navigatedList.FirstOrDefault(x => x.Value == obj).Key;
                if (NavigateTarget != key) NavigateTarget = key;
            }
        }
    }
}
