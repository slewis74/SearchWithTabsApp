using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V13.App;
using Android.Support.V4.View;
using Android.Widget;
using SearchWithTabsApp.Droid.Extensions;
using SearchWithTabsApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TabbedView), typeof(TabbedViewRenderer))]

namespace SearchWithTabsApp.Droid.Extensions
{
    public class TabbedViewRenderer : ViewRenderer<TabbedView, Android.Views.View>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;
            
            var pivot = new PagerSlidingTabStrip.PagerSlidingTabStrip(Context);

            var viewPager = new ViewPager(Context);
            viewPager.Adapter = new TabbedDataAdapter(((Activity)Context).FragmentManager);

            var linearLayout = new LinearLayout(Context);
            linearLayout.AddView(pivot);
            linearLayout.AddView(viewPager);

            pivot.SetViewPager(viewPager);

            SetNativeControl(linearLayout);
        }
    }

    public class TabbedDataAdapter : FragmentStatePagerAdapter
    {
        readonly List<Tuple<string, Fragment>> _pagerFragments = new List<System.Tuple<string, Fragment>>();

        public TabbedDataAdapter(FragmentManager fm)
            : base(fm)
        {
            BuildPageFragments();
        }

        private async void BuildPageFragments()
        {
            _pagerFragments.Clear();

            var allFragment = new Fragment();
            allFragment.Arguments = new Bundle();
            _pagerFragments.Add(new System.Tuple<string, Fragment>("All", allFragment));

            NotifyDataSetChanged();
        }

        public override int Count
        {
            get { return _pagerFragments.Count; }
        }

        public override Fragment GetItem(int position)
        {
            return _pagerFragments[position].Item2;
        }
    }
}