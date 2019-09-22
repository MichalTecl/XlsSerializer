using System;
using System.Collections.Concurrent;
using XlsSerializer.Core.Attributes;
using XlsSerializer.Core.Mapping;

namespace XlsSerializer.Core.Features
{
    internal static class DocumentMapper
    {
        private static readonly ConcurrentDictionary<Type, DocumentModel> m_mappings = new ConcurrentDictionary<Type, DocumentModel>();

        public static DocumentModel GetMapping(Type t)
        {
            return m_mappings.GetOrAdd(t, tp => new DocumentModel(CellBinding.CreateMap(tp)));
        }
    }
}
