using System;
using System.Collections.Generic;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.EsiConnector.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alliances",
                columns: table => new
                {
                    alliance_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    ticker = table.Column<string>(nullable: true),
                    creator_id = table.Column<int>(nullable: false),
                    creator_corporation_id = table.Column<int>(nullable: false),
                    executor_corporation_id = table.Column<int>(nullable: false),
                    date_founded = table.Column<DateTime>(type: "date", nullable: false),
                    faction_id = table.Column<int>(nullable: false),
                    corps_count = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    last_info_updated = table.Column<DateTime>(nullable: true),
                    last_corps_list_update = table.Column<DateTime>(nullable: true),
                    comigr = table.Column<List<int>>(type: "jsonb", nullable: true),
                    pr = table.Column<EveOnlineAlliancePreview>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alliances", x => x.alliance_id);
                });

            migrationBuilder.CreateTable(
                name: "chars",
                columns: table => new
                {
                    character_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    corporation_id = table.Column<int>(nullable: false),
                    alliance_id = table.Column<int>(nullable: false),
                    birthday = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<byte>(nullable: false),
                    race_id = table.Column<int>(nullable: false),
                    bloodline_id = table.Column<int>(nullable: false),
                    ancestry_id = table.Column<int>(nullable: false),
                    security_status = table.Column<float>(nullable: false),
                    faction_id = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    last_update_publicInfo = table.Column<DateTime>(nullable: false),
                    last_update_affiliation = table.Column<DateTime>(nullable: false),
                    lastUpdate_corpHistory = table.Column<DateTime>(nullable: true),
                    kma = table.Column<ICollection<EveOnlineKillMail>>(type: "jsonb", nullable: true),
                    pr = table.Column<EveOnlineCharacterPreview>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chars", x => x.character_id);
                });

            migrationBuilder.CreateTable(
                name: "chars_portraits",
                columns: table => new
                {
                    character_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chars_portraits", x => x.character_id);
                });

            migrationBuilder.CreateTable(
                name: "charshistory",
                columns: table => new
                {
                    record_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(nullable: false),
                    corporation_id = table.Column<int>(nullable: false),
                    prev_corp_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    character_id = table.Column<int>(nullable: false),
                    next_corp_id = table.Column<int>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_charshistory", x => x.record_id);
                });

            migrationBuilder.CreateTable(
                name: "contrs",
                columns: table => new
                {
                    contract_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    buyout = table.Column<double>(nullable: false),
                    collateral = table.Column<double>(nullable: false),
                    date_expired = table.Column<DateTime>(nullable: true),
                    date_issued = table.Column<DateTime>(nullable: true),
                    days_to_complete = table.Column<int>(nullable: false),
                    end_location_id = table.Column<long>(nullable: false),
                    for_corporation = table.Column<bool>(nullable: false),
                    issuer_corporation_id = table.Column<int>(nullable: false),
                    issuer_id = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    reward = table.Column<double>(nullable: false),
                    start_location_id = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    type = table.Column<byte>(nullable: false),
                    volume = table.Column<double>(nullable: false),
                    actual = table.Column<bool>(nullable: false),
                    region_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contrs", x => x.contract_id);
                });

            migrationBuilder.CreateTable(
                name: "contrsbids",
                columns: table => new
                {
                    bid_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_bid = table.Column<DateTime>(nullable: true),
                    amount = table.Column<float>(nullable: false),
                    contract_id = table.Column<int>(nullable: false),
                    isDisable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contrsbids", x => x.bid_id);
                });

            migrationBuilder.CreateTable(
                name: "contrsitems",
                columns: table => new
                {
                    record_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(nullable: false),
                    type_id = table.Column<int>(nullable: false),
                    is_included = table.Column<bool>(nullable: false),
                    is_blueprint_copy = table.Column<bool>(nullable: false),
                    item_id = table.Column<long>(nullable: false),
                    material_efficiency = table.Column<int>(nullable: false),
                    runs = table.Column<int>(nullable: false),
                    time_efficiency = table.Column<int>(nullable: false),
                    contract_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contrsitems", x => x.record_id);
                });

            migrationBuilder.CreateTable(
                name: "corps",
                columns: table => new
                {
                    corporation_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    ticker = table.Column<string>(nullable: true),
                    member_count = table.Column<int>(nullable: false),
                    ceo_id = table.Column<int>(nullable: false),
                    tax_rate = table.Column<float>(nullable: false),
                    creator_id = table.Column<int>(nullable: false),
                    alliance_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_founded = table.Column<DateTime>(type: "date", nullable: false),
                    url = table.Column<string>(nullable: true),
                    home_station_id = table.Column<int>(nullable: false),
                    shares = table.Column<long>(nullable: false),
                    faction_id = table.Column<int>(nullable: false),
                    war_eligible = table.Column<bool>(nullable: false),
                    ncp = table.Column<bool>(nullable: false),
                    last_update_publicInfo = table.Column<DateTime>(nullable: true),
                    lastUpdate_allianceHistory = table.Column<DateTime>(nullable: true),
                    chmigr = table.Column<List<int>>(type: "jsonb", nullable: true),
                    pr = table.Column<EveOnlineCorporationPreview>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corps", x => x.corporation_id);
                });

            migrationBuilder.CreateTable(
                name: "corpshistory",
                columns: table => new
                {
                    record_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(nullable: false),
                    alliance_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    corporation_id = table.Column<int>(nullable: false),
                    next_ally_id = table.Column<int>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corpshistory", x => x.record_id);
                });

            migrationBuilder.CreateTable(
                name: "corpsloaltyoffers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corpsloaltyoffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iaccounts",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    status = table.Column<byte>(nullable: false),
                    сreated = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iaccroles",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    expire = table.Column<DateTime>(nullable: true),
                    caption = table.Column<string>(nullable: true),
                    can_remove = table.Column<bool>(nullable: false),
                    type = table.Column<byte>(nullable: false),
                    corporation_ceo = table.Column<bool>(nullable: false),
                    corporation_id = table.Column<int>(nullable: false),
                    corporation_assets = table.Column<bool>(nullable: false),
                    corporation_accounting = table.Column<bool>(nullable: false),
                    corporation_contacts = table.Column<bool>(nullable: false),
                    corporation_fwars = table.Column<bool>(nullable: false),
                    corporation_privateInfo = table.Column<bool>(nullable: false),
                    corporation_manufacture = table.Column<bool>(nullable: false),
                    corporation_killmails = table.Column<bool>(nullable: false),
                    corporation_members = table.Column<bool>(nullable: false),
                    corpMembers_accounting = table.Column<bool>(nullable: false),
                    corpMembers_assets = table.Column<bool>(nullable: false),
                    corpMembers_calendar = table.Column<bool>(nullable: false),
                    corpMembers_history = table.Column<bool>(nullable: false),
                    corpMembers_manufacture = table.Column<bool>(nullable: false),
                    corpMembers_roles = table.Column<bool>(nullable: false),
                    corpMembers_fwars = table.Column<bool>(nullable: false),
                    corpMembers_contacts = table.Column<bool>(nullable: false),
                    corpMembers_settings = table.Column<bool>(nullable: false),
                    corpMembers_fleet = table.Column<bool>(nullable: false),
                    corpMembers_info = table.Column<bool>(nullable: false),
                    corpMembers_killmails = table.Column<bool>(nullable: false),
                    corpMembers_mail = table.Column<bool>(nullable: false),
                    character_id = table.Column<int>(nullable: false),
                    character_owner = table.Column<bool>(nullable: false),
                    character_accounting = table.Column<bool>(nullable: false),
                    character_history = table.Column<bool>(nullable: false),
                    character_fleet = table.Column<bool>(nullable: false),
                    character_assets = table.Column<bool>(nullable: false),
                    character_manufacture = table.Column<bool>(nullable: false),
                    character_calendar = table.Column<bool>(nullable: false),
                    character_roles = table.Column<bool>(nullable: false),
                    character_fwars = table.Column<bool>(nullable: false),
                    character_contacts = table.Column<bool>(nullable: false),
                    character_info = table.Column<bool>(nullable: false),
                    character_killmails = table.Column<bool>(nullable: false),
                    character_mail = table.Column<bool>(nullable: false),
                    character_settings = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "indac",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "indsys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_indsys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "issorequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    owner_id = table.Column<int>(nullable: false),
                    dt = table.Column<DateTime>(type: "date", nullable: false),
                    type = table.Column<byte>(nullable: false),
                    sso_Records_updates = table.Column<int>(nullable: false),
                    db_changes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issorequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kms",
                columns: table => new
                {
                    killmail_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    moon_id = table.Column<int>(nullable: true),
                    war_id = table.Column<int>(nullable: true),
                    killmail_hash = table.Column<string>(nullable: true),
                    killmail_time = table.Column<DateTime>(nullable: true),
                    solar_system_id = table.Column<int>(nullable: true),
                    total_destroyed = table.Column<double>(nullable: false),
                    total_dropped = table.Column<double>(nullable: false),
                    fitting = table.Column<double>(nullable: false),
                    preview = table.Column<EveOnlineKillmailPreview>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kms", x => x.killmail_id);
                });

            migrationBuilder.CreateTable(
                name: "kmsz",
                columns: table => new
                {
                    OnDate = table.Column<string>(nullable: false),
                    zKillBoard_Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kmsz", x => x.OnDate);
                });

            migrationBuilder.CreateTable(
                name: "mgs",
                columns: table => new
                {
                    market_group_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    types = table.Column<List<int>>(type: "jsonb", nullable: true),
                    parent_group_id = table.Column<int>(nullable: false),
                    enname = table.Column<string>(nullable: true),
                    dename = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true),
                    endescription = table.Column<string>(nullable: true),
                    dedescription = table.Column<string>(nullable: true),
                    frdescription = table.Column<string>(nullable: true),
                    jadescription = table.Column<string>(nullable: true),
                    rudescription = table.Column<string>(nullable: true),
                    zhdescription = table.Column<string>(nullable: true),
                    kodescription = table.Column<string>(nullable: true),
                    childs = table.Column<List<int>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mgs", x => x.market_group_id);
                });

            migrationBuilder.CreateTable(
                name: "mhps",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    order_count = table.Column<long>(nullable: false),
                    volume = table.Column<long>(nullable: false),
                    highest = table.Column<double>(nullable: false),
                    average = table.Column<double>(nullable: false),
                    lowest = table.Column<double>(nullable: false),
                    region_id = table.Column<int>(nullable: false),
                    type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mhps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mo",
                columns: table => new
                {
                    order_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    duration = table.Column<int>(nullable: false),
                    issued = table.Column<DateTime>(nullable: false),
                    type_id = table.Column<int>(nullable: false),
                    location_id = table.Column<long>(nullable: false),
                    range = table.Column<int>(nullable: false),
                    is_buy_order = table.Column<bool>(nullable: false),
                    min_volume = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    volume_total = table.Column<int>(nullable: false),
                    volume_remain = table.Column<int>(nullable: false),
                    system_id = table.Column<int>(nullable: false),
                    isDisable = table.Column<bool>(nullable: false),
                    region_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mo", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "ogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ots",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "uancestries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bloodline_id = table.Column<int>(nullable: false),
                    icon_id = table.Column<int>(nullable: false),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true),
                    dedescription = table.Column<string>(nullable: true),
                    endescription = table.Column<string>(nullable: true),
                    frdescription = table.Column<string>(nullable: true),
                    jadescription = table.Column<string>(nullable: true),
                    rudescription = table.Column<string>(nullable: true),
                    zhdescription = table.Column<string>(nullable: true),
                    kodescription = table.Column<string>(nullable: true),
                    deshort_description = table.Column<string>(nullable: true),
                    enshort_description = table.Column<string>(nullable: true),
                    frshort_description = table.Column<string>(nullable: true),
                    jashort_description = table.Column<string>(nullable: true),
                    rushort_description = table.Column<string>(nullable: true),
                    zhshort_description = table.Column<string>(nullable: true),
                    koshort_description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uancestries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ubloodLines",
                columns: table => new
                {
                    bloodline_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    race_id = table.Column<int>(nullable: false),
                    ship_type_id = table.Column<int>(nullable: false),
                    corporation_id = table.Column<int>(nullable: false),
                    perception = table.Column<int>(nullable: false),
                    willpower = table.Column<int>(nullable: false),
                    charisma = table.Column<int>(nullable: false),
                    memory = table.Column<int>(nullable: false),
                    intelligence = table.Column<int>(nullable: false),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true),
                    dedescription = table.Column<string>(nullable: true),
                    endescription = table.Column<string>(nullable: true),
                    frdescription = table.Column<string>(nullable: true),
                    jadescription = table.Column<string>(nullable: true),
                    rudescription = table.Column<string>(nullable: true),
                    zhdescription = table.Column<string>(nullable: true),
                    kodescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ubloodLines", x => x.bloodline_id);
                });

            migrationBuilder.CreateTable(
                name: "ucategories",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    published = table.Column<bool>(nullable: false),
                    groups = table.Column<List<int>>(nullable: true),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ucategories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "ufactions",
                columns: table => new
                {
                    faction_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    size_factor = table.Column<float>(nullable: false),
                    station_count = table.Column<int>(nullable: false),
                    station_system_count = table.Column<int>(nullable: false),
                    is_unique = table.Column<bool>(nullable: false),
                    solar_system_id = table.Column<int>(nullable: false),
                    corporation_id = table.Column<int>(nullable: false),
                    militia_corporation_id = table.Column<int>(nullable: false),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true),
                    dedescription = table.Column<string>(nullable: true),
                    endescription = table.Column<string>(nullable: true),
                    frdescription = table.Column<string>(nullable: true),
                    jadescription = table.Column<string>(nullable: true),
                    rudescription = table.Column<string>(nullable: true),
                    zhdescription = table.Column<string>(nullable: true),
                    kodescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ufactions", x => x.faction_id);
                });

            migrationBuilder.CreateTable(
                name: "ugraphics",
                columns: table => new
                {
                    graphic_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sof_race_name = table.Column<string>(nullable: true),
                    sof_fation_name = table.Column<string>(nullable: true),
                    sof_dna = table.Column<string>(nullable: true),
                    sof_hull_name = table.Column<string>(nullable: true),
                    collision_file = table.Column<string>(nullable: true),
                    graphic_file = table.Column<string>(nullable: true),
                    icon_folder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ugraphics", x => x.graphic_id);
                });

            migrationBuilder.CreateTable(
                name: "ugroups",
                columns: table => new
                {
                    group_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    published = table.Column<bool>(nullable: false),
                    category_id = table.Column<int>(nullable: false),
                    types = table.Column<List<int>>(type: "jsonb", nullable: true),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ugroups", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "ulocs",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<long>(nullable: false),
                    region_id = table.Column<long>(nullable: true),
                    constellation_id = table.Column<long>(nullable: true),
                    system_id = table.Column<long>(nullable: true),
                    planet_id = table.Column<long>(nullable: true),
                    type_id = table.Column<int>(nullable: true),
                    owner_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    type = table.Column<byte>(nullable: false),
                    position_x = table.Column<float>(nullable: true),
                    position_y = table.Column<float>(nullable: true),
                    position_z = table.Column<float>(nullable: true),
                    regionInfo = table.Column<UniverseRegionInfoResult>(type: "jsonb", nullable: true),
                    constellationInfo = table.Column<UniverseConstellationInfoResult>(type: "jsonb", nullable: true),
                    systemInfo = table.Column<UniverseSystemInfoResult>(type: "jsonb", nullable: true),
                    planetInfo = table.Column<UniversePlanetInfoResult>(type: "jsonb", nullable: true),
                    stargateInfo = table.Column<UniverseStargateInfoResult>(type: "jsonb", nullable: true),
                    moonInfo = table.Column<UniverseMoonInfoResult>(type: "jsonb", nullable: true),
                    starInfo = table.Column<UniverseStarInfoResult>(type: "jsonb", nullable: true),
                    stationInfo = table.Column<UniverseStationInfoResult>(type: "jsonb", nullable: true),
                    asteroidBeltInfo = table.Column<UniverseAsteroidBeltInfoResult>(type: "jsonb", nullable: true),
                    structureInfo = table.Column<CorporationStructuresResult.CorporationStructuresItem>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ulocs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "uraces",
                columns: table => new
                {
                    race_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alliance_id = table.Column<int>(nullable: false),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true),
                    dedescription = table.Column<string>(nullable: true),
                    endescription = table.Column<string>(nullable: true),
                    frdescription = table.Column<string>(nullable: true),
                    jadescription = table.Column<string>(nullable: true),
                    rudescription = table.Column<string>(nullable: true),
                    zhdescription = table.Column<string>(nullable: true),
                    kodescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uraces", x => x.race_id);
                });

            migrationBuilder.CreateTable(
                name: "utypes",
                columns: table => new
                {
                    type_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capacity = table.Column<float>(nullable: false),
                    dogma_attributes = table.Column<List<DogmaAttribute>>(type: "jsonb", nullable: true),
                    dogma_effects = table.Column<List<DogmaEffect>>(type: "jsonb", nullable: true),
                    graphic_id = table.Column<int>(nullable: true),
                    group_id = table.Column<int>(nullable: false),
                    icon_id = table.Column<int>(nullable: true),
                    market_group_id = table.Column<int>(nullable: true),
                    mass = table.Column<float>(nullable: false),
                    packaged_volume = table.Column<float>(nullable: false),
                    portion_size = table.Column<int>(nullable: true),
                    published = table.Column<bool>(nullable: false),
                    radius = table.Column<float>(nullable: false),
                    volume = table.Column<float>(nullable: false),
                    dename = table.Column<string>(nullable: true),
                    enname = table.Column<string>(nullable: true),
                    frname = table.Column<string>(nullable: true),
                    janame = table.Column<string>(nullable: true),
                    runame = table.Column<string>(nullable: true),
                    zhname = table.Column<string>(nullable: true),
                    koname = table.Column<string>(nullable: true),
                    dedescription = table.Column<string>(nullable: true),
                    endescription = table.Column<string>(nullable: true),
                    frdescription = table.Column<string>(nullable: true),
                    jadescription = table.Column<string>(nullable: true),
                    rudescription = table.Column<string>(nullable: true),
                    zhdescription = table.Column<string>(nullable: true),
                    kodescription = table.Column<string>(nullable: true),
                    img_tags = table.Column<List<string>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utypes", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "wars",
                columns: table => new
                {
                    war_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    declared = table.Column<DateTime>(nullable: false),
                    started = table.Column<DateTime>(nullable: true),
                    retracted = table.Column<DateTime>(nullable: true),
                    finished = table.Column<DateTime>(nullable: true),
                    mutual = table.Column<bool>(nullable: false),
                    open_for_allies = table.Column<bool>(nullable: false),
                    aggressor_ships_killed = table.Column<int>(nullable: false),
                    aggressor_isk_destroyed = table.Column<float>(nullable: false),
                    aggressor_alliance_id = table.Column<int>(nullable: true),
                    aggressor_corporation_id = table.Column<int>(nullable: true),
                    defender_ships_killed = table.Column<int>(nullable: false),
                    defender_isk_destroyed = table.Column<float>(nullable: false),
                    defender_alliance_id = table.Column<int>(nullable: true),
                    defender_corporation_id = table.Column<int>(nullable: true),
                    killmail_loaded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wars", x => x.war_id);
                });

            migrationBuilder.CreateTable(
                name: "iaccloginhist",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    IsSuccessed = table.Column<bool>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    accountId = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccloginhist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iaccloginhist_iaccounts_accountId",
                        column: x => x.accountId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "iaccuserclaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<decimal>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccuserclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iaccuserclaims_iaccounts_UserId",
                        column: x => x.UserId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "iaccuserlogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccuserlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_iaccuserlogins_iaccounts_UserId",
                        column: x => x.UserId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "iaccusertokens",
                columns: table => new
                {
                    UserId = table.Column<decimal>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccusertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_iaccusertokens_iaccounts_UserId",
                        column: x => x.UserId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "iconstrs",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    ConnectionStr = table.Column<string>(nullable: true),
                    owner_id = table.Column<int>(nullable: false),
                    owner_type = table.Column<byte>(nullable: false),
                    AccountId = table.Column<decimal>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iconstrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iconstrs_iaccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "issos",
                columns: table => new
                {
                    id = table.Column<decimal>(nullable: false),
                    pipe_request = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    added = table.Column<DateTime>(type: "date", nullable: false),
                    access_token = table.Column<string>(nullable: true),
                    refresh_token = table.Column<string>(nullable: true),
                    token_scopes = table.Column<List<string>>(type: "jsonb", nullable: true),
                    character_owner_hash = table.Column<string>(nullable: true),
                    last_owner_and_status_update = table.Column<DateTime>(type: "date", nullable: false),
                    accountId = table.Column<decimal>(nullable: false),
                    character_id = table.Column<int>(nullable: false),
                    character_name = table.Column<string>(nullable: true),
                    corporation_id = table.Column<int>(nullable: false),
                    corporation_name = table.Column<string>(nullable: true),
                    is_ceo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issos", x => x.id);
                    table.ForeignKey(
                        name: "FK_issos_iaccounts_accountId",
                        column: x => x.accountId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "iaccroleclaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<decimal>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccroleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iaccroleclaims_iaccroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "iaccroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "iaccrolesrefs",
                columns: table => new
                {
                    UserId = table.Column<decimal>(nullable: false),
                    RoleId = table.Column<decimal>(nullable: false),
                    id = table.Column<decimal>(nullable: false),
                    assign = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iaccrolesrefs", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_iaccrolesrefs_iaccroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "iaccroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_iaccrolesrefs_iaccounts_UserId",
                        column: x => x.UserId,
                        principalTable: "iaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kmsa",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    character_id = table.Column<int>(nullable: true),
                    corporation_id = table.Column<int>(nullable: true),
                    alliance_id = table.Column<int>(nullable: true),
                    ship_type_id = table.Column<int>(nullable: false),
                    faction_id = table.Column<int>(nullable: true),
                    killmailId = table.Column<int>(nullable: false),
                    d = table.Column<byte>(nullable: false),
                    security_status = table.Column<float>(nullable: true),
                    final_blow = table.Column<bool>(nullable: true),
                    damage_done = table.Column<int>(nullable: true),
                    weapon_type_id = table.Column<int>(nullable: true),
                    damage_taken = table.Column<int>(nullable: true),
                    px = table.Column<float>(nullable: true),
                    py = table.Column<float>(nullable: true),
                    pz = table.Column<float>(nullable: true),
                    location_id = table.Column<long>(nullable: true),
                    items = table.Column<List<EveOnlineKillMailVictimItemParent>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kmsa", x => x.id);
                    table.ForeignKey(
                        name: "FK_kmsa_kms_killmailId",
                        column: x => x.killmailId,
                        principalTable: "kms",
                        principalColumn: "killmail_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kmsa_kms_killmailId1",
                        column: x => x.killmailId,
                        principalTable: "kms",
                        principalColumn: "killmail_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "warsa",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alliance_id = table.Column<int>(nullable: true),
                    corporation_id = table.Column<int>(nullable: true),
                    excluded = table.Column<DateTime>(nullable: true),
                    warId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warsa", x => x.id);
                    table.ForeignKey(
                        name: "FK_warsa_wars_warId",
                        column: x => x.warId,
                        principalTable: "wars",
                        principalColumn: "war_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_character_id",
                table: "charshistory",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "IX_contrsbids_contract_id",
                table: "contrsbids",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_contrsitems_contract_id",
                table: "contrsitems",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_corpshistory_alliance_id",
                table: "corpshistory",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_corpshistory_corporation_id",
                table: "corpshistory",
                column: "corporation_id");

            migrationBuilder.CreateIndex(
                name: "IX_iaccloginhist_accountId",
                table: "iaccloginhist",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "iaccounts",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "iaccounts",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_iaccroleclaims_RoleId",
                table: "iaccroleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "iaccroles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_iaccrolesrefs_RoleId",
                table: "iaccrolesrefs",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_iaccuserclaims_UserId",
                table: "iaccuserclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_iaccuserlogins_UserId",
                table: "iaccuserlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_iconstrs_AccountId",
                table: "iconstrs",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_issos_accountId",
                table: "issos",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_kmsa_killmailId",
                table: "kmsa",
                column: "killmailId");

            migrationBuilder.CreateIndex(
                name: "IX_kmsa_killmailId1",
                table: "kmsa",
                column: "killmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_kmsa_alliance_id",
                table: "kmsa",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_kmsa_character_id",
                table: "kmsa",
                column: "character_id");

            migrationBuilder.CreateIndex(
                name: "IX_kmsa_corporation_id",
                table: "kmsa",
                column: "corporation_id");

            migrationBuilder.CreateIndex(
                name: "IX_kmsa_d",
                table: "kmsa",
                column: "d");

            migrationBuilder.CreateIndex(
                name: "IX_mhps_region_id",
                table: "mhps",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_mhps_type_id",
                table: "mhps",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ulocs_owner_id",
                table: "ulocs",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_ulocs_type",
                table: "ulocs",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_warsa_warId",
                table: "warsa",
                column: "warId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alliances");

            migrationBuilder.DropTable(
                name: "chars");

            migrationBuilder.DropTable(
                name: "chars_portraits");

            migrationBuilder.DropTable(
                name: "charshistory");

            migrationBuilder.DropTable(
                name: "contrs");

            migrationBuilder.DropTable(
                name: "contrsbids");

            migrationBuilder.DropTable(
                name: "contrsitems");

            migrationBuilder.DropTable(
                name: "corps");

            migrationBuilder.DropTable(
                name: "corpshistory");

            migrationBuilder.DropTable(
                name: "corpsloaltyoffers");

            migrationBuilder.DropTable(
                name: "iaccloginhist");

            migrationBuilder.DropTable(
                name: "iaccroleclaims");

            migrationBuilder.DropTable(
                name: "iaccrolesrefs");

            migrationBuilder.DropTable(
                name: "iaccuserclaims");

            migrationBuilder.DropTable(
                name: "iaccuserlogins");

            migrationBuilder.DropTable(
                name: "iaccusertokens");

            migrationBuilder.DropTable(
                name: "iconstrs");

            migrationBuilder.DropTable(
                name: "indac");

            migrationBuilder.DropTable(
                name: "indsys");

            migrationBuilder.DropTable(
                name: "issorequests");

            migrationBuilder.DropTable(
                name: "issos");

            migrationBuilder.DropTable(
                name: "kmsa");

            migrationBuilder.DropTable(
                name: "kmsz");

            migrationBuilder.DropTable(
                name: "mgs");

            migrationBuilder.DropTable(
                name: "mhps");

            migrationBuilder.DropTable(
                name: "mo");

            migrationBuilder.DropTable(
                name: "ogs");

            migrationBuilder.DropTable(
                name: "ots");

            migrationBuilder.DropTable(
                name: "uancestries");

            migrationBuilder.DropTable(
                name: "ubloodLines");

            migrationBuilder.DropTable(
                name: "ucategories");

            migrationBuilder.DropTable(
                name: "ufactions");

            migrationBuilder.DropTable(
                name: "ugraphics");

            migrationBuilder.DropTable(
                name: "ugroups");

            migrationBuilder.DropTable(
                name: "ulocs");

            migrationBuilder.DropTable(
                name: "uraces");

            migrationBuilder.DropTable(
                name: "utypes");

            migrationBuilder.DropTable(
                name: "warsa");

            migrationBuilder.DropTable(
                name: "iaccroles");

            migrationBuilder.DropTable(
                name: "iaccounts");

            migrationBuilder.DropTable(
                name: "kms");

            migrationBuilder.DropTable(
                name: "wars");
        }
    }
}
