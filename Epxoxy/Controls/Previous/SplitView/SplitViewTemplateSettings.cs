using System.Collections.Generic;
using System.Windows;

namespace Epxoxy.Controls.Previous
{
    public class SplitViewTemplateSettings : DependencyObject
    {
        public double CompactToOpenLength
        {
            get { return (double)GetValue(CompactToOpenLengthProperty); }
            set { SetValue(CompactToOpenLengthProperty, value); }
        }
        public static readonly DependencyProperty CompactToOpenLengthProperty =
            DependencyProperty.Register("CompactToOpenLength", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));

        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }
        public static readonly DependencyProperty OpenPaneLengthProperty =
            DependencyProperty.Register("OpenPaneLength", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));

        public double NegativePaneLength
        {
            get { return (double)GetValue(NegativePaneLengthProperty); }
            set { SetValue(NegativePaneLengthProperty, value); }
        }
        public static readonly DependencyProperty NegativePaneLengthProperty =
            DependencyProperty.Register("NegativePaneLength", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));

        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }
        public static readonly DependencyProperty CompactPaneLengthProperty =
            DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));
        
        public Thickness AdaptOpenMargin
        {
            get { return (Thickness)GetValue(AdaptOpenMarginProperty); }
            set { SetValue(AdaptOpenMarginProperty, value); }
        }
        public static readonly DependencyProperty AdaptOpenMarginProperty =
            DependencyProperty.Register("AdaptOpenMargin", typeof(Thickness), typeof(SplitViewTemplateSettings), 
                new PropertyMetadata(default(Thickness), OnPropertyChanged));
        
        public Thickness AdaptCompactToOpenMargin
        {
            get { return (Thickness)GetValue(AdaptCompactToOpenMarginProperty); }
            set { SetValue(AdaptCompactToOpenMarginProperty, value); }
        }
        public static readonly DependencyProperty AdaptCompactToOpenMarginProperty =
            DependencyProperty.Register("AdaptCompactToOpenMargin", typeof(Thickness), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(default(Thickness), OnPropertyChanged));
        
        public double TranslateCompactToOpenLength
        {
            get { return (double)GetValue(TranslateCompactToOpenLengthProperty); }
            set { SetValue(TranslateCompactToOpenLengthProperty, value); }
        }
        public static readonly DependencyProperty TranslateCompactToOpenLengthProperty =
            DependencyProperty.Register("TranslateCompactToOpenLength", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));

        public double TranslateOverlayFrom
        {
            get { return (double)GetValue(TranslateOverlayFromProperty); }
            set { SetValue(TranslateOverlayFromProperty, value); }
        }
        public static readonly DependencyProperty TranslateOverlayFromProperty =
            DependencyProperty.Register("TranslateOverlayFrom", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));

        public double NegativeTranslateOverlayFrom
        {
            get { return (double)GetValue(NegativeTranslateOverlayFromProperty); }
            set { SetValue(NegativeTranslateOverlayFromProperty, value); }
        }
        public static readonly DependencyProperty NegativeTranslateOverlayFromProperty =
            DependencyProperty.Register("NegativeTranslateOverlayFrom", typeof(double), typeof(SplitViewTemplateSettings),
                new PropertyMetadata(0d, OnPropertyChanged));
        
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var templateSettings = d as SplitViewTemplateSettings;
            if (templateSettings != null)
            {
                templateSettings.OnPropertyChanged(e.Property, e.NewValue);
            }
        }

        internal SplitViewTemplateSettings(SplitView owner)
        {
            this.Owner = owner;
            bindsDictionary = new Dictionary<DependencyProperty, Dictionary<DependencyProperty, List<DependencyObject>>>();
            this.Update();
        }

        internal SplitView Owner { get; }
        internal void Update()
        {
            Helpers.DebugHelper.debugWrite(this, "SplitViewTemplateSettings updated");
            this.OpenPaneLength = this.Owner.OpenPaneLength;
            this.CompactPaneLength = this.Owner.CompactPaneLength;

            this.NegativePaneLength = -this.OpenPaneLength;
            this.CompactToOpenLength = this.OpenPaneLength - this.CompactPaneLength;
            this.UpdateForPlacementChanged();
        }

        internal void UpdateForPlacementChanged()
        {
            PanePlacement newPlacement = this.Owner.PanePlacement;
            if (newPlacement == PanePlacement.Left)
            {
                TranslateOverlayFrom = this.NegativePaneLength;
                NegativeTranslateOverlayFrom = this.OpenPaneLength;
                TranslateCompactToOpenLength = this.CompactToOpenLength;
                AdaptOpenMargin = new Thickness(0, 0, this.OpenPaneLength, 0);
                AdaptCompactToOpenMargin = new Thickness(0, 0, this.CompactToOpenLength, 0);
            }
            else
            {
                TranslateOverlayFrom = this.OpenPaneLength;
                NegativeTranslateOverlayFrom = this.NegativePaneLength;
                TranslateCompactToOpenLength = -this.CompactToOpenLength;
                AdaptOpenMargin = new Thickness(this.OpenPaneLength, 0, 0, 0);
                AdaptCompactToOpenMargin = new Thickness(this.CompactToOpenLength, 0, 0, 0); ;
            }
        }

        private Dictionary<DependencyProperty, Dictionary<DependencyProperty, List<DependencyObject>>> bindsDictionary;

        internal void SetBinding(DependencyProperty dp, DependencyProperty bindTo, DependencyObject obj)
        {
            if (obj == null || dp == null || bindTo == null) return;
            if (bindTo.OwnerType != typeof(SplitViewTemplateSettings)) return;
            Dictionary<DependencyProperty, List<DependencyObject>> bindsList;
            if (!bindsDictionary.ContainsKey(bindTo))
            {
                bindsList = new Dictionary<DependencyProperty, List<DependencyObject>>();
                bindsDictionary.Add(bindTo, bindsList);
            }
            else
            {
                bindsList = bindsDictionary[bindTo];
            }
            List<DependencyObject> objList;
            if (!bindsList.ContainsKey(dp))
            {
                objList = new List<DependencyObject>();
                bindsList.Add(dp, objList);
            }
            else
            {
                objList = bindsList[dp];
            }
            object value = this.GetValue(dp);
            obj.SetValue(dp, value);
            objList.Add(obj);
        }

        internal void SetBinding(DependencyProperty dp, DependencyProperty bindTo, IList<DependencyObject> objs)
        {
            if (objs == null || objs.Count <= 0 || dp == null || bindTo == null) return;
            if (bindTo.OwnerType != typeof(SplitViewTemplateSettings)) return;
            Dictionary<DependencyProperty, List<DependencyObject>> bindsList;
            if (!bindsDictionary.ContainsKey(bindTo))
            {
                bindsList = new Dictionary<DependencyProperty, List<DependencyObject>>();
                bindsDictionary.Add(bindTo, bindsList);
            }
            else
            {
                bindsList = bindsDictionary[bindTo];
            }
            List<DependencyObject> objList;
            if (!bindsList.ContainsKey(dp))
            {
                objList = new List<DependencyObject>();
                bindsList.Add(dp, objList);
            }
            else
            {
                objList = bindsList[dp];
            }
            object value = this.GetValue(bindTo);
            for (int i = 0; i < objs.Count; ++i)
            {
                if (objs[i] == null) continue;
                objs[i].SetValue(dp, value);
                objList.Add(objs[i]);
            }
        }

        private void OnPropertyChanged(DependencyProperty src, object value)
        {
            if (bindsDictionary.ContainsKey(src))
            {
                var objBinds = bindsDictionary[src];
                foreach (var objBind in objBinds)
                {
                    DependencyProperty prop = objBind.Key;
                    List<DependencyObject> objs = objBind.Value;
                    for (int i = 0; i < objs.Count; ++i)
                    {
                        objs[i].SetValue(prop, value);
                    }
                }
            }
        }

        internal void Cleanup()
        {
            if (this.bindsDictionary != null) this.bindsDictionary.Clear();
        }
    }
}
