﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GSMA.MobileConnect.Utils
{
    /// <summary>
    /// Helper methods for dealing with scope and scope values
    /// </summary>
    public static class Scope
    {
        /// <summary>
        /// Returns a scope that is ensured to contain the defaultScope and has any duplication of values removed
        /// </summary>
        /// <param name="scope">Scope to coerce</param>
        /// <param name="defaultScope">Required default scope</param>
        /// <returns>Scope containing default scope values and no duplicated values</returns>
        public static string CoerceOpenIdScope(string scope, string defaultScope = Constants.Scope.OPENID)
        {
            var split = (scope ?? "").Split().ToList();

            var scopeValues = CoerceOpenIdScope(split, defaultScope);

            return CreateScope(scopeValues);
        }

        /// <summary>
        /// Returns a list of scope values that is ensured to contain the defaultScope values and has any duplication of values removed.
        /// This can be used when multiple modifications of scope are required to be chained
        /// </summary>
        /// <param name="scopeValues">Scope to coerce</param>
        /// <param name="defaultScope">Required default scope</param>
        /// <returns>List of scope values containing default scope values and no duplicated values</returns>
        public static List<string> CoerceOpenIdScope(IList<string> scopeValues, string defaultScope = Constants.Scope.OPENID)
        {
            return defaultScope.Split().Union(scopeValues).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
        }

        internal static string CreateScope(IList<string> scopeValues)
        {
            return string.Join(" ", scopeValues).Trim();
        }
    }
}
