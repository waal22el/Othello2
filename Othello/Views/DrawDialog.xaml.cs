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
    /// Interaction logic for DrawDialog.xaml
    /// </summary>
    public partial class DrawDialog : Window
    {
        public DrawDialog()
        {
            InitializeComponent();
            DrawText.Text = "It's a draw!";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
