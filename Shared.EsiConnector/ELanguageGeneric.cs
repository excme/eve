namespace eveDirect.Shared.EsiConnector
{
    public static class ELanguageGeneric
    {
        public static string ToArg(this object enumItem)
        {
            return enumItem.ToString().Replace('_', '-');
        }
    }
}
