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

namespace AddSuffixRule
{
    /// <summary>
    /// Interaction logic for AddSuffixWindow.xaml
    /// </summary>
    public partial class AddSuffixWindow : Window
    {
        
        public AddSuffix addsuffix { get; set; }

        public AddSuffixWindow(AddSuffix a)
        {
            InitializeComponent();
            addsuffix = (AddSuffix)a.Clone();
        }

        
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            var suffix = suffixInput.Text;
            if (suffix.Length == 0)
            {
                MessageBox.Show("Please enter a string.");
                return;
            }
            char[] invalidChar = { '<', '>', ':', '\"', '/', '\\', '|', '?', '*' };
            foreach (char c in invalidChar)
            {
                if (suffix.Contains(c))
                {
                    MessageBox.Show("A file name can't contain characters: / : \\ * < > \" ? |");
                    return;
                }
            }
           
            addsuffix._suffix = suffix;
            DialogResult = true;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
