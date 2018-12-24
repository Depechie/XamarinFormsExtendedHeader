using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExtendedHeader
{
    public partial class MainPage : ContentPage
    {
        private double _titleLabelLeft;
        private double _titleLabelXFactor;

        private double _opacityFactor = 0.0; //= 1.0 / 140.0;

        private double yStartingPoint = 0.0;
        private double yEndPoint = 0.0;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            MessagingCenter.Subscribe<LoadedMessage, bool>(this, string.Empty, (message, result) =>
            {
                if(result)
                {
                    //System.Diagnostics.Debug.WriteLine($"Y starting position screen 1: {DependencyService.Get<IViewHelper>().GetScreenCoordinates(AnchorBox)}");
                    //System.Diagnostics.Debug.WriteLine($"Y starting position screen 2: {DependencyService.Get<IViewHelper>().GetScreenCoordinates(AnchorBoxHeader)}");
                    //System.Diagnostics.Debug.WriteLine($"Y starting position screen 3: {DependencyService.Get<IViewHelper>().GetScreenCoordinates(HeaderGrid)}");
                    //System.Diagnostics.Debug.WriteLine($"Y starting position screen 3: {DependencyService.Get<IViewHelper>().GetScreenCoordinates(BaseHeaderGrid)}");
                    //System.Diagnostics.Debug.WriteLine($"{DeviceDisplay.MainDisplayInfo.Density}");

                    yStartingPoint = DependencyService.Get<IViewHelper>().GetScreenCoordinates(AnchorBox).Y;
                    yEndPoint = DependencyService.Get<IViewHelper>().GetScreenCoordinates(AnchorBoxHeader).Y;

                    if(Device.RuntimePlatform == Device.Android)
                    {
                        yStartingPoint = (yStartingPoint / DeviceDisplay.MainDisplayInfo.Density);
                        yEndPoint = (yEndPoint / DeviceDisplay.MainDisplayInfo.Density);
                    }

                    if(Device.RuntimePlatform == Device.iOS)
                    {
                        yStartingPoint = 200;
                        yEndPoint = 60;
                    }

                    System.Diagnostics.Debug.WriteLine($"{yStartingPoint} - {yEndPoint} - {yStartingPoint - yEndPoint}");

                    _opacityFactor = 1 / (yStartingPoint - yEndPoint);
                }
            });

            ContentScrollView.PropertyChanged += OnContentScrollViewPropertyChanged;
        }

        private void OnContentScrollViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(ScrollView.ScrollYProperty.PropertyName))
                AnimateElements();
        }

        private void AnimateElements()
        {
            var yCoordinate = DependencyService.Get<IViewHelper>().GetScreenCoordinates(AnchorBox).Y;

            if (Device.RuntimePlatform == Device.Android)
                yCoordinate = (yCoordinate / DeviceDisplay.MainDisplayInfo.Density);

            System.Diagnostics.Debug.WriteLine($"Y starting position screen: {yCoordinate}");

            if (yCoordinate > yStartingPoint)
                yCoordinate = yStartingPoint;

            if (yCoordinate < yEndPoint)
                yCoordinate = yEndPoint;

            HeaderRowDefinition.Height = yCoordinate;
            HeaderImage.Opacity = 1 - ((yStartingPoint - yCoordinate) * _opacityFactor);
        }
    }
}