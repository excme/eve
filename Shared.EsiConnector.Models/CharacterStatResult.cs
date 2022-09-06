using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/stats/
    /// </summary>
    public class CharacterStatResult:List<CharacterStatResult.CharacterStatItem>, ISsoResult
    {
        public class Character
        {
            public long days_of_activity { get; set; }
            public long minutes { get; set; }
            public long sessions_started { get; set; }
        }
        public class Combat
        {
            public long criminal_flag_set { get; set; }
            public long damage_from_players_fighter_bomber_amount { get; set; }
            public long damage_from_players_fighter_bomber_num_shots { get; set; }
            public long damage_from_structures_total_amount { get; set; }
            public long damage_from_structures_total_num_shots { get; set; }
            public long damage_to_players_bomb_amount { get; set; }
            public long damage_to_players_bomb_num_shots { get; set; }
            public long damage_to_players_missile_amount { get; set; }
            public long damage_to_players_missile_num_shots { get; set; }
            public long damage_to_players_super_amount { get; set; }
            public long damage_to_players_super_num_shots { get; set; }
            public long damage_to_structures_total_amount { get; set; }
            public long damage_to_structures_total_num_shots { get; set; }
            public long deaths_pod_high_sec { get; set; }
            public long deaths_pod_low_sec { get; set; }
            public long duel_requested { get; set; }
            public long kills_pod_high_sec { get; set; }
            public long kills_pod_low_sec { get; set; }
            public long repair_hull_by_remote_amount { get; set; }
            public long repair_hull_self_amount { get; set; }
            public long cap_drainedby_pc { get; set; }
            public long damage_from_np_cs_amount { get; set; }
            public long damage_from_np_cs_num_shots { get; set; }
            public long damage_from_players_combat_drone_amount { get; set; }
            public long damage_from_players_combat_drone_num_shots { get; set; }
            public long damage_from_players_hybrid_amount { get; set; }
            public long damage_from_players_hybrid_num_shots { get; set; }
            public long damage_from_players_missile_amount { get; set; }
            public long damage_from_players_missile_num_shots { get; set; }
            public long damage_from_players_projectile_amount { get; set; }
            public long damage_from_players_projectile_num_shots { get; set; }
            public long damage_to_players_combat_drone_amount { get; set; }
            public long damage_to_players_combat_drone_num_shots { get; set; }
            public long damage_to_players_hybrid_amount { get; set; }
            public long damage_to_players_hybrid_num_shots { get; set; }
            public long damage_to_players_projectile_amount { get; set; }
            public long damage_to_players_projectile_num_shots { get; set; }
            public long deaths_null_sec { get; set; }
            public long deaths_pod_null_sec { get; set; }
            public long drone_engage { get; set; }
            public long dscans { get; set; }
            public long kills_assists { get; set; }
            public long kills_null_sec { get; set; }
            public long kills_pod_null_sec { get; set; }
            public long npc_flag_set { get; set; }
            public long pvp_flag_set { get; set; }
            public long repair_armor_by_remote_amount { get; set; }
            public long repair_armor_remote_amount { get; set; }
            public long repair_armor_self_amount { get; set; }
            public long repair_hull_remote_amount { get; set; }
            public long repair_shield_by_remote_amount { get; set; }
            public long warp_scramble_pc { get; set; }
            public long warp_scrambledby_npc { get; set; }
            public long warp_scrambledby_pc { get; set; }
            public long weapon_flag_set { get; set; }
            public long webifiedby_npc { get; set; }
            public long webifiedby_pc { get; set; }
            public long webifying_pc { get; set; }
            public long cap_draining_pc { get; set; }
            public long damage_from_players_bomb_amount { get; set; }
            public long damage_from_players_bomb_num_shots { get; set; }
            public long damage_from_players_energy_amount { get; set; }
            public long damage_from_players_energy_num_shots { get; set; }
            public long probe_scans { get; set; }
            public long repair_capacitor_self_amount { get; set; }
            public long repair_shield_remote_amount { get; set; }
            public long cap_drainedby_npc { get; set; }
            public long damage_from_players_smart_bomb_amount { get; set; }
            public long damage_from_players_smart_bomb_num_shots { get; set; }
            public long damage_from_players_super_amount { get; set; }
            public long damage_from_players_super_num_shots { get; set; }
            public long damage_to_players_fighter_bomber_amount { get; set; }
            public long damage_to_players_fighter_bomber_num_shots { get; set; }
            public long damage_to_players_fighter_drone_amount { get; set; }
            public long damage_to_players_fighter_drone_num_shots { get; set; }
            public long deaths_low_sec { get; set; }
            public long deaths_pod_wormhole { get; set; }
            public long deaths_wormhole { get; set; }
            public long engagement_register { get; set; }
            public long kills_high_sec { get; set; }
            public long kills_low_sec { get; set; }
            public long repair_capacitor_by_remote_amount { get; set; }
            public long repair_capacitor_remote_amount { get; set; }
            public long repair_shield_self_amount { get; set; }
            public long damage_from_players_fighter_drone_amount { get; set; }
            public long damage_from_players_fighter_drone_num_shots { get; set; }
            public long damage_to_players_energy_amount { get; set; }
            public long damage_to_players_energy_num_shots { get; set; }
            public long kills_pod_wormhole { get; set; }
            public long kills_wormhole { get; set; }
            public long self_destructs { get; set; }
            public long damage_to_players_smart_bomb_amount { get; set; }
            public long damage_to_players_smart_bomb_num_shots { get; set; }
            public long deaths_high_sec { get; set; }
        }
        public class Industry
        {
            public long jobs_completed_copy_blueprint { get; set; }
            public long jobs_completed_manufacture_asteroid { get; set; }
            public long jobs_completed_manufacture_asteroid_quantity { get; set; }
            public long jobs_completed_manufacture_implant { get; set; }
            public long jobs_completed_manufacture_implant_quantity { get; set; }
            public long jobs_completed_manufacture_subsystem { get; set; }
            public long jobs_completed_manufacture_subsystem_quantity { get; set; }
            public long jobs_started_copy_blueprint { get; set; }
            public long jobs_completed_invention { get; set; }
            public long jobs_completed_manufacture { get; set; }
            public long jobs_completed_manufacture_charge { get; set; }
            public long jobs_completed_manufacture_charge_quantity { get; set; }
            public long jobs_completed_manufacture_commodity { get; set; }
            public long jobs_completed_manufacture_commodity_quantity { get; set; }
            public long jobs_completed_manufacture_deployable { get; set; }
            public long jobs_completed_manufacture_deployable_quantity { get; set; }
            public long jobs_completed_manufacture_drone { get; set; }
            public long jobs_completed_manufacture_drone_quantity { get; set; }
            public long jobs_completed_manufacture_module { get; set; }
            public long jobs_completed_manufacture_module_quantity { get; set; }
            public long jobs_completed_manufacture_other { get; set; }
            public long jobs_completed_manufacture_other_quantity { get; set; }
            public long jobs_completed_manufacture_ship { get; set; }
            public long jobs_completed_manufacture_ship_quantity { get; set; }
            public long jobs_completed_manufacture_structure { get; set; }
            public long jobs_completed_manufacture_structure_quantity { get; set; }
            public long jobs_completed_material_productivity { get; set; }
            public long jobs_completed_time_productivity { get; set; }
            public long jobs_started_invention { get; set; }
            public long jobs_started_manufacture { get; set; }
            public long jobs_started_material_productivity { get; set; }
            public long jobs_started_time_productivity { get; set; }
            public long reprocess_item { get; set; }
            public long reprocess_item_quantity { get; set; }
            public long hacking_successes { get; set; }
            public long jobs_cancelled { get; set; }
        }

        public class Isk
        {
            [JsonPropertyName("in")]
            public long _in { get; set; }
            [JsonPropertyName("out")]
            public long _out { get; set; }
        }

        public class Market
        {
            public long create_contracts_auction { get; set; }
            public long accept_contracts_courier { get; set; }
            public long accept_contracts_item_exchange { get; set; }
            public long buy_orders_placed { get; set; }
            public long create_contracts_item_exchange { get; set; }
            public long deliver_courier_contract { get; set; }
            public long isk_gained { get; set; }
            public long isk_spent { get; set; }
            public long modify_market_order { get; set; }
            public long search_contracts { get; set; }
            public long sell_orders_placed { get; set; }
            public long cancel_market_order { get; set; }
            public long create_contracts_courier { get; set; }
        }

        public class Mining
        {
            public long ore_plagioclase { get; set; }
            public long ore_kernite { get; set; }
            public long ore_arkonor { get; set; }
            public long ore_gneiss { get; set; }
            public long ore_mercoxit { get; set; }
            public long ore_crokite { get; set; }
            public long ore_ice { get; set; }
            public long ore_spodumain { get; set; }
            public long drone_mine { get; set; }
            public long ore_bistot { get; set; }
            public long ore_dark_ochre { get; set; }
            public long ore_hedbergite { get; set; }
            public long ore_hemorphite { get; set; }
            public long ore_omber { get; set; }
            public long ore_veldspar { get; set; }
            public long ore_harvestable_cloud { get; set; }
            public long ore_jaspet { get; set; }
            public long ore_pyroxeres { get; set; }
            public long ore_scordite { get; set; }
        }

        public class Module
        {
            public long activations_frequency_mining_laser { get; set; }
            public long activations_hybrid_weapon { get; set; }
            public long activations_projectile_weapon { get; set; }
            public long link_weapons { get; set; }
            public long overload { get; set; }
            public long repairs { get; set; }
            public long activations_armor_hardener { get; set; }
            public long activations_armor_repair_unit { get; set; }
            public long activations_cloaking_device { get; set; }
            public long activations_cynosural_field { get; set; }
            public long activations_damage_control { get; set; }
            public long activations_drone_control_unit { get; set; }
            public long activations_drone_tracking_modules { get; set; }
            public long activations_ecm_burst { get; set; }
            public long activations_micro_jump_drive { get; set; }
            public long activations_probe_launcher { get; set; }
            public long activations_propulsion_module { get; set; }
            public long activations_remote_armor_repairer { get; set; }
            public long activations_salvager { get; set; }
            public long activations_sensor_booster { get; set; }
            public long activations_stasis_web { get; set; }
            public long activations_strip_miner { get; set; }
            public long activations_tracking_computer { get; set; }
            public long activations_triage { get; set; }
            public long activations_warp_scrambler { get; set; }
            public long activations_armor_resistance_shift_hardener { get; set; }
            public long activations_automated_targeting_system { get; set; }
            public long activations_bastion { get; set; }
            public long activations_bomb_launcher { get; set; }
            public long activations_capacitor_booster { get; set; }
            public long activations_energy_destabilizer { get; set; }
            public long activations_fueled_armor_repairer { get; set; }
            public long activations_remote_capacitor_transmitter { get; set; }
            public long activations_remote_sensor_booster { get; set; }
            public long activations_remote_sensor_damper { get; set; }
            public long activations_remote_shield_booster { get; set; }
            public long activations_shield_booster { get; set; }
            public long activations_shield_hardener { get; set; }
            public long activations_smart_bomb { get; set; }
            public long activations_survey_scanner { get; set; }
            public long activations_cargo_scanner { get; set; }
            public long activations_data_miners { get; set; }
            public long activations_eccm { get; set; }
            public long activations_energy_weapon { get; set; }
            public long activations_fueled_shield_booster { get; set; }
            public long activations_tractor_beam { get; set; }
            public long activations_gas_cloud_harvester { get; set; }
            public long activations_clone_vat_bay { get; set; }
            public long activations_ecm { get; set; }
            public long activations_energy_vampire { get; set; }
            public long activations_festival_launcher { get; set; }
            public long activations_gang_coordinator { get; set; }
            public long activations_hull_repair_unit { get; set; }
            public long activations_industrial_core { get; set; }
            public long activations_interdiction_sphere_launcher { get; set; }
            public long activations_mining_laser { get; set; }
            public long activations_missile_launcher { get; set; }
            public long activations_passive_targeting_system { get; set; }
            public long activations_projected_eccm { get; set; }
            public long activations_remote_ecm_burst { get; set; }
            public long activations_remote_hull_repairer { get; set; }
            public long activations_remote_tracking_computer { get; set; }
            public long activations_ship_scanner { get; set; }
            public long activations_siege { get; set; }
            public long activations_super_weapon { get; set; }
            public long activations_target_breaker { get; set; }
            public long activations_target_painter { get; set; }
            public long activations_tracking_disruptor { get; set; }
            public long activations_warp_disrupt_field_generator { get; set; }
        }

        public class Pve
        {
            public long dungeons_completed_distribution { get; set; }
            public long dungeons_completed_agent { get; set; }
            public long missions_succeeded { get; set; }
            public long missions_succeeded_epic_arc { get; set; }
        }

        public class Social
        {
            public long add_contact_high { get; set; }
            public long add_contact_horrible { get; set; }
            public long add_contact_neutral { get; set; }
            public long add_note { get; set; }
            public long added_as_contact_good { get; set; }
            public long added_as_contact_high { get; set; }
            public long added_as_contact_neutral { get; set; }
            public long chat_messages_alliance { get; set; }
            public long chat_messages_corporation { get; set; }
            public long chat_messages_fleet { get; set; }
            public long chat_messages_solarsystem { get; set; }
            public long chat_total_message_length { get; set; }
            public long direct_trades { get; set; }
            public long fleet_broadcasts { get; set; }
            public long fleet_joins { get; set; }
            public long added_as_contact_bad { get; set; }
            public long added_as_contact_horrible { get; set; }
            public long mails_received { get; set; }
            public long mails_sent { get; set; }
            public long add_contact_good { get; set; }
            public long add_contact_bad { get; set; }
            public long calendar_event_created { get; set; }
            public long chat_messages_constellation { get; set; }
            public long chat_messages_region { get; set; }
            public long chat_messages_warfaction { get; set; }
        }

        public class Travel
        {
            public long align_to { get; set; }
            public long distance_warped_high_sec { get; set; }
            public long distance_warped_low_sec { get; set; }
            public long distance_warped_null_sec { get; set; }
            public long distance_warped_wormhole { get; set; }
            public long docks_high_sec { get; set; }
            public long docks_null_sec { get; set; }
            public long jumps_stargate_high_sec { get; set; }
            public long jumps_stargate_low_sec { get; set; }
            public long jumps_stargate_null_sec { get; set; }
            public long jumps_wormhole { get; set; }
            public long warps_high_sec { get; set; }
            public long warps_low_sec { get; set; }
            public long warps_null_sec { get; set; }
            public long warps_to_bookmark { get; set; }
            public long warps_to_celestial { get; set; }
            public long warps_to_fleet_member { get; set; }
            public long warps_wormhole { get; set; }
            public long warps_to_scan_result { get; set; }
            public long acceleration_gate_activations { get; set; }
            public long docks_low_sec { get; set; }
        }

        public class Inventory
        {
            public long abandon_loot_quantity { get; set; }
            public long trash_item_quantity { get; set; }
        }

        public class Orbital
        {
            public long strike_characters_killed { get; set; }
            public long strike_damage_to_players_armor_amount { get; set; }
            public long strike_damage_to_players_shield_amount { get; set; }
        }

        public class CharacterStatItem
        {
            public int year { get; set; }
            public Character character { get; set; }
            public Combat combat { get; set; }
            public Industry industry { get; set; }
            public Isk isk { get; set; }
            public Market market { get; set; }
            public Mining mining { get; set; }
            public Module module { get; set; }
            public Pve pve { get; set; }
            public Social social { get; set; }
            public Travel travel { get; set; }
            public Inventory inventory { get; set; }
            public Orbital orbital { get; set;}
        }
    }
}
