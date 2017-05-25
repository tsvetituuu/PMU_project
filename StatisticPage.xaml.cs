using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using static Encyclopedia.TestsPage;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Encyclopedia.registration;

namespace Encyclopedia
{
    public partial class StatisticPage : PhoneApplicationPage
    {
        public UserInfo currentUser = new UserInfo();
        public StatisticPage()
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
            this.textBlockName.Text =  currentUser.Username + ", here are the results";


           

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("Results.xml", FileMode.OpenOrCreate))
                    {
                        if (fileStream.Length > 1)

                        {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<UserStatistics>));
                            ObjUserDataList = (List<UserStatistics>)serializer.Deserialize(fileStream);
                        }


                    

                }
            }
            int maxDog = 0;
            int maxCat = 0;
            foreach (var UserStatistics in ObjUserDataList)
            {
                if (UserStatistics.Uname.Equals(currentUser.Username))
                {
                    maxDog = UserStatistics.ResultDog;
                    maxCat = UserStatistics.ResultCat;
                }

            }
            if (maxDog != 0 || maxCat != 0)
            {
                this.textBlockBestCat.Text = "Your result for dog is:" + maxDog + ". Your result for cat is :" + maxCat;
            }
        }
        public static List<UserStatistics> ObjUserDataList = new List<UserStatistics>();
    }
}