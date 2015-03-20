using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using SearchWithTabsApp.Pages;
using SearchWithTabsApp.WinPhone.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(TabbedView), typeof(TabbedViewRenderer))]

namespace SearchWithTabsApp.WinPhone.Extensions
{
    public class TabbedViewRenderer : ViewRenderer<TabbedView, Pivot>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            var pivot = new Pivot
            {
                Title = "Tabs Demo",
                Background = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };
            foreach (var tab in e.NewElement.Children.OfType<TabView>())
            {
                object content = null;
                if (tab.Content != null)
                {
                    var c = RendererFactory.GetRenderer(tab.Content);
                    c.SetElement(tab.Content);
                    content = c.ContainerElement;
                }
                pivot.Items.Add(new PivotItem { Header = tab.Title, Content = content });
            }

            SetNativeControl(pivot);
        }
    }
}