using System;
using System.Reflection;

namespace XlsSerializer.Core.SettingsElements
{
    public sealed class PropertyAndOwnerInstance
    {
        public readonly PropertyInfo Property;
        public readonly object PropertyOwner;

        public PropertyAndOwnerInstance(PropertyInfo property, object propertyOwner)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            PropertyOwner = propertyOwner ?? throw new ArgumentNullException(nameof(propertyOwner));
        }
    }
}
