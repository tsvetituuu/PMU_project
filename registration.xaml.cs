using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace Encyclopedia
{
    public partial class registration : PhoneApplicationPage
    {

        public registration()
        {
            InitializeComponent();
            textBox1.Text = "";
            textBox2.Password = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        public class UserInfo
        {
            string uname;
            string pwd;
            string email;
            int mno;

            public UserInfo()
            {

            }

            public UserInfo(string name, string pwd, string email, int mno)
            {
                this.uname = name;
                this.pwd = pwd;
                this.email = email;
                this.mno = mno;
            }
            public string Username
            {
                get { return uname; }
                set { uname = value; }
            }
            public string Password
            {
                get { return pwd; }
                set { pwd = value; }
            }
            public string EmailAdd
            {
                get { return email; }
                set { email = value; }
            }

            public int Mobile
            {
                get { return mno; }
                set { mno = value; }
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Boolean validationFailed = false;

            //UserName Validation   
            if (!Regex.IsMatch(textBox1.Text.Trim(), @"^[A-Za-z_][a-zA-Z0-9_\s]*$")) {
                MessageBox.Show("Invalid UserName");
                validationFailed = true;
            }
            if (textBox2.Password == ""){
                MessageBox.Show("Plz. enter the Password");
                validationFailed = true;
            }
            //Password length Validation   
            if (textBox2.Password.Length < 6){
                MessageBox.Show("Password length should be minimum of 6 characters!");
                validationFailed = true;
            }
            //EmailID validation 
            if (!Regex.IsMatch(textBox3.Text.Trim(), @"^([a-zA-Z_])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$")) {
                MessageBox.Show("Invalid EmailId");
                validationFailed = true;
            }
            //Phone Number Length Validation 
            if (textBox4.Text.Length != 10) {
                MessageBox.Show("Invalid PhonNo");
                validationFailed = true;
            }
            if (!validationFailed)
            {

                // Write to the Isolated Storage
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    Boolean isUnique = true;
                    foreach (var UserInfo in MainPage.ObjUserDataList)
                    {
                        if (UserInfo.Username == textBox1.Text)
                        {
                            isUnique = false;
                        }

                    }
                    if (isUnique)
                    {
                        using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("People.xml", FileMode.OpenOrCreate))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<UserInfo>));
                            using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                            {
                                serializer.Serialize(xmlWriter, GeneratePersonData());
                                NavigationService.Navigate(new Uri("/CongradNewUser.xaml", UriKind.Relative));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry! user name already exists.");
                    }

                }
            }
        }

        private List<UserInfo> GeneratePersonData()
        {
            List<UserInfo> data = new List<UserInfo>();
            UserInfo ui = new UserInfo();
            ui.Username = textBox1.Text;
            ui.Password = textBox2.Password;
            ui.EmailAdd = textBox3.Text;
            ui.Mobile = Convert.ToInt32(textBox4.Text);
            data.Add(ui);
            return data;
        }
    }
}
