using Profile.DataAccess;
using Profile.Services;
using System.Windows;
using System.Windows.Input;

namespace Profile.UI
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }
        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Context(Constants.ConnectionString))
            {
                var service = new AuthService(context);

                if (fullNameTB.Text.Length == 0 || emailTB.Text.Length == 0 || passwordTB.Password.Length == 0)
                {
                    MessageBox.Show("Введите данные!");
                }
                else if (!service.IsValidEmail(emailTB.Text))
                {
                    MessageBox.Show("Введите корректный email!");
                }
                else
                {
                    service.SignUp(fullNameTB.Text, emailTB.Text, passwordTB.Password);
                    var window = new SignInWindow();
                    this.Close();
                    window.ShowDialog();
                }
            }
        }

        private void TapEnterForSignUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SignUpButtonClick(sender, e);
        }
    }
}

