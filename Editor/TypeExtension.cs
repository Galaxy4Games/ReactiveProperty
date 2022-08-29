using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MVVM.Editor
{
    public static class TypeExtension
    {
        public static List<FieldInfo> GetAllReactiveFields(this Type type)
        {
            return type
                .GetFields()
                .Where(f => !Attribute.IsDefined(f, typeof(IgnoreGenerationAttribute)) &&
                            f.FieldType.GetInterfaces().Contains(typeof(IReactiveProperty)))
                .ToList();
        }

        public static List<PropertyInfo> GetAllReactiveProperties(this Type type)
        {
            return type
                .GetProperties()
                .Where(f => !Attribute.IsDefined(f, typeof(IgnoreGenerationAttribute)) &&
                            f.PropertyType.GetInterfaces().Contains(typeof(IReactiveProperty)))
                .ToList();
        }

        public static List<string> GetAllReactive(this Type type)
        {
            var props =
                type.GetAllReactiveProperties()
                    .Select(p => p.Name);

            var fields =
                type.GetAllReactiveFields()
                    .Select(p => p.Name);

            return props
                .Concat(fields)
                .ToList();
        }

        public static List<string> GetAllReactive(this Type type, Type genericType)
        {
            bool FilterPropertyInfoByGeneric(Type f)
            {
                if (genericType != null)
                    return f.GenericTypeArguments.Length > 0 &&
                           f.GenericTypeArguments[0] == genericType;

                return f.GenericTypeArguments.Length == 0; //если пришло нулл, то искать все у кого генериков 0
            }

            var props =
                type.GetAllReactiveProperties()
                    .Where((f) => FilterPropertyInfoByGeneric(f.PropertyType))
                    .Select(p => p.Name);

            var fields =
                type.GetAllReactiveFields()
                    .Where((f) => FilterPropertyInfoByGeneric(f.FieldType))
                    .Select(p => p.Name);

            return props
                .Concat(fields)
                .ToList();
        }
    }
}