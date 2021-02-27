using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace WinExtension.Common.Helpers
{
    public static class PropertyHelper<TClass>
    {
        public static string GetName<TProperty>(Expression<Func<TClass, TProperty>> selector)
        {
            var prop = GetProperty(selector);

            string name = prop.Name;

            if (TryGetAttribute(prop, out DataMemberAttribute datatMemberAttribute))
                name = datatMemberAttribute.Name;
            if (TryGetAttribute(prop, out DisplayAttribute displayAttribute))
                name = displayAttribute.Name;
            if (TryGetAttribute(prop, out DisplayNameAttribute displayNameAttribute))
                name = displayNameAttribute.DisplayName;

            return name;
        }

        public static PropertyInfo GetProperty<TProperty>(Expression<Func<TClass, TProperty>> selector,
                                                          bool checkOnOwnProperty = false)
        {
            Guard.IsNotNull(selector, nameof(selector));

            Expression body = selector;
            if (body is LambdaExpression)
                body = ((LambdaExpression)body).Body;
            if (body.NodeType != ExpressionType.MemberAccess)
                throw new InvalidOperationException();

            var member = body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{body}' refers to a method, not a property.");

            Type classType = typeof(TClass);
            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException($"Expression '{body}' refers to a field, not a property.");

            if (checkOnOwnProperty)
            {
                if (propertyInfo.ReflectedType != null && propertyInfo.ReflectedType != classType &&
                    classType.IsSubclassOf(propertyInfo.ReflectedType) == false)
                    throw new ArgumentException(
                        $"Expression '{body}' refers to a property that is not from type {classType}.");
            }

            return propertyInfo;
        }

        public static IEnumerable<TAttribute> GetAttribute<TAttribute>(PropertyInfo propertyInfo)
            where TAttribute : System.Attribute
        {
            Guard.IsNotNull(propertyInfo, nameof(propertyInfo));

            var attributes = propertyInfo.GetCustomAttributes(typeof(TAttribute), true);
            foreach (TAttribute attribute in attributes)
            {
                yield return attribute;
            }
        }

        /// <summary>
        /// Tries get atrribute of type <typeparamref name="TAttribute"/> defined by property <paramref name="selector"/> on class <typeparamref name="TClass"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to filter</typeparam>
        /// <typeparam name="TProperty">Type of selected property</typeparam>
        /// <param name="propertyInfo">Defnition of property</param>
        /// <param name="attribute">Result of request. <c>null</c> when return value is <c>false</c></param>
        /// <returns><c>true</c> id attribute exists, <c>false</c> othervise</returns>
        public static bool TryGetAttribute<TAttribute, TProperty>(Expression<Func<TClass, TProperty>> selector,
            out TAttribute attribute)
                where TAttribute : System.Attribute
        {
            var propertyInfo = GetProperty(selector);
            return TryGetAttribute(propertyInfo, out attribute);
        }

        /// <summary>
        /// Tries get attribute of type <typeparamref name="TAttribute"/> from <paramref name="propertyInfo"/> of class <typeparamref name="TClass"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to filter</typeparam>
        /// <param name="propertyInfo">Defnition of property</param>
        /// <param name="attribute">Result of request. <c>null</c> when return value is <c>false</c></param>
        /// <returns><c>true</c> id attribute exists, <c>false</c> othervise</returns>
        public static bool TryGetAttribute<TAttribute>(PropertyInfo propertyInfo, out TAttribute attribute)
           where TAttribute : System.Attribute
        {
            var attributes = GetAttribute<TAttribute>(propertyInfo).ToArray();
            if (attributes.Length > 1)
            {
                throw new SerializationException(
                    $"Property {propertyInfo.Name} does not have unique [{typeof(TAttribute).Name}] annotation.");
            }

            if (attributes.Length == 1)
            {
                attribute = attributes[0];
                return true;
            }

            attribute = null;
            return false;
        }
    }
}
