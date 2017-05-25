using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using static Encyclopedia.registration;
using System.IO;

namespace Encyclopedia
{
    public partial class welcomePage : PhoneApplicationPage
    {
        public UserInfo currentUser = new UserInfo();
        public welcomePage()
        {
            InitializeComponent();

            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoFile.FileExists("CurrentLoginUserDetails"))//read current user login details     
                {
                    using (IsolatedStorageFileStream fileStream = isoFile.OpenFile("CurrentLoginUserDetails", FileMode.Open))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(UserInfo));
                        currentUser = (UserInfo)serializer.ReadObject(fileStream);

                    }

                }
            }
            TextBlockUserName.Text = currentUser.Username;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Articles.xaml", UriKind.Relative));
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TestsPage.xaml", UriKind.Relative));
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("//StatisticPage.xaml", UriKind.Relative));
        }
    }
}