using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChangeExtensionRule
{
    /// <summary>
    /// Interaction logic for ChangeExtensionWindow.xaml
    /// </summary>
    public partial class ChangeExtensionWindow : Window
    {

        public ChangeExtension changeExt { get; set; }

        public ChangeExtensionWindow(ChangeExtension a)
        {
            InitializeComponent();
            changeExt = (ChangeExtension)a.Clone();
        }


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            var ext = extInput.Text;
            //Regex regex = new Regex(@"^[a-zA-Z0-9]*$");

            if (ext.Length == 0)
            {
                MessageBox.Show("Please enter a string.");
                return;
            }

            //if (regex.IsMatch((string)ext) == false)
            //{
            //    MessageBox.Show("Extension just contain characters: 0-9, a-z, A-Z");
            //    return;
            //}

            char[] invalidChar = { '<', '>', ':', '\"', '/', '\\', '|', '?', '*' };

            foreach (char c in invalidChar)
            {
                if (ext.Contains(c))
                {
                    MessageBox.Show("A file name can't contain characters: / : \\ * < > \" ? |");
                    return;
                }
            }
            changeExt._extension = ext;
            
            DialogResult = true;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
