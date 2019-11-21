using System;
using System.Collections.Generic;
using Domain;
using Microsoft.Extensions.Configuration;
using Profile.DataAccess;
using Profile.Services;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace Profile.UI
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();
            var connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
            new Constants(connectionString);
            InitializeComponent();
        }
        private void SignInButtonClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Context(Constants.ConnectionString))
            {
                var service = new AuthService(context);
                var user = new User();
                if (emailTextBox.Text.Length == 0 || passwordTextBox.Password.Length == 0)
                {
                    MessageBox.Show("Введите данные!");
                }
                else
                {
                    user = service.SignIn(emailTextBox.Text, passwordTextBox.Password);
                    if (user is null)
                    {
                        MessageBox.Show("Неправильный email или пароль!");
                    }
                    else
                    {
                        var window = new ProfileWindow(user);
                        this.Close();
                        window.Show();
                    }
                }
            }
        }

        private void CreateAccoutClick(object sender, RoutedEventArgs e)
        {
            var window = new SignUpWindow();
            this.Close();
            window.ShowDialog();
        }
        private void TapEnterForSign(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SignInButtonClick(sender, e);
        }
    }
}

