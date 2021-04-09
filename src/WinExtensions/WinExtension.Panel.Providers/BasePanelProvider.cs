using System;
using System.Threading;

namespace WinExtension.Panel.Providers
{
    public class BasePanelProvider
    {
        public delegate void UpdateHandler(BasePanelProvider sender, string data);
        public event UpdateHandler Update;

        protected virtual void OnUpdate(string data)
        {
            Update?.Invoke(this, data);
        }

        protected virtual string Provide()
        {
            throw new NotImplementedException("Override this virtual method");
        }
    }


    public class TimedPanelProvider : BasePanelProvider
    {
        private readonly Timer _internalTimer;

        public TimedPanelProvider(int millis)
        {
            this._internalTimer = new Timer(TimerCallback, null, 0, millis);
        }

        private void TimerCallback(Object o)
        {
            OnUpdate(Provide());
        }
    }
}
