using Microsoft.VisualBasic;
using mythos.Model;
using mythos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mythos.Models
{
    public class ImportedModsModel : ObservableObject
    {
        //! Privte
        private string? _name;

        private Version? _version;
        //! Needed
        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; SetTitle(); }
        }

        public string ImageSource { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string SubDescription { get; set; } = string.Empty;

        //! Optional (Auto if not imported)

        public bool IsLoaded { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public Version Version
        {
            get { return _version; }
            set { _version = value; SetValues(); }
        }

        //! Auto Added
        public string? Title { get; set; } = string.Empty;

        public string? InformationPanel { get; set; } = string.Empty;

        public string? ShortendInformationPanel { get; set; } = string.Empty;

        public int? FontSize { get; set; }

        void SetValues()
        {
            this.InformationPanel = "LastUpdated: " + this.LastUpdated + "Version: " + this.Version + "Auther: " + this.Author;
            this.ShortendInformationPanel = this.Version + "\nBy " + this.Author;
        }

        void SetTitle()
        {
            if (Name.Length > 16)
            {
                Title = Name.Substring(0, 16) + "...";
                FontSize = 18;
            }
            else
                this.Title = this.Name;

            if (Name.Length > 12)
                FontSize = 18;

            if (Name.Length <= 12)
                FontSize = 24;

            if (Name.Length <= 8)
                FontSize = 36;

            SetValues();
        }
    }
}
