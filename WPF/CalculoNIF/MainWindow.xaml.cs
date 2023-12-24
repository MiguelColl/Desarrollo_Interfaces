using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculoNIF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBox.Text.Length != 8)
            {
                error.Visibility = Visibility.Visible;
                resultadoContainer.Visibility = Visibility.Hidden;
            }
            else
            {
                if(Int32.TryParse(textBox.Text, out int dni))
                {
                    error.Visibility = Visibility.Hidden;
                    resultado.Content = dni.ToString() + GetLetter(dni % 23);
                    resultadoContainer.Visibility = Visibility.Visible;
                }
            }
        }

        private char GetLetter(int numero)
        {
            char letter = ' ';
            switch(numero)
            {
                case 0:
                    letter = 'T';
                    break;
                case 1:
                    letter = 'R';
                    break;
                case 2:
                    letter = 'W';
                    break;
                case 3:
                    letter = 'A';
                    break;
                case 4:
                    letter = 'G';
                    break;
                case 5:
                    letter = 'M';
                    break;
                case 6:
                    letter = 'Y';
                    break;
                case 7:
                    letter = 'F';
                    break;
                case 8:
                    letter = 'P';
                    break;
                case 9:
                    letter = 'D';
                    break;
                case 10:
                    letter = 'X';
                    break;
                case 11:
                    letter = 'B';
                    break;
                case 12:
                    letter = 'N';
                    break;
                case 13:
                    letter = 'J';
                    break;
                case 14:
                    letter = 'Z';
                    break;
                case 15:
                    letter = 'S';
                    break;
                case 16:
                    letter = 'Q';
                    break;
                case 17:
                    letter = 'V';
                    break;
                case 18:
                    letter = 'H';
                    break;
                case 19:
                    letter = 'L';
                    break;
                case 20:
                    letter = 'C';
                    break;
                case 21:
                    letter = 'K';
                    break;
                case 22:
                    letter = 'E';
                    break;
            }
            return letter;
        }
    }
}