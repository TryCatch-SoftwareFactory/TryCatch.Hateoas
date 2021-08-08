// <copyright file="Link.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    public class Link
    {
        public string Href { get; set; }

        public string Rel { get; set; }

        public string Action { get; set; }
    }
}
