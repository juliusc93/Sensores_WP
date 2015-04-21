using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;
using System.Device.Location;
using Microsoft.Xna.Framework;

namespace WP_Sensors_Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        Accelerometer acc;
        GeoCoordinateWatcher gps;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            acc = new Accelerometer();
            acc.ReadingChanged += acc_ReadingChanged;
            acc.Start();
            gps = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            gps.PositionChanged += gps_PositionChanged;
            gps.MovementThreshold = 10;
            gps.Start();
        }

        void gps_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Dispatcher.BeginInvoke(() => UpdateGPSData(e.Position.Location));
        }

        void UpdateGPSData(GeoCoordinate gc)
        {
            latValue.Text = gc.Latitude.ToString("0.000");
            longValue.Text = gc.Longitude.ToString("0.000");
            altValue.Text = gc.Altitude.ToString("0.000");
        }

        void acc_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            Dispatcher.BeginInvoke(() => UpdateAccData(e));
        }

        void UpdateAccData(AccelerometerReadingEventArgs e)
        {
            txtXvalue.Text = e.X.ToString("0.000") + "g";
            txtYvalue.Text = e.Y.ToString("0.000") + "g";
            txtZvalue.Text = e.Z.ToString("0.000") + "g";
            if (txtZvalue.Text.Equals("NeuN")) txtZvalue.Text = "No disponible";
        }
    }
}