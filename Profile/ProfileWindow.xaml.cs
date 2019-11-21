using Domain;
using Microsoft.Win32;
using Profile.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Profile
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public User CurrentUser { get; set; }
        public ProfileWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            ShowUserData();
        }

        private void ShowUserData()
        {
            if(profileImage.Source != null)
            {
                var imageSource = new BitmapImage(new Uri(CurrentUser.ImagePath, UriKind.Absolute));
                profileImage.Source = imageSource;
            }
            fullNameTextBox.Text = CurrentUser.FullName;
            addressTextBox.Text = CurrentUser.Address;
        }

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
                
            using (var context = new Context(Constants.ConnectionString))
            {
                var user = context.Users
                    .SingleOrDefault(x => x.Id == CurrentUser.Id);
                if(fullNameTextBox.Text != string.Empty)
                {
                     user.FullName = fullNameTextBox.Text;
                }
                else if(addressTextBox.Text != string.Empty)
                {
                    user.Address = addressTextBox.Text;
                }
                else if(profileImage.Source.ToString() != string.Empty)
                {

                    user.ImagePath = profileImage.Source.ToString();
                }
                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        private void ChangePhotoClick(object sender, RoutedEventArgs e)
        {
            var chooseImageDialog = new OpenFileDialog();
            chooseImageDialog.ShowDialog();
            var imagePath = chooseImageDialog.FileName;
            var imageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            profileImage.Source = imageSource;
        }
    }
}
