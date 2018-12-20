using System;
using Xamarin.Forms;

namespace ExtendedHeader
{
    public interface IViewHelper
    {
        (double X, double Y) GetScreenCoordinates(View view);
    }
}