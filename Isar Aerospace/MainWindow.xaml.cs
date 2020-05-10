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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Isar_Aerospace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Book> books { get; set; }
        // public List<Book> books = new List<Book>();
        public List<string> bindingList { get; set; }
        public GradientStopCollection grsc { get; set; }
        // public List<string> bindingList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // WindowState = WindowState.Maximized;
            bindingList = new List<string>();
            grsc = new GradientStopCollection(3);
            grsc.Add(new GradientStop(Colors.Red, 0));
            grsc.Add(new GradientStop(Colors.Yellow, .5));
            grsc.Add(new GradientStop(Colors.Green, 1));

            gradientBox.Fill = new LinearGradientBrush(grsc);
        }


        private void btnParseDisplay_Click(object sender, RoutedEventArgs e)
        {
            string file = Utilities.OpenFile();
            //System.Windows.Forms.MessageBox.Show("File: " + file, "Message");
            List<string> comboItems = new List<string>();
            books = Utilities.ReadCSV(file, ref comboItems);
            bindingList = comboItems;

            dataGrid.ItemsSource = books;

        }

        private void Description_Click(object sender, RoutedEventArgs e)
        {

            var item = dataGrid.SelectedItem as Book;
            if(item != null)
            System.Windows.Forms.MessageBox.Show(item.Description, "Description");
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = books.Where(b => b.InStock).ToList();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        
    }
}
