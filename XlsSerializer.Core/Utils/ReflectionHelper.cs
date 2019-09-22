using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XlsSerializer.Core.Utils
{
    internal static class ReflectionHelper
    {
        private static readonly MethodInfo s_toArrayMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray));

        public static bool GetIsCollection(Type t, out Type itemType, bool acceptArray)
        {
            itemType = null;
            if (typeof(IEnumerable).IsAssignableFrom(t) &&
                ((itemType = t.GenericTypeArguments.FirstOrDefault()) != null))
            {
                return true;
            }

            if (acceptArray && t.IsArray)
            {
                itemType = t.GetElementType();
                return true;
            }

            return false;
        }

        public static object ConstructListOf(Type t)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(t);

            return Activator.CreateInstance(constructedListType);
        }

        public static void PopulateCollection(IEnumerable source, object target)
        {
            var addMethod = target.GetType().GetMethods()
                .FirstOrDefault(m => m.Name.Equals(nameof(ICollection<object>.Add)) && m.GetParameters().Length == 1);

            if (addMethod == null)
            {
                throw new NotSupportedException($"Cannot populate collection of type {target.GetType()}");
            }

            foreach (var item in source)
            {
                addMethod.Invoke(target, new [] { item });
            }
        }

        public static object ConvertGenericCollectionToArray(object genericCollection, Type arrayItemType)
        {
            return s_toArrayMethod.MakeGenericMethod(arrayItemType).Invoke(null, new[] { genericCollection });
        }
        
        public static void PopulateCollectionProperty(object owner, PropertyInfo property, IList<object> rawItems)
        {
            if (!GetIsCollection(property.PropertyType, out var itemType, true))
            {
                throw new InvalidOperationException($"Cannot populate collection of type {property.PropertyType} in property {property.Name}");
            }

            var targetCollection = (property.CanRead ? (property.GetValue(owner)) : null) ?? ConstructListOf(itemType);
            var addMethod = (targetCollection?.GetType().GetMethods().FirstOrDefault(m => m.Name.Equals("Add") && m.GetParameters().Length == 1));

            if (addMethod == null)
            {
                throw new InvalidOperationException($"Cannot populate collection of type {property.PropertyType} in property {property.Name}");
            }

            foreach (var item in rawItems)
            {
                addMethod.Invoke(targetCollection, new[] {item});
            }

            if (property.CanWrite)
            {
                if (property.PropertyType.IsArray)
                {
                    var array = s_toArrayMethod.MakeGenericMethod(itemType).Invoke(null, new[] {targetCollection});
                    property.SetValue(owner, array, null);
                    return;
                }

                property.SetValue(owner, targetCollection, null);
            }
        }

        public static object ConvertToList(IEnumerable items, Type itemType)
        {
            var result = ConstructListOf(itemType);
            PopulateCollection(items, result);

            return ConvertGenericCollectionToArray(result, itemType);
        }
    }
}
