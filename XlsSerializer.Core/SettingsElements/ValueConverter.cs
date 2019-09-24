using System;
using System.Collections.Generic;

using XlsSerializer.Core.SettingsElements.Defaults;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.SettingsElements
{
    public sealed class ValueConverter : IValueConverterBuilder
    {
        private readonly Dictionary<Type, ITypeConverter> m_converters;

        public static readonly IValueConverterBuilder Default;

        static ValueConverter()
        {
            var defaultConverters = new Dictionary<Type, ITypeConverter>
            {
                [typeof(DateTime)] = new DateStringConvertor()
            };

            Default = new ValueConverter(defaultConverters);
        }

        private ValueConverter(IDictionary<Type, ITypeConverter> converters)
        {
            m_converters = new Dictionary<Type, ITypeConverter>(converters);
        }

        public object ToCellValue(Type valueType, object input)
        {
            valueType = valueType ?? input?.GetType();

            if (valueType == null)
            {
                return null;
            }

            if (m_converters.TryGetValue(valueType, out var converter))
            {
                return converter.ToCellValue(input);
            }

            if (ReflectionHelper.TryUnwrapUnderlyingType(valueType, out var underlying))
            {
                return ToCellValue(underlying, input);
            }

            return input;
        }

        public object FromCellValue(Type desiredType, object input)
        {
            if (m_converters.TryGetValue(desiredType, out var converter))
            {
                return converter.FromCellValue(desiredType, input);
            }

            if ((input != null) && (ReflectionHelper.TryUnwrapUnderlyingType(desiredType, out var underlying)))
            {
                return FromCellValue(underlying, input);
            }
            
            if (input == null)
            {
                return null;
            }

            return Convert.ChangeType(input, desiredType);
        }

        public IValueConverterBuilder RegisterTypeConverter(params ITypeConverter[] converter)
        {
            var newConverter = new ValueConverter(m_converters);

            foreach (var c in converter)
            {
                newConverter.m_converters[c.ConvertedType] = c;
            }
            
            return newConverter;
        }
    }
}
