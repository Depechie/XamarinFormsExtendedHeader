using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using ExtendedHeader.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace ExtendedHeader.Droid
{
    //public static class Extensions
    //{
    //    public static List<Android.Views.View> GetViewsByType(this Android.Views.View view, Type viewType = null)
    //    {
    //        if (!(view is ViewGroup group))
    //            return new List<Android.Views.View>();

    //        var result = new List<Android.Views.View>();

    //        for (int i = 0; i < group.ChildCount; i++)
    //        {
    //            var child = group.GetChildAt(i);
    //            result.AddRange(child.GetViewsByType(viewType));

    //            if (viewType == null || child.GetType() == viewType || child.GetType().BaseType == viewType)
    //                result.Add(child);
    //        }

    //        return result.Distinct().ToList();
    //    }
    //}

    public class CustomContentPageRenderer : PageRenderer
    {
        private readonly Android.Views.View _rootView;
        private int _pageLayoutCount = 0;

        public CustomContentPageRenderer(Context context) : base(context)
        {
            _rootView = CrossCurrentActivity.Current.Activity.Window.DecorView.RootView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            _rootView.ViewTreeObserver.GlobalLayout += ViewTreeObserver_GlobalLayout;
        }

        private void ViewTreeObserver_GlobalLayout(object sender, EventArgs e)
        {
            ++_pageLayoutCount;

            if (_pageLayoutCount == 2)
            {
                //There are 2 page cycles for rendering a page in Android, first empty, second with all controls loaded
                _rootView.ViewTreeObserver.GlobalLayout -= ViewTreeObserver_GlobalLayout;
                MessagingCenter.Send<LoadedMessage, bool>(new LoadedMessage(), string.Empty, true);
            }
        }
    }
}