using mythos.DataClasses;
using mythos.MVVM.Model;
using mythos.MVVM.View;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
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

namespace mythos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImportUserAccuntData();
            //?add the startup classes/funcations here idot. I know you will forget that and put them some were else.

        }

        public void ImportUserAccuntData()
        {
            string TEMPName = "EpicDensetsu1234567";
            int fontSize = 15; // The number of charcters after which all the letter will be removed and ... added insted
            if (TEMPName.Length > 15) { TEMPName = (TEMPName.Remove(15) + "..." ); }
            List<UserAccuntData> contacts = new List<UserAccuntData>()
            {
                new UserAccuntData
                {
                    userName = TEMPName, //TODO Change all of these to accuale vars
                    accuntToken = "non",
                    imageSource = "https://cdn.discordapp.com/avatars/843270188686245888/f3c27f99128b8510f28e868e98b9c058",
                    fontSize = fontSize,
                }
            };
            ProfileDisplay.ItemsSource= contacts;
        }
    }
}
