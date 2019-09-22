using XlsSerializer.Core.Attributes.Contract;

namespace XlsSerializer.Core.Utils
{
    public static class AttributeExtensions
    {
        public static int GetProcessingOrder(this object a)
        {
            if (a is IHasProcessingOrder order)
            {
                return order.ProcessingOrder;
            }
            else
            {
                return 0;
            }
        }
    }
}
