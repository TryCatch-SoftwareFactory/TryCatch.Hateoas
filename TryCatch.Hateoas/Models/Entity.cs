// <copyright file="Entity.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System.Collections.Generic;

    public abstract class Entity
    {
        public IEnumerable<Link> Links { get; set; }
    }
}
