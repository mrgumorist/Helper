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
            try
            {
                string text = Input.Text;
                var some = text.Split(new String[] { "questions" }, StringSplitOptions.None);
                text = some[1];
                Input.Text = text;
                some = text.Split(new List<string> { "title" }.ToArray(), StringSplitOptions.None);
                // MessageBox.Show(some.Length.ToString());
                Input.Text = "";
                foreach (var item in some)
                {
                    if (item.Contains("\"kind\": \"sorter\","))
                    {
                        var i = item.Split(new List<string> { "text" }.ToArray(), StringSplitOptions.None);
                        int index = 0;
                        foreach (var item1 in i)
                        {
                            if (index == 0)
                            {
                                index++;
                                continue;
                            }
                            var b = item1.Split(new String[] { "\n" }, StringSplitOptions.None);
                            Output.Text += index.ToString() + ":  " + b[0] + "\n";
                            index++;
                        }
                        Output.Text += "\n-----------------------\n";
                        continue;
                    }
                    try
                    {
                        var New = item.Split(new String[] { "\n" }, StringSplitOptions.None);
                        var Answer = item.Split(new List<string> { "text" }.ToArray(), StringSplitOptions.None);
                        Dictionary<string, string> NewDictionary = new Dictionary<string, string>();
                        try
                        {
                            var Categoris = item.Split(new List<string> { "\"categories\": [{" }.ToArray(), StringSplitOptions.None);
                            var Clozes = Categoris[1].Split(new List<string> { "\"clozes\": []," }.ToArray(), StringSplitOptions.None);
                            string Categoris_ = Clozes[0];
                            string Cloze = Clozes[1];
                            var IdCategor = Categoris_.Split(new List<string> { "\"id\":" }.ToArray(), StringSplitOptions.None);
                            var NamesCategor = Categoris_.Split(new List<string> { "\"text\":" }.ToArray(), StringSplitOptions.None);
                            for (int i = 0; i < IdCategor.Length; i++)
                            {
                                var Key = NamesCategor[i].Split(new List<string> { "\n" }.ToArray(), StringSplitOptions.None);
                                if (Key[0] == "")
                                { continue; }
                                var Value = IdCategor[i].Split(new List<string> { "\n" }.ToArray(), StringSplitOptions.None);
                                NewDictionary.Add(Key[0], Value[0]);
                            }
                            var IdClozes = Cloze.Split(new List<string> { "\"category\":" }.ToArray(), StringSplitOptions.None);
                            var NamesClozes = Cloze.Split(new List<string> { "\"text\":" }.ToArray(), StringSplitOptions.None);
                            //MessageBox.Show(IdClozes.Length.ToString() + "        " + NamesClozes.Length.ToString());
                            for (int i = 1; i < IdClozes.Length; i++)
                            {
                                //MessageBox.Show(IdCategor[i] + "       qwerty    " + NamesCategor[i]);
                                var Key = NamesClozes[i].Split(new List<string> { "\n" }.ToArray(), StringSplitOptions.None);
                                var Value = IdClozes[i].Split(new List<string> { "\n" }.ToArray(), StringSplitOptions.None);
                                //MessageBox.Show(i.ToString() + " ]Key:" + Value[0]);
                                try
                                {
                                    //MessageBox.Show(Key[0] + "    " + Value[0]);
                                    NewDictionary.Add(Key[0], Value[0]);
                                }
                                catch
                                {
                                }
                            }
                            foreach (var item1 in NewDictionary)
                            {
                                foreach (var item2 in NewDictionary)
                                {
                                    //MessageBox.Show(item2.Key + "     " + item2.Value);
                                    if ((item1.Value.Contains(item2.Value) || item2.Value.Contains(item1.Value)) && item1.Key != item2.Key)
                                    {
                                        Output.Text += item1.Key + "------" + item2.Key + "\n";
                                    }
                                }
                            }
                        }
                        catch { }
                        foreach (var item1 in Answer)
                        {
                            if (item1.Contains("\"isCorrect\": true,"))
                            {
                                var i = item1.Split(new String[] { "\n" }, StringSplitOptions.None);
                                Output.Text += New[0] + "         " + i[0] + "\n";
                            }
                            if (item1.Contains("\"isTrueCorrect\": true,"))
                            {
                                var i = item1.Split(new String[] { "\n" }, StringSplitOptions.None);
                                Output.Text += New[0] + "         " + "true" + "\n";
                            }
                            if (item1.Contains("\"isTrueCorrect\": false,"))
                            {
                                var i = item1.Split(new String[] { "\n" }, StringSplitOptions.None);
                                Output.Text += New[0] + "         " + "false" + "\n";
                            }
                        }
                        Output.Text += "\n-----------------------------\n";
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            catch
            { MessageBox.Show("Error"); }
        }

    
        private void DeleteSelected(object sender, RoutedEventArgs e)
        {
            Output.SelectedText = "";
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            int lineCount = Output.LineCount;
            List<string> lines = new List<string>();
            for (int line = 0; line < lineCount; line++)
                  lines.Add(Output.GetLineText(line));
           
            int index=0;
            for(int i=1; i<lineCount; i++)
            {
                if(String.Equals("-----------------------------\n", lines[i]))
                {
                    index = i;
                    break;
                }
            }
            Output.Text = "";
            for (int i= index; i<lineCount; i++ )
            {
                Output.Text += lines[i];
            }


        }
    }
}
