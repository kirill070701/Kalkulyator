using System;
using System.Data;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(UIElement element in MainRoot.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = ((Button)e.OriginalSource).Content as string;

            if (str == "AC")
            {
                TextLabel.Text = "";
            }
            else if (str == "←" && TextLabel.Text != "")
            {
                TextLabel.Text = TextLabel.Text.Remove(TextLabel.Text.Length - 1, 1);
            }
            else if (str == "=")
            {
                //string value = new DataTable().Compute(TextLabel.Text, null).ToString();
                string from = new DataTable().Compute(SearchOperation(TextLabel.Text), null).ToString();

                //MessageBox.Show(SearchOperation(TextLabel.Text));
                TextLabel.Text = from;
            }
            else if(str != "←")
            {
                TextLabel.Text += str;
            }
        }

        private string SearchOperation(string equation)
        {
            if (equation.Contains("Cos("))
            {
                (string start, string cos, string end) = FindingValue(equation, "Cos(");
                equation = start + Math.Round(Math.Cos(int.Parse(cos) * Math.PI / 180), 3) + end;
            }
            if (equation.Contains("Sin("))
            {
                (string start, string sin, string end) = FindingValue(equation, "Sin(");
                equation = start + Math.Round(Math.Sin(int.Parse(sin) * Math.PI / 180), 3) + end;
            }
            if (equation.Contains("Tg("))
            {
                (string start, string tg, string end) = FindingValue(equation, "Tg(");
                equation = start + (Math.Round(Math.Tan(int.Parse(tg) * Math.PI / 180), 3)) + end;
            }
            if (equation.Contains("Ctg("))
            {
                (string start, string ctg, string end) = FindingValue(equation, "Ctg(");
                equation = start + (Math.Round(1 / Math.Tan(int.Parse(ctg) * Math.PI / 180), 3)) + end;
            }
            if (equation.Contains("√("))
            {
                (string start, string sqrt, string end) = FindingValue(equation, "√(");
                equation = start + (Math.Round(Math.Sqrt(int.Parse(sqrt)), 3)) + end;
            }
            return CommaReplacement(equation);
            
        }
        private (string, string, string) FindingValue(string equation, string trig)
        {
            int i = equation.IndexOf(trig);
            string str = equation.Remove(0, i + trig.Length);
            string x = "";
            string y = "";
            int j = 0;
            while (x != ")")
            {
                y += x;
                x = str.Substring(j, 1);
                j++;
            }
            string end = str.Remove(0, j);
            string start ="";
            x = "";
            j = 0;
            while (x != trig)
            {
                if (j > 0)
                {
                    start += equation.Substring(j - 1, 1);
                }
                x = equation.Substring(j, trig.Length);
                j++;
            }
            
            
            //MessageBox.Show(); // то что остается после скобки
            //MessageBox.Show(start);
            return (start, y, end);
        }

        private string CommaReplacement(string example)
        {
            return example.Replace(",", ".");
        }
    }
}
