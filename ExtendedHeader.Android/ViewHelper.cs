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

            return (result[0], result[1]);
        }
    }
}