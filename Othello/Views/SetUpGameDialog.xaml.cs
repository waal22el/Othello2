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

namespace Othello.Views
{
    /// <summary>
    /// Interaction logic for SetUpGameDialog.xaml
    /// </summary>
    public partial class SetUpGameDialog : Window
    {
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }
        public string Player1Type { get; private set; }
        public string Player2Type { get; private set; }

        public SetUpGameDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Player1Name = Player1NameTextBox.Text;
            Player2Name = Player2NameTextBox.Text;
            Player1Type = (Player1TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            Player2Type = (Player2TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(Player1Name) || string.IsNullOrWhiteSpace(Player2Name))
            {
                MessageBox.Show("Both players must have names.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
