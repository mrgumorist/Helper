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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HelperLol
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "Smurf" && textBox2.Password== "Smurf"|| textBox1.Text == "Rostik" && textBox2.Password == "Rostik")
            {
                Logged logged = new Logged();
                this.Hide();
                logged.ShowDialog();
                this.Show();
                textBox1.Text = null;
                textBox2.Password = null;
            }
            else
            {
                MessageBox.Show("Fuck you! Bad Try :) You will only buy this program");
                Close();
            }
        }
    }
}
