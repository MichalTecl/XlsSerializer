using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XlsSerializer.Core.Attributes.Contract;
using XlsSerializer.Core.Features;
using XlsSerializer.Core.Utils;

namespace XlsSerializer.Core.Mapping
{
    internal sealed class SheetAssociation
    {
        private static readonly IHasSheet[] s_emptySetup = new IHasSheet[0];
        private static readonly ConcurrentDictionary<Type, SheetAssociation[]> s_associations = new ConcurrentDictionary<Type, SheetAssociation[]>(); 

        public readonly PropertyInfo BoundProperty;
        public readonly string SheetName;
        private readonly int m_sheetOrder;
        public readonly IList<IHasSheet> SheetSetup;
        
        private SheetAssociation(PropertyInfo boundProperty, string sheetName, int sheetOrder, IEnumerable<IHasSheet> sheetSetup)
        {
            BoundProperty = boundProperty;
            SheetName = sheetName;
            m_sheetOrder = sheetOrder;
            SheetSetup = new List<IHasSheet>(sheetSetup);
        }

        public static IEnumerable<SheetAssociation> GetSheetAssociations(Type t)
        {
            return s_associations.GetOrAdd(t, modelType =>
            {
                var map = new List<SheetAssociation>();

                var modelSheet = modelType.GetCustomAttributes().OfType<IHasSheet>().LastOrDefault();
                if (modelSheet != null)
                {
                    map.Add(new SheetAssociation(null, modelSheet.SheetName, modelSheet.SheetOrder,
                        new[] {modelSheet}));
                }
                else if (modelType.GetProperties().SelectMany(p => p.GetCustomAttributes()).OfType<IHasRowIndex>().Any())
                {
                    map.Add(new SheetAssociation(null, XlsSheetSerializerCore.DefaultSheetName, 0, s_emptySetup));
                }

                foreach (var property in modelType.GetProperties())
                {
                    var sheetAttributes = property.GetCustomAttributes().OfType<IHasSheet>()
                        .OrderBy(a => a.GetProcessingOrder()).ToList();
                    var sheetAttribute = sheetAttributes.LastOrDefault();

                    if (sheetAttribute != null)
                    {
                        map.Add(new SheetAssociation(property, sheetAttribute.SheetName, sheetAttribute.SheetOrder, sheetAttributes));
                    }
                }

                if (!map.Any())
                {
                    map.Add(new SheetAssociation(null, XlsSheetSerializerCore.DefaultSheetName, 0, s_emptySetup));
                }

                return map.OrderBy(a => a.m_sheetOrder).ToArray();
            });
        }
    }
}
