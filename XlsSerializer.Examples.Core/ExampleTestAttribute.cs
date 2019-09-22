using System;
using System.Reflection;

namespace XlsSerializer.Examples.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExampleTestAttribute : Attribute
    {
        public ExampleTestAttribute(int order, string title)
        {
            Order = order;
            Title = title;
        }

        public int Order { get; }

        public string Title { get; }

        internal static Tuple<Type, ExampleTestAttribute> Find(Type type)
        {
            return new Tuple<Type, ExampleTestAttribute>(type,
                type.GetCustomAttribute(typeof(ExampleTestAttribute)) as ExampleTestAttribute ??
                new ExampleTestAttribute(-1, type.Name));
        }
    }
}
