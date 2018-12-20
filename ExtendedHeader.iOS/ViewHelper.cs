using System;
using CoreGraphics;
using ExtendedHeader.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ViewHelper))]
namespace ExtendedHeader.iOS
{
    public class ViewHelper : IViewHelper
    {
        public (double X, double Y) GetScreenCoordinates(View view)
        {
            var renderer = Platform.GetRenderer(view);

            UIView nativeView = (UIView)renderer;
            var result = nativeView.ConvertPointToView(nativeView.Frame.Location, nativeView.GetTopView());

            return (result.X, result.Y);
        }
    }

    public static class Extensions
    {
        public static UIView GetTopView(this UIView element)
        {
            if(element.Superview != null)
                return element.Superview.GetTopView();

            return element;
        }
    }
}