using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using static Chai.MainWindow;

namespace Chai
{
    public partial class calcu : Window
    {
        private string Variable;
        public static string failz = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string failz1 = failz + "\\Plushours.xml";
        string path = failz1;
        public static int h = 0;
        public static string checker = h.ToString() + ".";
        List<string> menuplus = new List<string>();
        string ind = zna4;

        public calcu(string somevar)
        {
            InitializeComponent();
            Variable = somevar;
            if (File.Exists(path))
            {
                XElement xelement = XElement.Load(path);
                IEnumerable<XElement> users = xelement.Elements();
                foreach (var user in users)
                {
                    if (user.Attribute("id").Value == ind)
                    {
                        foreach (var hours in user.Descendants())
                        {
                            menuplus.Add(hours.Value);
                        }  
                    }
                }
            }
            if (!File.Exists(path))
            {
                for (int a = 0; a < 3; a++)
                {
                    tozedobavka.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
            else
            {
                if (menuplus.Count == 0)
                {
                    for (int a = 0; a < 3; a++)
                    {
                        tozedobavka.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                }
            }
            if (File.Exists(path))
            {
                XElement xelement = XElement.Load(path);
                IEnumerable<XElement> users = xelement.Elements();
                var c = 0;
                foreach (var user in users)
                {
                    if (user.Attribute("id").Value == ind)
                    {
                        for (int a = 0; a < menuplus.Count; a++)
                        {
                            tozedobavka.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                        foreach (TextBox child in plushours.Children)
                        {
                            TextBox backh = (TextBox)plushours.Children[c];
                            backh.Text = menuplus[c].ToString();
                            c++;
                        }
                        menuplus.Clear();
                    }
                }
            }
        }

        int lindex = 0;
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            TextBox plushours1 = new TextBox()
            {
                Text = "0.0",
                Name = "Name" + lindex.ToString(),
            };
            Button deletethis1 = new Button
            {
                Name = "id" + lindex.ToString(),
            };
            plushours1.Background = (Brush)new BrushConverter().ConvertFrom("#3A3F39");
            plushours1.Foreground = Brushes.White;
            plushours1.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            plushours1.BorderThickness = new Thickness(0);
            plushours1.Padding = new Thickness(2);
            plushours1.FontSize = 16;
            plushours1.Margin = new Thickness(3);
            plushours1.TextAlignment = TextAlignment.Right;


            deletethis1.Click += Removelem1;
            deletethis1.Content = "✘";
            deletethis1.HorizontalContentAlignment = HorizontalAlignment.Center;
            deletethis1.Background = (Brush)new BrushConverter().ConvertFrom("#da1043");
            deletethis1.Foreground = Brushes.White;
            deletethis1.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#000000");
            deletethis1.BorderThickness = new Thickness(0);
            deletethis1.Padding = new Thickness(2);
            deletethis1.FontSize = 16;
            deletethis1.Margin = new Thickness(3);

            plushours.Children.Add(plushours1);
            deletethis.Children.Add(deletethis1);

            lindex++;
        }

        private void Removelem1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Console.WriteLine(lindex);
            string messageBoxText = "Желаете удалить поле ввода?";
            string caption = "";
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (deletethis.Children.Contains(btn))
                    {
                        plushours.Children.RemoveAt(deletethis.Children.IndexOf(btn));
                        deletethis.Children.RemoveAt(deletethis.Children.IndexOf(btn));
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
            if (lindex > 0)
            {
                lindex--;
            }
        }
        List<double> Chasiki = new List<double> { };
        public double summas;
        public static class MyVariables
        {
            public static bool IsClosed;
            public static double Summas;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var i = 0;

            foreach (TextBox child in plushours.Children)
            {
                TextBox vrema = (TextBox)plushours.Children[i];
                double b = Convert.ToDouble(vrema.Text.ToString());
                Chasiki.Add(b);
                i++;
            }

            summas = Chasiki.Sum();
            MyVariables.Summas = summas;
            MyVariables.IsClosed = true;
            bob.Text = summas.ToString("F1");
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XElement xelement = XElement.Load(path);
                IEnumerable<XElement> users = xelement.Elements();

                XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "user", "");
                XmlAttribute newAttribute = doc.CreateAttribute("id");
                newAttribute.InnerText = ind.ToString();
                XmlNode node = doc.SelectSingleNode("/users/user[@id=" + ind.ToString() + "]");
                {
                    foreach (var user in users)
                    {
                        if (user.Attribute("id").Value == ind)
                        {
                            XmlNode parent = node.ParentNode;
                            parent.RemoveChild(node);
                            newNode.Attributes.Append(newAttribute);
                            doc.DocumentElement.AppendChild(newNode);
                        }

                    }
                }
                {
                    foreach (var user in users)
                    {
                        if (user.Attribute("id").Value != ind)
                        {
                            doc.DocumentElement.AppendChild(newNode);
                            newNode.Attributes.Append(newAttribute);
                        }
                    }
                    for (int b = 0; b < Chasiki.Count; b++)
                    {
                        XmlNode newNode1 = doc.CreateNode(XmlNodeType.Element, "hours", "");
                        newNode1.InnerText = Chasiki[b].ToString("F1");
                        newNode.AppendChild(newNode1);
                    }
                }
                doc.Save(path);
            }
            if (!File.Exists(path))
            {
                XmlWriter xmlWriter = XmlWriter.Create(path);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("users");
                xmlWriter.WriteStartElement("user");
                xmlWriter.WriteAttributeString("id", ind.ToString());
                for (int a = 0; a < Chasiki.Count; a++)
                {
                    xmlWriter.WriteStartElement("hours");
                    xmlWriter.WriteString(Chasiki[a].ToString("F1"));
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            Close();


        }

    }
}
