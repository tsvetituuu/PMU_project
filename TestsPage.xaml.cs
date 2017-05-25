using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.UI.Input;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using static Encyclopedia.registration;
using System.Runtime.Serialization;

namespace Encyclopedia
{
    public partial class TestsPage : PhoneApplicationPage

    {
        string selected = "dog";
        Boolean answeer = true;
        int level = 1;

        UserStatistics currentUser = new UserStatistics();
        public TestsPage()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

        }
        public class UserStatistics
        {
            string uname;
            int resultDog = 0;
            int resultCat = 0;


            public UserStatistics()
            {

            }

            public string Uname
            {
                get { return uname; }
                set { uname = value; }
            }

            public int ResultDog
            {
                get { return resultDog; }
                set { resultDog = value; }
            }

            public int ResultCat
            {
                get {  return resultCat; }
                set { resultCat = value; }
            }
            


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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            selected = "dog";
            RadioButton level1 = (RadioButton)this.level1;
            RadioButton level2 = (RadioButton)this.level2;
            RadioButton level3 = (RadioButton)this.level3;
            if ((bool)level1.IsChecked)
            {
                level = 1;
                changeQuestion();
            }
            else
            {
                if ((bool)level2.IsChecked)
                {
                    level = 2;
                    changeQuestion();
                }
                else
                {
                    level = 3;
                    changeQuestion();
                }
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            selected = "cat";
            RadioButton level1 = (RadioButton)this.level1;
            RadioButton level2 = (RadioButton)this.level2;
            RadioButton level3 = (RadioButton)this.level3;
            if ((bool)level1.IsChecked)
            {
                level = 1;
                changeQuestion();
            }
            else
            {
                if ((bool)level2.IsChecked)
                {
                    level = 2;
                    changeQuestion();
                }
                else
                {
                    level = 3;
                    changeQuestion();
                }
            }

        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

            RadioButton radioYes = (RadioButton)this.Yes;
            RadioButton radioNo = (RadioButton)this.No;
            if (((bool)radioYes.IsChecked && answeer) || ((bool)radioNo.IsChecked && answeer))
            {
                if (selected == "dog")
                {
                    currentUser.ResultDog +=  this.level;
                }
                else
                {
                    currentUser.ResultCat += this.level;
                }


            }else
            {
                if (selected == "dog")
                {
                    currentUser.ResultDog -= this.level;
                }
                else
                {
                    currentUser.ResultCat -= this.level;
                }
            }
            updateResuts();
            changeQuestion();
        }
        private void updateResuts()
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("Results.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<UserStatistics>));
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                    {
                        serializer.Serialize(xmlWriter, GeneratePersonData());
                    }
                }
            }
        }


        private List<UserStatistics> GeneratePersonData()
        {
            List<UserStatistics> data = new List<UserStatistics>();
            UserInfo currentUserInfo = new UserInfo();
            using (IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoFile.FileExists("CurrentLoginUserDetails"))//read current user login details     
                {
                    using (IsolatedStorageFileStream fileStream = isoFile.OpenFile("CurrentLoginUserDetails", FileMode.Open))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(UserInfo));
                        currentUserInfo = (UserInfo)serializer.ReadObject(fileStream);

                    }

                }
            }
            
            currentUser.Uname = currentUserInfo.Username;

            data.Add(currentUser);
            return data;
        }

        private void changeQuestion()
        {
            
            if (selected.Equals("dog"))
            {
                if (level == 1)
                {
                    this.textBlock.Text = "Is the dog best human friend?";
                    this.answeer = true;
                }
                else
                {
                    if(level == 2)
                    {
                        this.textBlock.Text = "Could dog perform protection for a person?";
                        this.answeer = true;

                    }
                    else
                    {
                        this.textBlock.Text = "Does the dog like cats?";
                        this.answeer = false;
                    }
                }
            }
            else
            {
                if (level == 1)
                {
                    this.textBlock.Text = "Are the cats indoor pets?";
                    this.answeer = true;
                }
                else
                {
                    if (level == 2)
                    {
                        this.textBlock.Text = "Could the cat perform protection for a person?";
                        this.answeer = false;

                    }
                    else
                    {
                        this.textBlock.Text = "Does the cat like dogs?";
                        this.answeer = false;
                    }
                }

            }
        }
    }
}

