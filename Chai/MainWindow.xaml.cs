using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using System.Xml.Linq;
using static Chai.calcu;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Path = System.IO.Path;

namespace Chai
{
    public partial class MainWindow : Window
    {
        Grid MainGrid = new Grid();
        public static string fail = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Log");
        public string Logi => Path.Combine(path, "Logs_" + DateTime.Now.ToString("yyyy-dd-M__HH-mm-ss") + ".csv");
        public static string fail1 = fail + "\\Imena.txt";
        public static string fail2 = fail + "\\PlusHours.xml";
        
        public MainWindow()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
               
            if (File.Exists(fail2))
            {
                File.Delete(fail2);
            }
            InitializeComponent();
        }
        int abs = 0;

        public void Button_Add(object sender, RoutedEventArgs e)
        {
            TextBox name = new TextBox()
            {
                Name = "Name" + abs.ToString(),
            };

            TextBox hours = new TextBox
            {
                Text = "0.0",
                Name = "Hours" + abs.ToString(),

            };
            Button info = new Button()
            {
                Name = "id" + abs.ToString(),
            };
            TextBox money = new TextBox
            {
                Text = "0.00",
                Name = "Money" + abs.ToString(),

            };
            Button deletethis = new Button
            {
                Name = "id" + abs.ToString(),
            };
            deletethis.Click += Removelem;
            deletethis.Content = "✘";
            deletethis.HorizontalContentAlignment = HorizontalAlignment.Center;
            deletethis.Background = (Brush)new BrushConverter().ConvertFrom("#da1043");
            deletethis.Foreground = Brushes.White;
            deletethis.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            deletethis.BorderThickness = new Thickness(0);
            deletethis.Padding = new Thickness(2);
            deletethis.FontSize = 16;
            deletethis.Margin = new Thickness(3);

            info.Click += dopcalculator;
            info.Content = "...";
            info.HorizontalContentAlignment = HorizontalAlignment.Center;
            info.Background = (Brush)new BrushConverter().ConvertFrom("#585e59");
            info.Foreground = Brushes.White;
            info.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            info.BorderThickness = new Thickness(0);
            info.Padding = new Thickness(2);
            info.FontSize = 16;
            info.Margin = new Thickness(3);

            name.Background = (Brush)new BrushConverter().ConvertFrom("#3A3F39");
            name.Foreground = Brushes.White;
            name.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            name.BorderThickness = new Thickness(0);
            name.Padding = new Thickness(2);
            name.FontSize = 16;
            name.Margin = new Thickness(3);

            hours.Background = (Brush)new BrushConverter().ConvertFrom("#3A3F39");
            hours.Foreground = Brushes.White;
            hours.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            hours.BorderThickness = new Thickness(0);
            hours.Padding = new Thickness(2);
            hours.FontSize = 16;
            hours.Margin = new Thickness(3);
            hours.TextAlignment = TextAlignment.Right;

            money.Background = (Brush)new BrushConverter().ConvertFrom("#3A3F39");
            money.Foreground = Brushes.White;
            money.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            money.BorderThickness = new Thickness(0);
            money.Padding = new Thickness(2);
            money.FontSize = 16;
            money.Margin = new Thickness(3);
            money.IsReadOnly = true;
            money.TextAlignment = TextAlignment.Right;

            stack1.Children.Add(name);
            stack2.Children.Add(hours);
            stack3.Children.Add(info);
            stack4.Children.Add(money);
            stack5.Children.Add(deletethis);
            abs++;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            
            bool newValt = imena.IsChecked == true;
            if (newValt)
            {
                using (var streamReader = File.OpenText(fail1))
                {
                    var s = string.Empty;
                    while ((s = streamReader.ReadLine()) != null)
                        dlavvoda.Add(s);
                }
                for (int a = 0; a < dlavvoda.Count; a++)
                {
                    if (dlavvoda.Count > stack1.Children.Count)
                    {
                        dobavka.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                    TextBox imena1 = (TextBox)stack1.Children[a];
                    imena1.Text = dlavvoda[a].ToString();
                }
                dlavvoda.Clear();
            }
        }
        private void Removelem(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Console.WriteLine(abs);
            string messageBoxText = "Желаете удалить поле ввода?";
            string caption = "";
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (stack5.Children.Contains(btn))
                    {
                        stack1.Children.RemoveAt(stack5.Children.IndexOf(btn));
                        stack2.Children.RemoveAt(stack5.Children.IndexOf(btn));
                        stack3.Children.RemoveAt(stack5.Children.IndexOf(btn));
                        stack4.Children.RemoveAt(stack5.Children.IndexOf(btn));
                        stack5.Children.RemoveAt(stack5.Children.IndexOf(btn));

                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
            if (abs > 0)
            {
                abs--;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stack1.Children.Clear();
            stack2.Children.Clear();
            stack3.Children.Clear();
            stack4.Children.Clear();
            stack5.Children.Clear();
            dengi.Text = "0.00";
            
        }
        List<double> Chasi = new List<double> { };
        List<double> gotoviipp = new List<double> { };
        List<double> gotovidengilist = new List<double> { };
        List<string> name = new List<string> { };
        List<string> dlavvoda = new List<string>();
        public object znachenie { get; private set; }
        public object hour1 { get; internal set; }

        private void Show_text_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var i = 0;

            foreach (TextBox child in stack1.Children)
            {
                TextBox hoursz = (TextBox)stack2.Children[i];
                TextBox money = (TextBox)stack4.Children[i];
                TextBox Names = (TextBox)stack1.Children[i];
                double b = Convert.ToDouble(hoursz.Text.ToString());
                Chasi.Add(b);
                name.Add(Names.Text);
                i++;

            }
            double sum = Chasi.Sum();

            for (int a = 0; a < Chasi.Count; a++)
            {
                double protsent = Chasi[a] / sum * 100;
                gotoviipp.Add(protsent);
            }
            double dengidb = Convert.ToDouble(dengi.Text);
            for (int b = 0; b < gotoviipp.Count; b++)
            {
                double gotoviedengi = dengidb * (gotoviipp[b] / 100);
                gotovidengilist.Add(gotoviedengi);
            }
            var c = 0;
            foreach (TextBox child in stack1.Children)
            {
                if (Double.IsNaN(gotovidengilist[c]) || Double.IsInfinity(gotovidengilist[c]))
                {
                    TextBox money1 = (TextBox)stack4.Children[c];
                    money1.Text = "0.00";
                }
                else
                {
                    TextBox money1 = (TextBox)stack4.Children[c];
                    money1.Text = gotovidengilist[c].ToString("F2");
                }
                c++;
            }

            string path = fail1;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(string.Join("\n", name));
                }

            }
            else if (File.Exists(path))
            {
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(string.Join("\n", name));
                }
            }

            string path1 = Logi;
            File.Create(path1).Dispose();
            using (var w = new StreamWriter(path1))
            {

                    var first = "Name";
                    var second = "Hours";
                    var third = "Tips";
                    var itogo = "Total Money:";
                    var dengz = dengi.Text.ToString();
      
                    var line = string.Format("{0};{1};{2}", first, second, third);
                    w.WriteLine(line);
                    
                    for (int a = 0; a < Chasi.Count; a++)
                    {
                    if (Double.IsNaN(gotovidengilist[a]) || Double.IsInfinity(gotovidengilist[a]))
                    {
                        var line1 = string.Format("{0};{1};{2}", name[a].ToString(), Chasi[a].ToString("F2"), "0.00");
                        w.WriteLine(line1);
                    }
                    else
                    {
                        var line1 = string.Format("{0};{1};{2}", name[a].ToString(), Chasi[a].ToString("F1"), gotovidengilist[a].ToString("F2"));
                        w.WriteLine(line1);
                    }
                    }
                    var line3 = string.Format("");
                    w.WriteLine(line3);
                    var line2 = string.Format("{0};;{1}", itogo, dengz);
                    w.WriteLine(line2);
                    w.Flush();
            }
            name.Clear();
            Chasi.Clear();
            gotoviipp.Clear();
            gotovidengilist.Clear();
        }

        private void comboBox_SelectionChanged(object sender1, SelectionChangedEventArgs e)
        {
            string euro = "€";
            string dollar = "$";
            string ruble = "₽";
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    izmena.Text = "       Работники       Часы{общ} Чаевые " + euro;
                    break;
                case 1:
                    izmena.Text = "       Работники       Часы{общ} Чаевые " + dollar;
                    break;
                case 2:
                    izmena.Text = "       Работники       Часы{общ} Чаевые " + ruble;
                    break;
            }
        }
        public double znach = MyVariables.Summas;
        public bool Okno = MyVariables.IsClosed;
        public static TextBox bob;

        public string aboba;
        public static string zna4;
        private void dopcalculator(object sender, RoutedEventArgs e)
        {
            Button otpravka = (Button)sender;
            zna4 = stack3.Children.IndexOf(otpravka).ToString();
            bob = (TextBox)stack2.Children[stack3.Children.IndexOf(otpravka)];
            if (stack3.Children.Contains(otpravka))
            {
                calcu taskWindow = new calcu("Varible");
                taskWindow.ShowDialog();
            }
        }
    }
}

