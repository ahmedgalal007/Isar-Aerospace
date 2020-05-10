using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Isar_Aerospace
{
    /// <summary>
    /// Interaction logic for Splach.xaml
    /// </summary>
    public partial class Splach : Window
    {
        public int Interval { get; set; } = 1;
        public double WOpacity { get; set; } = 1;
        MainWindow mainWindow;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public Splach()
        {
            InitializeComponent();

            
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            WOpacity = 0.2;
            media.Stop();

        }

        


    private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (bbLoader.Value < 100)
            {
                bbLoader.Value += 1;
            }
            else if(media.Visibility != Visibility.Visible)
            {
                media.Visibility = Visibility.Visible;
                logo.Visibility = Visibility.Visible;
                media.Play();
                bbLoader.Visibility = Visibility.Hidden;
                media.Source = new Uri(Environment.CurrentDirectory + "/sunrise.mp4");
                image.Visibility = Visibility.Hidden;
            }

            else if( mainWindow != null)
            {
                WOpacity += 0.04;
                mainWindow.Opacity = WOpacity;
            }
            
            
            if (WOpacity >= 1 )
            {
                dispatcherTimer.Stop();
                this.Close();
            }

           
            Interval += 1;
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();

        }
    }
}
