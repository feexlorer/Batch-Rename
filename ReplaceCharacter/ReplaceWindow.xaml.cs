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

namespace ReplaceCharacterRule
{
    /// <summary>
    /// Interaction logic for ReplaceWindow.xaml
    /// </summary>
    public partial class ReplaceWindow : Window
    {
        public ReplaceCharater Replace { get; set; }

        public ReplaceWindow(ReplaceCharater replace)
        {
            InitializeComponent();
            Replace = (ReplaceCharater)replace.Clone();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string[] inputOld = inputCharOld.Text.Split("\n");
            var inputNew = inputCharNew.Text;

            if (inputOld.Length == 0 || inputNew.Length == 0)
            {
                MessageBox.Show("Please enter a string.");
                return;
            }
            char[] invalidChar = { '<', '>', ':', '\"', '/', '\\', '|', '?', '*' };
            foreach (char c in invalidChar)
            {
                if (inputNew.Contains(c))
                {
                    MessageBox.Show("A file name can't contain characters: / : \\ * < > \" ? |");
                    return;
                }
            }

            Replace.SpecialChars = inputOld.ToList();
            Replace.Replacement = inputNew;
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
