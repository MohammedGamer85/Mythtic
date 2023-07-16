using Microsoft.Windows.Themes;
using mythos.Core;
using mythos.MVVM.Model;
using mythos.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
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

namespace mythos.MVVM.ViewModel
{
    public class HomeViewModel
    {
        PublicVars vars = new();
        ImportData importData = new();
        public ObservableCollection<MyModsModel> Mods { get; set; } = new ();
        public string NumberOfMods { get; set; }

        public HomeViewModel()
        {
            UpdateMyModsList();
        }

        public void UpdateMyModsList()
        {
            for (int i = 0; i < importData.modDiectionary.Count; i++)
            {
                Mods.Add(new MyModsModel
                {
                    Title = importData.modDiectionary[i].GetName(),
                    ImageSource = importData.modDiectionary[i].GetImageSource(),
                    author = importData.modDiectionary[i].GetAuthor(),
                    Description = importData.modDiectionary[i].GetDescription(),
                    informationPanel = importData.modDiectionary[i].GetReleaseDate() + " | " + importData.modDiectionary[i].GetVsersion() + " | " + importData.modDiectionary[i].GetAuthor(),
                    IsLoaded = importData.modDiectionary[i].GetIsLoaded()

                }); ;
            }
            NumberOfMods = $"My Mods | Number of mods {Mods.Count}";
        }
    }
}
