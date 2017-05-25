using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Encyclopedia.Resources;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Windows.Phone.UI.Input;

namespace Encyclopedia
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static List<registration.UserInfo> ObjUserDataList = new List<registration.UserInfo>();


        private registration.UserInfo adminUser = new registration.UserInfo("admin", "admin", "admin123@abv.bg", 0220002321);


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;


    }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            //Code to disable the back button
            e.Handled = true;

            //Here you can add your own code and perfrom any task
            var mm = MessageBox.Show("Do you wish to exit the app", "Exit", MessageBoxButton.OKCancel);
            if (mm == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var Settings = IsolatedStorageSettings.ApplicationSettings;
            //Check if user already login,so we need to direclty navigate to details page instead of showing login page when user launch the app.   
            if (Settings.Contains("CheckLogin")) {
                NavigationService.Navigate(new Uri("welcomePage.xaml", UriKind.Relative));
            } else {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("People.xml", FileMode.OpenOrCreate)) {
                        if (fileStream.Length > 1) {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<registration.UserInfo>));
                            ObjUserDataList = (List<registration.UserInfo>)serializer.Deserialize(fileStream);
                        }
                    }
                }
            }
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var Settings = IsolatedStorageSettings.ApplicationSettings;
            if (textBox1.Text == adminUser.Username && textBox2.Password == adminUser.Password)
            {
                
                Settings["CheckLogin"] = "Login sucess";//write iso     
                using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isoFile.FileExists("CurrentLoginUserDetails"))
                    {
                        isoFile.DeleteFile("CurrentLoginUserDetails");
                    }
                    using (IsolatedStorageFileStream fileStream = isoFile.OpenFile("CurrentLoginUserDetails", FileMode.Create))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(registration.UserInfo));
                        serializer.WriteObject(fileStream, adminUser);
                    }
                    NavigationService.Navigate(new Uri("/welcomePage.xaml", UriKind.Relative));
                }
            }
                if (textBox1.Text != "" && textBox2.Password != "") {
                    int Temp = 0;
                    foreach (var UserLogin in ObjUserDataList) {
                        if (textBox1.Text == UserLogin.Username && textBox2.Password == UserLogin.Password) {
                            Temp = 1;
                            Settings = IsolatedStorageSettings.ApplicationSettings;
                            Settings["CheckLogin"] = "Login sucess";//write iso     
                            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication()) {
                                if (isoFile.FileExists("CurrentLoginUserDetails")) {
                                    isoFile.DeleteFile("CurrentLoginUserDetails");
                                }
                                using (IsolatedStorageFileStream fileStream = isoFile.OpenFile("CurrentLoginUserDetails", FileMode.Create)){
                                    DataContractSerializer serializer = new DataContractSerializer(typeof(registration.UserInfo));
                                    serializer.WriteObject(fileStream, UserLogin);
                                }
                                NavigationService.Navigate(new Uri("/welcomePage.xaml", UriKind.Relative));
                           }
                        }
                    }
                if (textBox1.Text == "admin" && textBox2.Password == "admin")
                {
                    Temp = 1;
                }
                if (Temp == 0) {

                        MessageBox.Show("Invalid UserID/Password");
                    }
                } else {
                    MessageBox.Show("Enter UserID/Password");
                }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/registration.xaml", UriKind.Relative));
        }
    }
}