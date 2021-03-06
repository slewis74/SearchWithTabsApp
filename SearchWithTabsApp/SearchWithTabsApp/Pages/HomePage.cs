﻿using Xamarin.Forms;

namespace SearchWithTabsApp.Pages
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Title = "Home Page";
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);

            var searchBar = new SearchBar
            {
                Placeholder = "Search"
            };

            var label1 = new Label { Text = "Tab 1", TextColor = Color.Red, FontSize = 40 };

            var tabbedView = new TabbedView
            {
                Children =
                {
                    new TabView { Title = "All", Content = label1 },
                    new TabView { Title = "Categery 1"},
                    new TabView { Title = "Categery 2"}
                }
            };

            var grid = new Grid
            {
                Children = {
                    searchBar,
                    tabbedView
                }
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(searchBar, 0);
            Grid.SetRow(tabbedView, 1);

            Content = grid;
        }
    }

    public class TabbedView : Grid
    {
    }

    public class TabView : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof (string), typeof (TabView), (object) string.Empty);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, (object)value); }
        }
       
    }
}
