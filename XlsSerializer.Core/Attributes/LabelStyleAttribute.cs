using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using XlsSerializer.Core.Attributes.Contract;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class LabelStyleAttribute : CellStyleAttributeBase, ISetupLabelCell
    {
        private static readonly ConcurrentDictionary<PropertyInfo, ISetupLabelCell[]> s_styles = new ConcurrentDictionary<PropertyInfo, ISetupLabelCell[]>();
        private static readonly ISetupLabelCell s_defaultLabelSetup = new LabelStyleAttribute
        {
            FontStyle = FontStyle.Bold,
            Locked = true
        };

        public void Apply(PropertyInfo boundProperty, ExcelRange cell)
        {
            Apply(cell);
        }

        internal static ISetupLabelCell[] FindStyle(PropertyInfo forProperty)
        {
            return s_styles.GetOrAdd(forProperty, property =>
                {
                    var s = forProperty?.DeclaringType?.GetCustomAttributes().OfType<ISetupLabelCell>()
                        .OrderBy(a => a.GetProcessingOrder())
                        .Concat(forProperty.GetCustomAttributes().OfType<ISetupLabelCell>()
                            .OrderBy(a => a.GetProcessingOrder())).ToArray();

                    if ((s?.Length ?? 0) == 0)
                    {
                        s = new[] {s_defaultLabelSetup};
                    }

                    return s;
                });
        } 
    }
}
