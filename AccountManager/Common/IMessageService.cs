using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Common
{
    public interface IMessageService
    {
        void Show(string message);
        void Show(string message, string caption);
    }
}
