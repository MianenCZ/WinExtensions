using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WinExtension.Panel.Providers
{

    public class SystemInformation
    {
        public static SystemInformation Info
        {
            get
            {
                if (_infoSingleton is null)
                    _infoSingleton = new SystemInformation();
                return _infoSingleton;
            }
        }
        private static SystemInformation _infoSingleton;


        private System.Diagnostics.PerformanceCounter m_memoryCounter;
        private System.Diagnostics.PerformanceCounter m_CPUCounter;
        private System.Diagnostics.PerformanceCounter m_CPUTemperatureCounter;

        private SystemInformation()
        {
#pragma warning disable CA1416 // Validate platform compatibility
            m_memoryCounter = new PerformanceCounter
            {
                CategoryName = "Memory",
                CounterName = "Available MBytes"
            };

            m_CPUCounter = new System.Diagnostics.PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };

            m_CPUTemperatureCounter = new System.Diagnostics.PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "Temperature",
            };
#pragma warning restore CA1416 // Validate platform compatibility
        }

        public float GetAvailableMemory()
        {
            return m_memoryCounter.NextValue();
        }

        public float GetCPULoad()
        {
            return m_CPUCounter.NextValue();
        }

        public float GetCPUTemperature()
        {
            return m_CPUTemperatureCounter.NextValue();
        }


        public static void GetAllCounters()
        {
            foreach (var cat in PerformanceCounterCategory.GetCategories())
            {
                Console.WriteLine(cat.CategoryName);
                try
                {
                    foreach (var count in cat.GetCounters("_Total"))
                    {
                        Console.WriteLine("\t" + count.CounterName);
                    }
                }
                catch { }
            }
        }

        public static void GetProcessorCounters()
        {
            foreach (var count in PerformanceCounterCategory.GetCategories().First(x => x.CategoryName == "Processor").GetCounters("_Total"))
            {
                Console.WriteLine("\t" + count.CounterName);
            }
        }

        public static void GetProcessorInformationCounters()
        {
            foreach (var count in PerformanceCounterCategory.GetCategories().First(x => x.CategoryName == "Processor Information").GetCounters("_Total"))
            {
                Console.WriteLine("\t" + count.CounterName);
            }
        }
    }
}
