using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace AddPrefixRule
{
    /// <summary>
    /// Interaction logic for AddPrefixWindow.xaml
    /// </summary>
    public partial class AddPrefixWindow : Window
    {
        public AddPrefix addPrefix { get; set; }

        public AddPrefixWindow(AddPrefix a)
        {
            InitializeComponent();
            addPrefix = (AddPrefix)a.Clone();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            var prefix = prefixInput.Text;
            if (prefix.Length == 0)
            {
                MessageBox.Show("Please enter a string.");
                return;
            }

            char[] invalidChar = { '<', '>', ':', '\"', '/', '\\', '|', '?', '*' };

            foreach (char c in invalidChar)
            {
                if (prefix.Contains(c))
                {
                    MessageBox.Show("A file name can't contain characters: / : \\ * < > \" ? |");
                    return;
                }
            }   
            addPrefix.prefix = prefix;
            DialogResult = true;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
