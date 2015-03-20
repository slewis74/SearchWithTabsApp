using System.Linq;
using SearchWithTabsApp.iOS.Extensions;
using SearchWithTabsApp.Pages;
using SlidingPages.Bindings;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedView), typeof(TabbedViewRenderer))]

namespace SearchWithTabsApp.iOS.Extensions
{
    public class TabbedViewRenderer : ViewRenderer<TabbedView, UIView>
    {
        private TTScrollSlidingPagesController _pivot;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            var z = UIApplication.SharedApplication.Windows[0].RootViewController;

            _pivot = new TTScrollSlidingPagesController
            {
                DisableUIPageControl = true,
                TitleScrollerTextColour = UIColor.Yellow,
                TitleScrollerBottomEdgeColour = UIColor.Yellow,
                TitleScrollerInActiveTextColour = UIColor.FromRGB(128, 128, 128),
                DisableTitleScrollerShadow = true
            };
            var dataSourcePages = new TitleViewSlidingPagesDataSource();
            _pivot.DataSource = dataSourcePages;

            foreach (var tab in e.NewElement.Children.OfType<TabView>())
            {
                UIViewController controller = null;
                if (tab.Content != null)
                {
                    var c = RendererFactory.GetRenderer(tab.Content);
                    c.SetElement(tab.Content);

                    controller = new UIViewController();
                    controller.Add(c.NativeView);
                }
                else
                {
                    controller = new UIViewController();
                }

                dataSourcePages.AddView(controller.View, tab.Title);
            }

            z.AddChildViewController(_pivot);

            SetNativeControl(_pivot.View);
        }
    }
}