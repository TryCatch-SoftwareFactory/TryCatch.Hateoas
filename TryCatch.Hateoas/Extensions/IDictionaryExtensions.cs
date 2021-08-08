// <copyright file="IDictionaryExtensions.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace System.Collections.Generic
{
    using System;
    using System.Linq;

    public static class IDictionaryExtensions
    {
        public static IDictionary<string, string> AddRange(this IDictionary<string, string> current, IDictionary<string, string> keysToAdd)
        {
            if (keysToAdd is null)
            {
                throw new ArgumentNullException(nameof(keysToAdd));
            }

            foreach (var keyValue in keysToAdd)
            {
                current.Add(keyValue.Key, keyValue.Value);
            }

            return current;
        }

        public static IDictionary<string, string> NormalizedKeys(this IDictionary<string, string> keys, IDictionary<string, string> normalizedKeys)
        {
            if (normalizedKeys is null)
            {
                throw new ArgumentNullException(nameof(normalizedKeys));
            }

            var newDictionary = new Dictionary<string, string>();

            foreach (var keyValue in keys)
            {
                var key = normalizedKeys.Keys.FirstOrDefault(x => x.ToUpperInvariant() == keyValue.Key.ToUpperInvariant());

                if (key == default)
                {
                    newDictionary.Add(keyValue.Key, keyValue.Value.ToString());

                    continue;
                }

                newDictionary.Add(key, keyValue.Value.ToString());
            }

            return newDictionary;
        }

        public static IDictionary<string, string> FilterKeys(this IDictionary<string, string> keys, IEnumerable<string> without)
        {
            if (without is null)
            {
                throw new ArgumentNullException(nameof(without));
            }

            return keys.Where(x => !without.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        public static string AsQueryString(this IDictionary<string, string> queryParams) => queryParams.Any()
            ? $"&{string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"))}".Replace("&&", "&", StringComparison.InvariantCulture)
            : string.Empty;
    }
}
