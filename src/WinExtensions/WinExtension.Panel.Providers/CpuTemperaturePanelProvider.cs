using System;
using System.Collections.Generic;
using System.Text;

namespace WinExtension.Panel.Providers
{
    public class CpuTemperaturePanelProvider : TimedPanelProvider
    {
        public CpuTemperaturePanelProvider() : base(1000) { }

        protected override string Provide()
        {
            return $"{SystemInformation.Info.GetCPUTemperature()}";
        }
    }

}
