namespace eveDirect.Shared.Api
{
    public static class ApiRoutes
    {
        public static class Alliance
        {
            public const string Route = "api/ally";

            public const string Names = "ns";
            public const string Name = "n";

            public const string Preview = "pr";

            public const string CurrentCharacters = "nc";
        }
        public static class Character
        {
            public const string Route = "api/char";
            
            public const string Names = "ns";
            public const string Name = "n";
            
            public const string Infos = "ps";
            public const string Info = "p";
            
            public const string CorpHistories = "hs";
            public const string CorpHistory = "h";
            
            public const string AllyHistory = "ah";

            public const string Previews = "prs";
            public const string Preview = "pr";
            public const string Wars = "w";
            public const string Kills = "k";
            public const string Contracts_V1 = "mc/v1";

            public const string Newborns = "nb";
            public const string NewbornsCorps = "nbcorp";
            public const string NewbornsChart = "nbc";
            public const string NewbornsCCP = "nbccp";

            public const string MigrationsRoot = "mgr";
        }
        public static class Corporation
        {
            public const string Route = "api/corp";
            public const string Names = "ns";
            public const string Name = "n";
            public const string Infos = "ps";
            public const string Info = "p";
            public const string Contracts = "c";
            public const string ContractsHistory = "ch";
            public const string MembersMigrations = "cm";
            public const string AllianceHistory = "al";
            public const string Previews = "prs";
            public const string Preview = "pr";
            public const string Wars = "w";
            public const string Kills = "k";
        }
        public static class Market
        {
            public const string Route = "api/market";
            public const string Market_Orders = "mo";
            public const string Market_Groups = "mg";
            public const string Market_AllRegionsSystems = "mrs";
            public const string Market_AllRegions = "mr";
        }
        public static class Contracts
        {
            public const string Route = "api/contracts";
            public const string Details = "d";
            public const string Items = "i";
            public const string Bids = "b";
            public const string List = "l";
            public const string Groups = "g";
        }
        public static class Universe
        {
            public const string Route = "api/univ";
            public const string TypeName = "tn";
            public const string TypeNames = "tns";
            public const string TypeAllNames = "tna";
            public const string TypeNamesMissing = "tnm";
            public const string LocationName = "ln";
            public const string LocationNames = "lns";
        }
        public static class Translations
        {
            public const string Route = "api/tr";
            public const string Version = "v";
            public const string Strings = "s";
        }

        public static class Web
        {
            public const string Route = "api/web";
            public const string RootValues = "rv";
        }

        public static class Search
        {
            public const string Route = "api/s";
            public const string BySubString = "s";
        }
    }
}
