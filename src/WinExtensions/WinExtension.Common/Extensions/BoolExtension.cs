using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FS_Game.Common.Extensions
{
    public static class BoolExtension
    {
        /// <summary>
        /// Takes <b>REFERENCE</b> to <c>bool</c> <paramref name="_extended_value"/>. Provides API to execute <paramref name="action"/> only onec when <paramref name="_extended_value"/> is set to <c>true</c>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Input Value</term>
        ///         <description>Action</description>
        ///     </listheader>
        ///     <item>
        ///         <term><c>true</c></term>
        ///         <description><paramref name="action"/> is executed, <paramref name="_extended_value"/> is set to false, <c>true</c> is returned</description>
        ///     </item>
        ///     <item>
        ///         <term><c>false</c></term>
        ///         <description><c>false</c> is returned</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="_extended_value">Reference to <c>bool</c> value that is validation lock for action</param>
        /// <param name="action">Action to perform only once</param>
        /// <example>
        /// <code lang="cs">
        /// bool first = true;
        /// for (int i = 0; i &lt; 10; i++)
        /// {
        ///     first.Once(() =&gt; Console.WriteLine(i));
        /// }
        ///
        /// //Output: 0
        /// </code>
        /// </example>
        /// <returns>Input value of <paramref name="_extended_value"/></returns>
        public static bool Once(this ref bool _extended_value, Action action)
        {
            if (_extended_value)
            {
                _extended_value = false;
                action();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Takes REFERENCE to <c>bool</c> <paramref name="_extended_value"/>. Provides API to execute <paramref name="action"/> except first usage of <paramref name="_extended_value"/> when is set to <c>true</c>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Input Value</term>
        ///         <description>Action</description>
        ///     </listheader>
        ///     <item>
        ///         <term><c>true</c></term>
        ///         <description><paramref name="_extended_value"/> is set to false, <c>true</c> is returned</description>
        ///     </item>
        ///     <item>
        ///         <term><c>false</c></term>
        ///         <description><paramref name="action"/> is executed, <c>false</c> is returned</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="_extended_value">Reference to <c>bool</c> value that is validation lock for action</param>
        /// <param name="action">Action to perform anytime except first execution</param>
        /// <example>
        /// <code lang="cs">
        /// bool first = true;
        /// for (int i = 0; i &lt; 10; i++)
        /// {
        ///     first.ExceptFirst(() =&gt; Console.Write(' '));
        ///     Console.Write(i);
        /// }
        /// //Output: 0 1 2 3 4 5 6 7 8 9
        /// </code>
        /// </example>
        /// <returns>Input value of <paramref name="_extended_value"/></returns>
        public static bool ExceptFirst(this ref bool _extended_value, Action action)
        {
            if (_extended_value)
            {
                _extended_value = false;
                return true;
            }
            else
            {
                action();
                return false;
            }
        }
    }
}