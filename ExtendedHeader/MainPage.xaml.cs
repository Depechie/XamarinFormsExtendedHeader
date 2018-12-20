using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExtendedHeader
{
    public partial class MainPage : ContentPage
    {
        private double _titleLabelLeft;
        private double _titleLabelXFactor;
        private double _opacityFactor = 1.0 / 140.0;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            ContentScrollView.PropertyChanged += OnContentScrollViewPropertyChanged;
            TitleLabel.SizeChanged += OnTitleLabelSizeChanged;
        }

        private void OnTitleLabelSizeChanged(object sender, EventArgs e)
        {
            TitleLabel.SizeChanged -= OnTitleLabelSizeChanged;

            _titleLabelLeft = TitleLabel.X;
            _titleLabelXFactor = ((Width / 2) - ((TitleLabel.Width + _titleLabelLeft) / 2)) / 140;
        }

        private void OnContentScrollViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(ScrollView.ScrollYProperty.PropertyName))
                AnimateElements();
        }

        private void AnimateElements()
        {
            var boxCoordinates = DependencyService.Get<IViewHelper>().GetScreenCoordinates(AnchorBox);
            var yCoordinate = Device.RuntimePlatform == Device.Android ? boxCoordinates.Y / DeviceDisplay.MainDisplayInfo.Density : boxCoordinates.Y;

            System.Diagnostics.Debug.WriteLine($"Y position screen: {yCoordinate}");

            if (yCoordinate > 200)
                yCoordinate = 200;

            if (yCoordinate < 60)
                yCoordinate = 60;

            double scrolled = (200 - yCoordinate);

            System.Diagnostics.Debug.WriteLine($"Scrolled: {scrolled}");

            HeaderRowDefinition.Height = yCoordinate;
            HeaderImage.Opacity = 1 - (scrolled * _opacityFactor);
        }
    }
}