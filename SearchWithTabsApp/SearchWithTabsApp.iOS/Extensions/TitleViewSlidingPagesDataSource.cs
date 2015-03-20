using System;
using System.Collections.Generic;
using SlidingPages.Bindings;
using UIKit;

namespace SearchWithTabsApp.iOS.Extensions
{
    public class TitleViewSlidingPagesDataSource : TTSlidingPagesDataSource
    {
        private readonly object sync = new object();
        private readonly List<string> viewNames;
        private readonly List<UIView> views;

        public TitleViewSlidingPagesDataSource()
        {
            views = new List<UIView>();
            viewNames = new List<string>();
        }

        public void AddView(UIView view, string title, UIView addAfterView = null)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            if (title == null)
                throw new ArgumentNullException("title");

            lock (sync)
            {
                if (views.Contains(view))
                    return;

                if (addAfterView == null)
                {
                    views.Add(view);
                    viewNames.Add(title.ToUpper());
                }
                else
                {
                    var index = views.IndexOf(addAfterView) + 1;
                    views.Insert(index, view);
                    viewNames.Insert(index, title.ToUpper());
                }
            }
        }

        public void RemoveView(UIView view)
        {
            var index = views.IndexOf(view);
            if (index < 0)
                return;
            lock (sync)
            {
                views.RemoveAt(index);
                viewNames.RemoveAt(index);
            }
        }

        public override nint NumberOfPagesForSlidingPagesViewController(TTScrollSlidingPagesController source)
        {
            return views.Count;
        }

        public override TTSlidingPageTitle TitleForSlidingPagesViewController(TTScrollSlidingPagesController source,
            nint index)
        {
            if (index >= views.Count)
                return null;

            return new TTSlidingPageTitle(viewNames[(int)index]);
        }

        public override TTSlidingPage PageForSlidingPagesViewController(TTScrollSlidingPagesController source, nint index)
        {
            if (index >= views.Count)
                return null;

            return new TTSlidingPage(views[(int)index]);
        }
    }
}