namespace eveDirect.Clients.Web
{
    public static class Routes
    {
        public const string MarketRoot = "market";
        public const string MarketContracts = MarketRoot + "/contracts";
        public const string MarketOrders = MarketRoot + "/orders";

        public const string CharactersRoot = "characters";
        public const string CharacterNewBorns = CharactersRoot + "/newborns";
        public const string CharacterMigrations = CharactersRoot + "/migrations";

        public const string CorporationsRoot = "corporations";
        public const string CorporationMigrations = CorporationsRoot + "/migrations";
        public const string CorporationNew = CorporationsRoot + "/new";

        public const string AlliancesRoot = "alliances";
        public const string AlliancesNew = AlliancesRoot + "/new";

        public const string UniverseRoot = "universe";
        public const string UniverseMap = UniverseRoot + "/map";
        public const string UniverseTypes = UniverseRoot + "/types";

        public const string WarfaceRoot = "warface";
        public const string WarfaceKills = WarfaceRoot + "/kills";
        public const string WarfaceWars = WarfaceRoot + "/wars";

        public const string AccountRoot = "account";
        public const string AccountLogin = AccountRoot + "/login";
        public const string AccountRegister = AccountRoot + "/register";
    }
}
