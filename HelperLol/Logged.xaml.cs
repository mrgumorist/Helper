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

namespace HelperLol
{
    /// <summary>
    /// Interaction logic for Logged.xaml
    /// </summary>
    public partial class Logged : Window
    {
        public Logged()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StartParse_Click(object sender, RoutedEventArgs e)
        {
            string text = Input.Text;
            var some = text.Split(new String[] { "questions" }, StringSplitOptions.None);
            text = some[1];
            Input.Text = text;
            Output.Text = null;
            some = text.Split(new List<string> { "title" }.ToArray(), StringSplitOptions.None);
            MessageBox.Show(some.Length.ToString());
            Input.Text = "";
            foreach (var item in some)
            {
                var New = item.Split(new String[] { "\n" }, StringSplitOptions.None);
                var Answer = item.Split(new List<string> { "text" }.ToArray(), StringSplitOptions.None);

                var Id = item.Split(new List<string> { "id" }.ToArray(), StringSplitOptions.None);
                Dictionary<string, string> NewDictionary = new Dictionary<string, string>();
                try
                {
                    foreach (var item1 in Id)
                    {
                        var id = item1.Split(new String[] { "\n" }, StringSplitOptions.None);

                        var b = item1.Split(new List<string> { "text" }.ToArray(), StringSplitOptions.None);

                        var c = b[1].Split(new String[] { "\n" }, StringSplitOptions.None);
                        NewDictionary.Add(c[0], id[0]);
                    }
                    foreach (var I in NewDictionary)
                    {
                        foreach (var I1 in NewDictionary)
                        {
                            if (I.Value == I1.Value)
                            {
                                Input.Text += I.Key + "----" + I1.Key;
                            }
                        }
                    }
                }
                catch (Exception)
                {

                };
                foreach (var item1 in Answer)
                {

                    if (item1.Contains("\"isCorrect\": true,"))
                    {
                        var i = item1.Split(new String[] { "\n" }, StringSplitOptions.None);

                        Output.Text += "Quesion " + New[0] + "        Answer " + i[0] + "\n";
                    }
                    if (item1.Contains("\"isTrueCorrect\": true,"))
                    {
                        var i = item1.Split(new String[] { "\n" }, StringSplitOptions.None);

                        Output.Text += "Quesion "+ New[0] + "        " + "true" + "\n";
                    }
                    if (item1.Contains("\"isTrueCorrect\": false,"))
                    {
                        var i = item1.Split(new String[] { "\n" }, StringSplitOptions.None);

                        Output.Text += "Quesion " + New[0] + "         " + "false" + "\n";
                    }
                }
                Output.Text += "\n-----------------------------\n";
            }
        }
    }
}
