using ExtendedHeader.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace ExtendedHeader.iOS
{
    public class CustomContentPageRenderer : PageRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            MessagingCenter.Send<LoadedMessage, bool>(new LoadedMessage(), string.Empty, true);
        }
    }
}