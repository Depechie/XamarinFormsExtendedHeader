using ExtendedHeader.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ViewHelper))]
namespace ExtendedHeader.Droid
{
    public class ViewHelper : IViewHelper
    {
        public (double X, double Y) GetScreenCoordinates(Xamarin.Forms.View view)
        {
            var renderer = Platform.GetRenderer(view);

            Android.Views.View nativeView = (Android.Views.View)renderer;
            var result = new int[2];
            nativeView.GetLocationOnScreen(result);

            int statusBarHeight = -1;
            int resourceId = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
                statusBarHeight = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Resources.GetDimensionPixelSize(resourceId);

            return (result[0] - statusBarHeight, result[1] - statusBarHeight);
        }
    }
}