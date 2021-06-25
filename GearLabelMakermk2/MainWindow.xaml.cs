using System;
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

namespace GearLabelMakermk2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Check for actual job number
            if(string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("Enter a job number");
                return;
            }
            else if (!(txtInput.Text.Trim().Length == 8 || txtInput.Text.Trim().Length == 10))
            {
                MessageBox.Show("Etner an 8 or 10 didget serial number");
                return;
            }
            TagInformation ti = new TagInformation(txtInput.Text.Trim());
        }
    }
}
