namespace XlsSerializer.Core.SettingsElements
{
    public interface IValueConverterBuilder : IValueConverter
    {
        IValueConverterBuilder RegisterTypeConverter(params ITypeConverter[] converter);
    }
}
