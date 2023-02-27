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

namespace AddCounterRule
{
    /// <summary>
    /// Interaction logic for AddCounterWindow.xaml
    /// </summary>
    public partial class AddCounterWindow : Window
    {
        public AddCounter addcoutner;
        public AddCounterWindow(AddCounter a)
        {
            InitializeComponent();
            addcoutner = (AddCounter)a.Clone();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartValue.Text.Length == 0 || Steps.Text.Length == 0 || NumberOfDigits.Text.Length == 0)
            {
                MessageBox.Show("Please enter all fields!");
                return;
            }
            try
            {
                addcoutner._startValue = Convert.ToInt32(StartValue.Text);
                addcoutner._current = addcoutner._startValue;
                addcoutner._step = Convert.ToInt32(Steps.Text);
                addcoutner._numberOfDigit = Convert.ToInt32(NumberOfDigits.Text);
            }
            catch
            {
                MessageBox.Show("Please enter numbers in all fields!");
                return;
            }
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
