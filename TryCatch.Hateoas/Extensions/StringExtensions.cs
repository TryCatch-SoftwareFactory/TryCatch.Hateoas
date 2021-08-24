// <copyright file="StringExtensions.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("TryCatch.Hateoas.UnitTests")]

namespace System
{
    using System.Collections.Generic;

    internal static class StringExtensions
    {
        internal static string SetQueryParamAsLast(this string query, string queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam))
            {
                throw new ArgumentException($"{nameof(queryParam)} can't be NULL, empty or whitespace.", nameof(queryParam));
            }

            var addKey = query.IndexOf("={" + queryParam + "}") > -1;

            var queryString = query.IndexOf("={" + queryParam + "}") > -1
                ? query.Replace(queryParam + "={" + queryParam + "}", string.Empty, StringComparison.InvariantCulture)
                : query;

            queryString = addKey ? $"{queryString}&{queryParam}=" : queryString;

            return queryString.CleanUri();
        }

        internal static string ReplaceQueryParam(this string baseUri, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"{nameof(key)} can't be NULL, empty or whitespace.", nameof(key));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{nameof(value)} can't be NULL, empty or whitespace.", nameof(value));
            }

            return baseUri
                .Replace("{" + key + "}", $"{value}", StringComparison.InvariantCulture)
                .CleanUri();
        }

        internal static string ReplaceQueryParams(this string baseUri, IDictionary<string, string> queryParams)
        {
            if (queryParams is null)
            {
                throw new ArgumentNullException(nameof(queryParams));
            }

            if (string.IsNullOrWhiteSpace(baseUri))
            {
                return baseUri;
            }

            foreach (var keyValue in queryParams)
            {
                baseUri = baseUri.Replace(@"{" + keyValue.Key + "}", keyValue.Value, StringComparison.InvariantCultureIgnoreCase);
            }

            return baseUri.CleanUri();
        }

        internal static string ReplaceDefaultQueryParams(this string baseUri, IDictionary<string, string> defaultQueryParams)
        {
            if (defaultQueryParams is null)
            {
                throw new ArgumentNullException(nameof(defaultQueryParams));
            }

            if (string.IsNullOrWhiteSpace(baseUri))
            {
                return baseUri;
            }

            foreach (var keyValue in defaultQueryParams)
            {
                if (string.IsNullOrWhiteSpace(keyValue.Value))
                {
                    baseUri = baseUri.Replace(keyValue.Key + "={" + keyValue.Key + "}", string.Empty, StringComparison.InvariantCultureIgnoreCase);
                }
                else
                {
                    baseUri = baseUri.Replace(@"{" + keyValue.Key + "}", keyValue.Value, StringComparison.InvariantCultureIgnoreCase);
                }
            }

            return baseUri.CleanUri();
        }

        internal static string CleanUri(this string baseUri)
        {
            var substrings = new Dictionary<string, string>()
            {
                { "&&", "&" },
                { "?&", "?" },
                { "?=", "?" },
                { "==", "=" },
            };

            foreach (var sub in substrings)
            {
                while (baseUri.IndexOf(sub.Key) > -1)
                {
                    baseUri = baseUri.Replace(sub.Key, sub.Value, StringComparison.InvariantCulture);
                }
            }

            if (baseUri.EndsWith("?"))
            {
                baseUri = baseUri.Length > 1
                    ? baseUri.Substring(0, baseUri.Length - 1)
                    : string.Empty;
            }

            return baseUri;
        }
    }
}
