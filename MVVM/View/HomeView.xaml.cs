using mythos.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace mythos.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DisplayMyMods();
        }

        public void DisplayMyMods()
        {
            List<MyModsModel> contacts = new List<MyModsModel>()
            {
                new MyModsModel
                {
                    ImageSource = "https://th.bing.com/th/id/OIP.J8-fsFy6EHYDaooV0_VTgAHaHa?w=163&h=180&c=7&r=0&o=5&pid=1.7",
                    Description = "Description: This is the Description",
                    Auther = "By Auther",
                    IsLoaded = true,
                    Title = "Title",
                    informationPanel  ="information Panel : Time Added: Day/Month/Year 00:00pm/am | Version: 1.0 | By Auther",
                },
                new MyModsModel
                {
                    ImageSource = "https://mythos-static.umbrielstudios.com/users/Mohammed85.jpg",
                    Description = "Description: This is the Description",
                    Auther = "By The Devourer",
                    IsLoaded = true,
                    Title = "Piglin Survival",
                    informationPanel  = "Day/Month/Year 00:00pm/am | 1.0 | By The Devoure",
                },
                new MyModsModel
                {
                    ImageSource = "https://mythos.umbrielstudios.com/static/media/mythos_logo.df59d1413fde4aaba5d9.png",
                    Description = "Description: you are spawned in a world with the ability to build well of fate and pvp bases with infinit matirals and allays",
                    Auther = "By Mohammed 8229",
                    IsLoaded = true,
                    Title = "CreativeModeFlat",
                    informationPanel  = "Day/Month/Year 00:00pm/am | 1.0 | By Mohammed 8229",
                },
                new MyModsModel
                {
                    ImageSource = "https://th.bing.com/th/id/OIP.PnAy7sgKcjfVXXBH89AA2QEsDj?w=211&h=176&c=7&r=0&o=5&pid=1.7",
                    Description = "Description: Pvp by balnced and with more mobes and buildables to allow for more deverse stratiges",
                    Auther = "By EpicDensetsu",
                    IsLoaded = true,
                    Title = "PVP Extended",
                    informationPanel  = "Day/Month/Year 00:00pm/am  |  1.0  | By EpicDensetsu",
                },
            };
            ListContacts.ItemsSource= contacts;
            int num = ListContacts.Items.Count;
            NumberOfMods.Text = $"My Mods | Number of mods {num}";
        }
    }
}
