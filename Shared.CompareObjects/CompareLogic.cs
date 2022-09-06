using System;
using System.Collections.Generic;
using System.IO;

namespace eveDirect.Shared.CompareObjects
{
    /// <summary>
    /// Class that allows comparison of two objects of the same type to each other.  Supports classes, lists, arrays, dictionaries, child comparison and more.
    /// </summary>
    /// <example>
    /// CompareLogic compareLogic = new CompareLogic();
    /// 
    /// Person person1 = new Person();
    /// person1.DateCreated = DateTime.Now;
    /// person1.Name = "Greg";
    ///
    /// Person person2 = new Person();
    /// person2.Name = "John";
    /// person2.DateCreated = person1.DateCreated;
    ///
    /// ComparisonResult result = compareLogic.Compare(person1, person2);
    /// 
    /// if (!result.AreEqual)
    ///    Console.WriteLine(result.DifferencesString);
    /// 
    /// </example>
    public class CompareLogic : ICompareLogic
    {
        #region Class Variables
        private ComparisonConfig _config;
        #endregion

        #region Properties

        /// <summary>
        /// The default configuration
        /// </summary>
        public ComparisonConfig Config
        {
            get { return _config; }
            set
            {
                _config = value;
                VerifyConfig verifyConfig = new VerifyConfig();
                verifyConfig.Verify(value);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Set up defaults for the comparison
        /// </summary>
        public CompareLogic()
        {
            Config = new ComparisonConfig();
        }

        /// <summary>
        /// Pass in the configuration
        /// </summary>
        /// <param name="config"></param>
        public CompareLogic(ComparisonConfig config)
        {
            Config = config;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Compare two objects of the same type to each other.
        /// </summary>
        /// <remarks>
        /// Check the Differences or DifferencesString Properties for the differences.
        /// Default MaxDifferences is 1 for performance
        /// </remarks>
        /// <param name="expectedObject">The expected object value to compare</param>
        /// <param name="actualObject">The actual object value to compare</param>
        /// <returns>True if they are equal</returns>
        public ComparisonResult Compare(object expectedObject, object actualObject)
        {
            ComparisonResult result = new ComparisonResult(Config);

            result.Watch.Start();

            RootComparer rootComparer = RootComparerFactory.GetRootComparer();

            CompareParms parms = new CompareParms
            {
                Config = Config,
                Result = result,
                Object1 = expectedObject,
                Object2 = actualObject,
                BreadCrumb = string.Empty
            };

            rootComparer.Compare(parms);

            if (Config.AutoClearCache)
                ClearCache();

            result.Watch.Stop();

            return result;
        }

        /// <summary>
        /// Reflection properties and fields are cached. By default this cache is cleared automatically after each compare.
        /// </summary>
        public void ClearCache()
        {
            Cache.ClearCache();
        }

        #endregion

    }
}
