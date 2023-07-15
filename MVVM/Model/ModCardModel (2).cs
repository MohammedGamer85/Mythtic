using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mythos.MVVM.Model
{
    internal class ModCardModel
    {
        public string ModName { get; set; }

        public string ImageSource { get; set; }

        public ObservableCollection<ModModel> Mods { get; set; }

        public string LastMod => Mods.Last().Mod;
    }
}
