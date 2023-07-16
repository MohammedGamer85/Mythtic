using mythos.Core;
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
        private ImportData importData = new();
        private PublicVars vars = new();
        public MainWindow()
        {
            InitializeComponent();
            /*?
            Add the startup classes/funcations here idot. I know you will forget that and put them some were else.
            I am clear here only with the only exption being View funcations they stay inside there view files*/

        }
    }
}
