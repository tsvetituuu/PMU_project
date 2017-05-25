using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Encyclopedia
{
    public partial class Articles : PhoneApplicationPage
    {
        public Articles()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RadioButton level1 = (RadioButton)this.level1;
            RadioButton level2 = (RadioButton)this.level2;
            RadioButton level3 = (RadioButton)this.level3;
            if ((bool)level1.IsChecked)
            {
                this.textBox.Text = "Dog is a member of genus Canus that part of the wolflike candis. They have 4 legs and their long association with humas has led dogs to be uniquely attuned to human behavior. Dogs vary widely in shape , size and colors.";
            }
            else
            {
                if ((bool)level2.IsChecked)
                {
                    this.textBox.Text = "Dogs perform many roles for people such as hunting, herding, pulling load, protection, assisting poliice and military, companionship and more recently, aiding handicapped individuals. This influence on human society has given them the sobriquet 'man's mest friend'.";
                }
                else
                {
                    this.textBox.Text = "Domestic dogs have been selectivly bred for millennia for various behaviors, sensory capabilities, and physical attributes. Modern dog breeds show more variation in size, appearance, and behavior than any other domestic animal. Dogs are predators and scavengers, and like many other predatory mammals, the dog has powerful muscles, fused wrist bones, a cardiovascular system that supports both sprinting and endurance, and teeth for catching and tearing." ;
                }
            }
            

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            RadioButton level1 = (RadioButton)this.level1;
            RadioButton level2 = (RadioButton)this.level2;
            RadioButton level3 = (RadioButton)this.level3;
            if ((bool)level1.IsChecked)
            {
                this.textBox.Text = "Cat is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them. Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards.";
            }
            else
            {
                if ((bool)level2.IsChecked)
                {
                    this.textBox.Text = "Cats have a high breeding rate. Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of cats by neutering, as well as the abandonment of formet household pets, has resulted in large numbers of feral cats worldwide, requiring population control.";
                }
                else
                {
                    this.textBox.Text = "Cats have either a mutualistic ot commensal relationship with humans. Two main theories are given about how cats were domesticated. In one, people deliberately tamed cats in a process of artificial selection as they where useful predators of vermin. The alternative idea is that cats were simple tolerated by people and gadually diverged from their wild relatives through natural seection, as they adapted to hunting the vermin found around humas in towns and villages.";
                }
            }
        }
        private void txtBody_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }

      
    }
}