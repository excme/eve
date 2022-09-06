using System.Runtime.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/stars/{star_id}/
    /// </summary>
    public class UniverseStarInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public string name { get; set; }
        public int type_id { get; set; }
        public long age { get; set; }
        public float luminosity { get; set; }
        public long radius { get; set; }
        public ESpectralClass spectral_class { get; set; }
        public int temperature { get; set; }
        public int solar_system_id { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ESpectralClass : byte
        {
            [EnumMember(Value = "K2 V")] K2_V = 1,
            [EnumMember(Value = "K4 V")] K4_V = 2,
            [EnumMember(Value = "G2 V")] G2_V = 3,
            [EnumMember(Value = "G8 V")] G8_V = 4,
            [EnumMember(Value = "M7 V")] M7_V = 5,
            [EnumMember(Value = "K7 V")] K7_V = 6,
            [EnumMember(Value = "M2 V")] M2_V = 7,
            [EnumMember(Value = "K5 V")] K5_V = 8,
            [EnumMember(Value = "M3 V")] M3_V = 9,
            [EnumMember(Value = "G0 V")] G0_V = 10,
            [EnumMember(Value = "G7 V")] G7_V = 11,
            [EnumMember(Value = "G3 V")] G3_V = 12,
            [EnumMember(Value = "F9 V")] F9_V = 13,
            [EnumMember(Value = "G5 V")] G5_V = 14,
            [EnumMember(Value = "F6 V")] F6_V = 15,
            [EnumMember(Value = "K8 V")] K8_V = 16,
            [EnumMember(Value = "K9 V")] K9_V = 17,
            [EnumMember(Value = "K6 V")] K6_V = 18,
            [EnumMember(Value = "G9 V")] G9_V = 19,
            [EnumMember(Value = "G6 V")] G6_V = 20,
            [EnumMember(Value = "G4 VI")] G4_VI = 21,
            [EnumMember(Value = "G4 V")] G4_V = 22,
            [EnumMember(Value = "F8 V")] F8_V = 23,
            [EnumMember(Value = "F2 V")] F2_V = 24,
            [EnumMember(Value = "F1 V")] F1_V = 25,
            [EnumMember(Value = "K3 V")] K3_V = 26,
            [EnumMember(Value = "F0 VI")] F0_VI = 27,
            [EnumMember(Value = "G1 VI")] G1_VI = 28,
            [EnumMember(Value = "G0 VI")] G0_VI = 29,
            [EnumMember(Value = "K1 V")] K1_V = 30,
            [EnumMember(Value = "M4 V")] M4_V = 31,
            [EnumMember(Value = "M1 V")] M1_V = 32,
            [EnumMember(Value = "M6 V")] M6_V = 33,
            [EnumMember(Value = "M0 V")] M0_V = 34,
            [EnumMember(Value = "K2 IV")] K2_IV = 35,
            [EnumMember(Value = "G2 VI")] G2_VI = 36,
            [EnumMember(Value = "K0 V")] K0_V = 37,
            [EnumMember(Value = "K5 IV")] K5_IV = 38,
            [EnumMember(Value = "F5 VI")] F5_VI = 39,
            [EnumMember(Value = "G6 VI")] G6_VI = 40,
            [EnumMember(Value = "F6 VI")] F6_VI = 41,
            [EnumMember(Value = "F2 IV")] F2_IV = 42,
            [EnumMember(Value = "G3 VI")] G3_VI = 43,
            [EnumMember(Value = "M8 V")] M8_V = 44,
            [EnumMember(Value = "F1 VI")] F1_VI = 45,
            [EnumMember(Value = "K1 IV")] K1_IV = 46,
            [EnumMember(Value = "F7 V")] F7_V = 47,
            [EnumMember(Value = "G5 VI")] G5_VI = 48,
            [EnumMember(Value = "M5 V")] M5_V = 49,
            [EnumMember(Value = "G7 VI")] G7_VI = 50,
            [EnumMember(Value = "F5 V")] F5_V = 51,
            [EnumMember(Value = "F4 VI")] F4_VI = 52,
            [EnumMember(Value = "F8 VI")] F8_VI = 53,
            [EnumMember(Value = "K3 IV")] K3_IV = 54,
            [EnumMember(Value = "F4 IV")] F4_IV = 55,
            [EnumMember(Value = "F0 V")] F0_V = 56,
            [EnumMember(Value = "G7 IV")] G7_IV = 57,
            [EnumMember(Value = "G8 VI")] G8_VI = 58,
            [EnumMember(Value = "F2 VI")] F2_VI = 59,
            [EnumMember(Value = "F4 V")] F4_V = 60,
            [EnumMember(Value = "F7 VI")] F7_VI = 61,
            [EnumMember(Value = "F3 V")] F3_V = 62,
            [EnumMember(Value = "G1 V")] G1_V = 63,
            [EnumMember(Value = "G9 VI")] G9_VI = 64,
            [EnumMember(Value = "F3 IV")] F3_IV = 65,
            [EnumMember(Value = "F9 VI")] F9_VI = 66,
            [EnumMember(Value = "M9 V")] M9_V = 67,
            [EnumMember(Value = "K0 IV")] K0_IV = 68,
            [EnumMember(Value = "F1 IV")] F1_IV = 69,
            [EnumMember(Value = "G4 IV")] G4_IV = 70,
            [EnumMember(Value = "F3 VI")] F3_VI = 71,
            [EnumMember(Value = "K4 IV")] K4_IV = 72,
            [EnumMember(Value = "G5 IV")] G5_IV = 73,
            [EnumMember(Value = "G3 IV")] G3_IV = 74,
            [EnumMember(Value = "G1 IV")] G1_IV = 75,
            [EnumMember(Value = "K7 IV")] K7_IV = 76,
            [EnumMember(Value = "G0 IV")] G0_IV = 77,
            [EnumMember(Value = "K6 IV")] K6_IV = 78,
            [EnumMember(Value = "K9 IV")] K9_IV = 79,
            [EnumMember(Value = "G2 IV")] G2_IV = 80,
            [EnumMember(Value = "F9 IV")] F9_IV = 81,
            [EnumMember(Value = "F0 IV")] F0_IV = 82,
            [EnumMember(Value = "K8 IV")] K8_IV = 83,
            [EnumMember(Value = "G8 IV")] G8_IV = 84,
            [EnumMember(Value = "F6 IV")] F6_IV = 85,
            [EnumMember(Value = "F5 IV")] F5_IV = 86,
            A0 = 87,
            A0IV = 88,
            A0IV2 = 89,
        }
    }
}
