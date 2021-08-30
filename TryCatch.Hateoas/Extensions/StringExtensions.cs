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
        internal static string CleanUri(this string baseUri)
        {
            var substrings = new Dictionary<string, string>()
            {
                { "&&", "&" },
                { "?&", "?" },
                { "?=", "?" },
                { "==", "=" },
                { "//", "/" },
                { "??", "?" },
                { "/?", "?" },
            };

            var root = string.Empty;
            var path = baseUri;
            var uri = new Uri(baseUri, UriKind.RelativeOrAbsolute);

            if (Uri.IsWellFormedUriString(baseUri, UriKind.Absolute))
            {
                var port = uri.IsDefaultPort ? string.Empty : $":{uri.Port}";
                path = $"{uri.AbsolutePath}?{uri.Query}";
                root = $"{uri.Scheme}://{uri.Host}{port}";
            }

            foreach (var sub in substrings)
            {
                while (path.IndexOf(sub.Key) > -1)
                {
                    path = path.Replace(sub.Key, sub.Value, StringComparison.InvariantCulture);
                }
            }

            if (path.EndsWith("?"))
            {
                path = path.Length > 1 ? path.Substring(0, path.Length - 1) : string.Empty;
            }

            return $"{root}{path}";
        }
    }
}
