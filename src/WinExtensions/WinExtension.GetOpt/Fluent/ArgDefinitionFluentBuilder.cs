using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Exceptions;

namespace WinExtension.GetOpt
{
    public class ArgDefinitionFluentBuilder<TTarget, TProp> : IArgDefinitionFluentBuilder<TTarget, TProp> where TTarget: new()
    {
        private readonly ArgDefinition<TTarget> _arg;
        public ArgDefinitionFluentBuilder(ArgDefinition<TTarget> arg)
        {
            this._arg = arg;
        }


        public IArgDefinitionFluentBuilder<TTarget, TProp> WithName(string name)
        {
            this._arg.ArgName = name;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> IsRequired(bool isRequired = true)
        {
            this._arg.IsRequired = isRequired;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> IsVariadic(bool isVariadic = true)
        {
            this._arg.IsVariadic = isVariadic;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> HasDescription(string description)
        {
            this._arg.Description = description;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> HasHelpText(string help)
        {
            this._arg.Help = help;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> WithCustomFormater(Func<string, TProp> formater)
        {
            this._arg.formaterInfo = formater.Method;
            return this;
        }
    }

    internal static class DefaultFormaters
    {
        internal static string DefaultFormatToString(string source)
            => source;
        internal static int DefaultFormatToInt(string source)
        {
            try
            {
                return int.Parse(source);
            }
            catch (Exception ex)
            {
                throw new ArgumentFormatException(ex.Message);
            }
        }
        internal static float DefaultFormatToFloat(string source)
        {
            try
            {
                return float.Parse(source);
            }
            catch (Exception ex)
            {
                throw new ArgumentFormatException(ex.Message);
            }
        }
        internal static double DefaultFormatToDouble(string source)
        {
            try
            {
                return double.Parse(source);
            }
            catch (Exception ex)
            {
                throw new ArgumentFormatException(ex.Message);
            }
        }
        internal static decimal DefaultFormatToDecimal(string source)
        {
            try
            {
                return decimal.Parse(source);
            }
            catch (Exception ex)
            {
                throw new ArgumentFormatException(ex.Message);
            }
        }
    }
}
