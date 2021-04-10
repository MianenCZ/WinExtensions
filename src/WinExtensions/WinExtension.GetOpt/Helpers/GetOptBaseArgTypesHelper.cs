using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Helpers;

namespace WinExtension.GetOpt
{
    public static class GetOptBaseArgTypesHelper
    {
        public static IArgDefinitionFluentBuilder<TTarget, string> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, string>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, s => s);
        }

        public static IArgDefinitionFluentBuilder<TTarget, int> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, int>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToIntFormatter());
        }

        public static IArgDefinitionFluentBuilder<TTarget, byte> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, byte>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToByteFormatter());
        }

        public static IArgDefinitionFluentBuilder<TTarget, char> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, char>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToCharFormatter());
        }

        public static IArgDefinitionFluentBuilder<TTarget, Guid> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, Guid>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToGuidFormatter());
        }

        public static IArgDefinitionFluentBuilder<TTarget, float> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, float>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToFloatFormatter());
        }

        public static IArgDefinitionFluentBuilder<TTarget, double> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, double>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToDoubleFormatter());
        }

        public static IArgDefinitionFluentBuilder<TTarget, decimal> AddArg<TTarget>(
            this GetOptBase<TTarget> getOpt,
            Expression<Func<TTarget, decimal>> selector) where TTarget : new()
        {
            return getOpt.AddArg(selector, DefaultFormatters.ToDecimalFormatter());
        }
    }
}
