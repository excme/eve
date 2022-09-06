namespace eveDirect.Caching
{
    public static class CacheKeys
    {
        public static string ApiMarketGroups { get { return "a_mgs8_{0}"; } }
        //public static string ApiMarketActiveRegionsSystems { get { return "a_mars"; } }
        public static string ApiMarketAllRegionsSystems { get { return "a_mrs"; } }
        public static string ApiMerketContractsRegions { get { return "a_mr"; } }

        public static string ApiMarketContractsList { get { return "a_mcl2"; } }
        public static string ApiMarketContractsGroups { get { return "a_mcgs_{0}"; } }

        public static string ApiUniverseTypeName { get { return "a_utn1_{0}"; } }
        public static string ApiUniverseLocationNames { get { return "a_ulns"; } }
    }
}
