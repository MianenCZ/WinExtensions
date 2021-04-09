using System;
using System.Collections.Generic;
using System.Text;

namespace WinExtension.Panel.Providers
{
    public class DateTimePanelProvider : TimedPanelProvider
    {
        public DateTimePanelProvider() : base(10000) { }


        protected override string Provide()
        {
            return DateTime.Now.ToString();
        }
    }
}
