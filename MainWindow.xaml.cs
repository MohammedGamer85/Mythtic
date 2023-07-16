using mythos.Core;
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
using System.IO;
using System.Security.AccessControl;

namespace mythos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImportedMod jsonData = new(); //todo replace
        PublicVars vars = new();
        public MainWindow()
        {
            InitializeComponent();
            CreateNecessaryFiles();
            jsonData.Import(); //todo be replace
            ImportUserAccountData();
            /*?
            Add the startup classes/funcations here idot. I know you will forget that and put them some were else.
            I am clear here only with the only exption being View funcations they stay inside there view files*/

        }

        void CreateNecessaryFiles()
        {
            if (!Directory.Exists(vars.userDocFiles))
            {
                Directory.CreateDirectory(vars.userDocFiles);
            }
        }

        public void ImportUserAccountData()
        {
            var testarray = "dfsdff";
            MessageBox.Show(testarray);
            string TEMPName = "asda";
            int fontSize = 15; // The number of charcters after which all the letter will be removed and ... added insted
            if (TEMPName.Length > 15) { TEMPName = (TEMPName.Remove(15) + "..." ); }
            List<UserAccountData> contacts = new List<UserAccountData>()
            {
                new UserAccountData
                {
                    userName = TEMPName, //TODO Change all of these to accuale vars
                    AccountToken = "non",
                    imageSource = "https://cdn.discordapp.com/avatars/843270188686245888/f3c27f99128b8510f28e868e98b9c058",
                    fontSize = fontSize,
                }
            };
            ProfileDisplay.ItemsSource= contacts;

        }
    }
}
