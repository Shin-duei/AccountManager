using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Common
{
    public interface IWindowService
    {
        void ShowWindow(object viewObject);
        bool? ShowDialog(object viewObject);
        void Close(object viewObject);
    }
}
