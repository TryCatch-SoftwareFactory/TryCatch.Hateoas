// <copyright file="IDictionaryExtensions.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("TryCatch.Hateoas.UnitTests")]

namespace System.Collections.Generic
{
    using System;
    using System.Linq;

    internal static class IDictionaryExtensions
    {
        internal static IDictionary<string, string> MergeWith(this IDictionary<string, string> current, IDictionary<string, string> keysToMerge)
        {
            if (keysToMerge is null)
            {
                throw new ArgumentNullException(nameof(keysToMerge));
            }

            var newCurrent = current.NormalizedKeys(keysToMerge);

            foreach (var item in keysToMerge)
            {
                if (newCurrent.ContainsKey(item.Key))
                {
                    newCurrent[item.Key] = item.Value;
                }
                else
                {
                    newCurrent.Add(item.Key, item.Value);
                }
            }

            return newCurrent;
        }

        internal static IDictionary<string, string> AddRange(this IDictionary<string, string> current, IDictionary<string, string> keysToAdd)
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

        internal static IDictionary<string, string> FilterKeys(this IDictionary<string, string> keys, IEnumerable<string> without)
        {
            if (without is null)
            {
                throw new ArgumentNullException(nameof(without));
            }

            return keys.Where(x => !without.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        internal static string AsQueryString(this IDictionary<string, string> queryParams) => queryParams.Any()
            ? ($"&{string.Join("&", queryParams.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => $"{x.Key}={x.Value}"))}"
                + $"{string.Join("&", queryParams.Where(x => string.IsNullOrWhiteSpace(x.Value)).Select(x => $"{x.Key}="))}")
                .Replace("&&", "&", StringComparison.InvariantCulture)
            : string.Empty;

        internal static IDictionary<string, string> NormalizedKeys(this IDictionary<string, string> keys, IDictionary<string, string> normalizedKeys)
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
    }
}
