using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountManager.Common
{
    /// <summary>
    /// Abstract base class for a ViewModel implementation.
    /// </summary>
    //[DataContract]
    public abstract class ViewModel : ViewModelBase
    {
        /// <summary>
        /// Reference to view object
        /// </summary>
        public object ViewObject
        {
            get; set;
        }

        protected IMessageService MessageServiceInstance => ServiceLocator.Current.GetInstance<IMessageService>();

        protected IWindowService WindowServiceInstance => ServiceLocator.Current.GetInstance<IWindowService>();

        protected void ClearInteractionRequestedListener()
        {
            InteractionRequested = null;
        }

        public virtual void OnDeactivated()
        {
            this.ClearInteractionRequestedListener();
        }

        protected void CheckAndExecuteActionOnUIThread(Action action)
        {
            if (Application.Current?.Dispatcher?.CheckAccess() == true)
            {
                action();
            }
            else
            {
                Application.Current?.Dispatcher?.BeginInvoke(new Action(() =>
                {
                    action();
                }));
            }
        }


        public class InteractionRequestedEventArgs : EventArgs
        {
            public string InteractionName;
            public object[] Args;
            public InteractionRequestedEventArgs(string interactionName, params object[] args)
            {
                InteractionName = interactionName;
                Args = args;
            }

        }

        public event EventHandler<InteractionRequestedEventArgs> InteractionRequested;
        protected void RaiseInteractionRequest(string interactionName, params object[] args)
        {
            InteractionRequested?.Invoke(this, new InteractionRequestedEventArgs(interactionName, args));
        }

    }
}
