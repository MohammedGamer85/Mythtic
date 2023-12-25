using Avalonia;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Win32.SafeHandles;
using mythtic.Classes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using mythtic.Services;
using mythtic.Services.PreloadedInformation;

namespace mythtic.Data
{

    /*! 
        This is Used to validate the data that is gotten by the user before making a request to the
        Api to avoid the code that is dealing with the requests to crach or have errors. + it converts the recived info into other formats.
     */


    public class AuthenticationRequests
    {
        private readonly HttpClientHelper _httpClientHelper = new HttpClientHelper();

        public async Task<ObservableCollection<ListOfDiscoverModsItem>> MythosDiscoverModList()
        {
            string url = "https://mythos.legendsmodding.com/api/myths?";

            ListOfDiscoverModClassRecived result = await _httpClientHelper.GetRequest<ListOfDiscoverModClassRecived>(url);

            if (result.Success == true)
            {
                ObservableCollection<ListOfDiscoverModsItem> formatedResult = new();
                var i = result.Data;
                foreach (var x in i)
                {
                    x.Name ??= "Unknown";
                    x.GameMode ??= "None";
                    x.Description ??= "There is no ShotDescription";
                    x.Category ??= "Uncategorized";
                    if (x.DefaultImage == null)
                        x.DefaultImage = "https://mythos.legendsmodding.com/favicon.ico";
                    else
                        x.DefaultImage = $"https://static.legendsmodding.com/myths/{x.DefaultImage}.jpg";
                    x.LatestVersion ??= "0,0,0";
                    if (x.ReleaseDate == null)
                    {
                        x.ReleaseDate = DateTime.Now.ToString();
                    }
                    x.InformationPanel = "LatestVersion: " + x.ReleaseDate + "\nVersion: " + x.LatestVersion + "  GameMode: " + x.GameMode;
                    formatedResult.Add(x);
                }
                return formatedResult;
            }
            else
            {
                return null;
            }
        }

        public async Task<DisocverModItem> GetMythosModDetials(int webId)
        {
            string url = "https://mythos.legendsmodding.com/api/myth/" + webId;

            DisocverModItemInfoClassRecived result = await _httpClientHelper.GetRequest<DisocverModItemInfoClassRecived>(url);

            if (result.Success == true)
            {
                var x = result.Data;
                x.Name ??= "Unknown";
                x.GameMode ??= "None";
                x.ShortDescription ??= "There is no ShotDescription";
                x.LongDescription ??= "There is no LongDescription";
                if (x.DefaultImage == null)
                    x.DefaultImage = "https://mythos.legendsmodding.com/favicon.ico";
                else
                    x.DefaultImage = $"https://static.legendsmodding.com/myths/{x.DefaultImage}.jpg";
                for(int i =0; i < x.Images.Length; i++)
                {
                    x.Images[i].Url = $"https://static.legendsmodding.com/myths/{x.Images[i].ImageHash}.jpg";
                }
                if (x.ReleaseDate == null)
                {
                    x.ReleaseDate = DateTime.Now;
                }
                x.InformationPanel = "LatestVersion: " + x.ReleaseDate + "\nVersion: " + x.Versions[0].Version + "  GameMode: " + x.GameMode;
                return x;
            }

            return null;
        }

        //! Deals With login requests.
        internal async Task<Account?> LoginRequest(string email, string password)
        {
            string url = "https://mythos.legendsmodding.com/api/authenticate";

            var loginRequest = new LoginRequest()
            {
                Email = email,
                Password = password,
            };

            var result = await _httpClientHelper.PostRequest<Account, LoginRequest>(url, loginRequest);

            if (result.Success == true)
            {
                Logger.Log("LoginRequest Successed API Success Status:True (AuthenticationRequests/LoginRequest)\n");
                return result;
            }
            else
            {
                Logger.Log("loginRequst Falled API Success Status:False (AuthenticationRequests/LoginRequest)\n");
                return default;
            }
        }
    }
}