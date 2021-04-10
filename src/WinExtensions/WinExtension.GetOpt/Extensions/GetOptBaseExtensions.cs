using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace WinExtension.GetOpt
{
    public static class GetOptBaseExtensions
    {
        /// <summary>
        /// Apply building rules to add <paramref name="shortName"/> and <paramref name="longName"/> to Option definition
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="builder">option definition builder</param>
        /// <param name="shortName">short version of option</param>
        /// <param name="longName">long version of option</param>
        /// <returns>modified option definition builder</returns>
        public static IOptDefinitionFluentBuilder<TTarget> HasNames<TTarget>(
            this IOptDefinitionFluentBuilder<TTarget> builder,
            string shortName,
            string longName) 
            where TTarget : new()
        {
            return builder.HasLongName(longName).HasShortName(shortName);
        }
    }
}
