using AccountManager.Models;
using AccountManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountManager
{
    class Program
    {
        [STAThread]
        public static void Main()
        {

            new Application().Run(new MainWindow());
        }
    }
}
