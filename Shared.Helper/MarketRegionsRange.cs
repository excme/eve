using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Helper
{
    /// <summary>
    /// Регионы, в которых можно торговать
    /// </summary>
    public static class MarketRegionsRange
    {
        public static bool Exist(long region_id)
        {
            return marketRegions.Contains(region_id);
        }
        static List<long> marketRegions { get; set; }
        static MarketRegionsRange()
        {
            marketRegions = new List<long>() { 10000001, 10000002, 10000003, 10000005, 10000006, 10000007, 10000008, 10000009, 10000010, 10000011, 10000012, 10000013, 10000014, 10000015, 10000016, 10000018, 10000020, 10000021, 10000022, 10000023, 10000025, 10000027, 10000028, 10000029, 10000030, 10000031, 10000032, 10000033, 10000034, 10000035, 10000036, 10000037, 10000038, 10000039, 10000040, 10000041, 10000042, 10000043, 10000044, 10000045, 10000046, 10000047, 10000048, 10000049, 10000050, 10000051, 10000052, 10000053, 10000054, 10000055, 10000056, 10000057, 10000058, 10000059, 10000060, 10000061, 10000062, 10000063, 10000064, 10000065, 10000066, 10000067, 10000068, 10000069 };
        }
        public static List<long>  GetList()
        {
            return marketRegions;
        }
    }
}
