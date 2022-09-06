using eveDirect.Shared.EsiConnector.Logic;
using eveDirect.Shared.EsiConnector.Models.SSO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;

namespace eveDirect.Shared.EsiConnector
{
    public class EsiClient : IEsiClient
    {
        HttpClient client { get; }
        EsiConfig config { get; }
        ILogger logger { get; }

        public EsiClient(IOptions<EsiConfig> _config):this(_config, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public EsiClient(IOptions<EsiConfig> _config, ILogger logger)
        {
            config = _config.Value;
            client = new HttpClient();
            this.logger = logger;

            if(config.ProxyAddr?.Length > 0)
            {
                var credentials = new NetworkCredential(
                        userName: config.ProxyUser,
                        password: config.ProxyPass);

                var proxy = new WebProxy
                {
                    Address = new Uri($"http://{config.ProxyAddr}:{config.ProxyPort}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,

                    // *** These creds are given to the proxy server, not the web server ***
                    Credentials = credentials
                };

                var httpClientHandler = new HttpClientHandler { Proxy = proxy, PreAuthenticate = true, UseDefaultCredentials = false, Credentials = credentials };
                client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            }


            //if(logger != null)
            //    EsiRequest._logger = logger;

            // Enforce user agent value
            if (string.IsNullOrEmpty(config.UserAgent))
                throw new ArgumentException("Для вашего блага, пожалуйста, укажите значение 'X-User-Agent'. Это может быть имя персонажа и/или название проекта. CCP с большей вероятностью свяжутся с вами, чем прекратит доступ к ESI, если вы предоставите что-то, что может идентифицировать вас в галактике New Eden.");
            else
                client.DefaultRequestHeaders.Add("X-User-Agent", config.UserAgent);

            SSO = new SsoLogic(client, config, logger);
            Alliance = new AllianceLogic(client, config, logger);
            Assets = new AssetsLogic(client, config, logger);
            Bookmarks = new BookmarksLogic(client, config, logger);
            Calendar = new CalendarLogic(client, config, logger);
            Character = new CharacterLogic(client, config, logger);
            Clones = new ClonesLogic(client, config, logger);
            Contacts = new ContactsLogic(client, config, logger);
            Contracts = new ContractsLogic(client, config, logger);
            Corporation = new CorporationLogic(client, config, logger);
            Dogma = new DogmaLogic(client, config, logger);
            FactionWarfare = new FactionWarfareLogic(client, config, logger);
            Fittings = new FittingsLogic(client, config, logger);
            Fleets = new FleetsLogic(client, config, logger);
            Incursions = new IncursionsLogic(client, config, logger);
            Industry = new IndustryLogic(client, config, logger);
            Insurance = new InsuranceLogic(client, config, logger);
            Killmails = new KillmailsLogic(client, config, logger);
            Location = new LocationLogic(client, config, logger);
            Loyalty = new LoyaltyLogic(client, config, logger);
            Mail = new MailLogic(client, config, logger);
            Market = new MarketLogic(client, config, logger);
            Opportunities = new OpportunitiesLogic(client, config, logger);
            PlanetaryInteraction = new PlanetaryInteractionLogic(client, config, logger);
            Routes = new RoutesLogic(client, config, logger);
            Search = new SearchLogic(client, config, logger);
            Skills = new SkillsLogic(client, config, logger);
            Sovereignty = new SovereigntyLogic(client, config, logger);
            Status = new StatusLogic(client, config, logger);
            Universe = new UniverseLogic(client, config, logger);
            UserInterface = new UserInterfaceLogic(client, config, logger);
            Wallet = new WalletLogic(client, config, logger);
            Wars = new WarsLogic(client, config, logger);
        }

        public SsoLogic SSO { get; set; }
        public AllianceLogic Alliance { get; set; }
        public AssetsLogic Assets { get; set; }
        public BookmarksLogic Bookmarks { get; set; }
        public CalendarLogic Calendar { get; set; }
        public CharacterLogic Character { get; set; }
        public ClonesLogic Clones { get; set; }
        public ContactsLogic Contacts { get; set; }
        public ContractsLogic Contracts { get; set; }
        public CorporationLogic Corporation { get; set; }
        public DogmaLogic Dogma { get; set; }
        public FactionWarfareLogic FactionWarfare { get; set; }
        public FleetsLogic Fleets { get; set; }
        public FittingsLogic Fittings { get; set; }
        public IncursionsLogic Incursions { get; set; }
        public IndustryLogic Industry { get; set; }
        public InsuranceLogic Insurance { get; set; }
        public KillmailsLogic Killmails { get; set; }
        public LocationLogic Location { get; set; }
        public LoyaltyLogic Loyalty { get; set; }
        public MailLogic Mail { get; set; }
        public MarketLogic Market { get; set; }
        public OpportunitiesLogic Opportunities { get; set; }
        public PlanetaryInteractionLogic PlanetaryInteraction { get; set; }
        public RoutesLogic Routes { get; set; }
        public SearchLogic Search { get; set; }
        public SkillsLogic Skills { get; set; }
        public StatusLogic Status { get; set; }
        public SovereigntyLogic Sovereignty { get; set; }
        public UniverseLogic Universe { get; set; }
        public UserInterfaceLogic UserInterface { get; set; }
        public WalletLogic Wallet { get; set; }
        public WarsLogic Wars { get; set; }


        public void SetCharacterData(AuthorizedCharacterData data)
        {
            Assets = new AssetsLogic(client, config, logger, data);
            Bookmarks = new BookmarksLogic(client, config, logger, data);
            Calendar = new CalendarLogic(client, config, logger, data);
            Character = new CharacterLogic(client, config, logger, data);
            Clones = new ClonesLogic(client, config, logger, data);
            Contacts = new ContactsLogic(client, config, logger, data);
            Contracts = new ContractsLogic(client, config, logger, data);
            Corporation = new CorporationLogic(client, config, logger, data);
            FactionWarfare = new FactionWarfareLogic(client, config, logger, data);
            Fittings = new FittingsLogic(client, config, logger, data);
            Fleets = new FleetsLogic(client, config, logger, data);
            Industry = new IndustryLogic(client, config, logger, data);
            Killmails = new KillmailsLogic(client, config, logger, data);
            Location = new LocationLogic(client, config, logger, data);
            Loyalty = new LoyaltyLogic(client, config, logger, data);
            Mail = new MailLogic(client, config, logger, data);
            Market = new MarketLogic(client, config, logger, data);
            Opportunities = new OpportunitiesLogic(client, config, logger, data);
            PlanetaryInteraction = new PlanetaryInteractionLogic(client, config, logger, data);
            Search = new SearchLogic(client, config, logger, data);
            Skills = new SkillsLogic(client, config, logger, data);
            UserInterface = new UserInterfaceLogic(client, config, logger, data);
            Wallet = new WalletLogic(client, config, logger, data);
            //Universe = new UniverseLogic(client, config, logger, data);
        }

        public void SetIfNoneMatchHeader(string eTag)
        {
            Alliance.ETag = eTag;
            Assets.ETag = eTag;
            Bookmarks.ETag = eTag;
            Calendar.ETag = eTag;
            Character.ETag = eTag;
            Clones.ETag = eTag;
            Contacts.ETag = eTag;
            Contracts.ETag = eTag;
            Corporation.ETag = eTag;
            FactionWarfare.ETag = eTag;
            Fittings.ETag = eTag;
            Fleets.ETag = eTag;
            Industry.ETag = eTag;
            Killmails.ETag = eTag;
            Location.ETag = eTag;
            Loyalty.ETag = eTag;
            Mail.ETag = eTag;
            Market.ETag = eTag;
            Opportunities.ETag = eTag;
            PlanetaryInteraction.ETag = eTag;
            Search.ETag = eTag;
            Skills.ETag = eTag;
            UserInterface.ETag = eTag;
            Wallet.ETag = eTag;
            Universe.ETag = eTag;
        }
    }

    public interface IEsiClient
    {
        SsoLogic SSO { get; set; }
        AllianceLogic Alliance { get; set; }
        AssetsLogic Assets { get; set; }
        BookmarksLogic Bookmarks { get; set; }
        CalendarLogic Calendar { get; set; }
        CharacterLogic Character { get; set; }
        ClonesLogic Clones { get; set; }
        ContactsLogic Contacts { get; set; }
        ContractsLogic Contracts { get; set; }
        CorporationLogic Corporation { get; set; }
        DogmaLogic Dogma { get; set; }
        FactionWarfareLogic FactionWarfare { get; set; }
        FittingsLogic Fittings { get; set; }
        FleetsLogic Fleets { get; set; }
        IncursionsLogic Incursions { get; set; }
        IndustryLogic Industry { get; set; }
        InsuranceLogic Insurance { get; set; }
        KillmailsLogic Killmails { get; set; }
        LocationLogic Location { get; set; }
        LoyaltyLogic Loyalty { get; set; }
        MailLogic Mail { get; set; }
        MarketLogic Market { get; set; }
        OpportunitiesLogic Opportunities { get; set; }
        PlanetaryInteractionLogic PlanetaryInteraction { get; set; }
        RoutesLogic Routes { get; set; }
        SearchLogic Search { get; set; }
        SkillsLogic Skills { get; set; }
        SovereigntyLogic Sovereignty { get; set; }
        StatusLogic Status { get; set; }
        UniverseLogic Universe { get; set; }
        UserInterfaceLogic UserInterface { get; set; }
        WalletLogic Wallet { get; set; }
        WarsLogic Wars { get; set; }

        void SetCharacterData(AuthorizedCharacterData data);
        void SetIfNoneMatchHeader(string eTag);
    }
}
