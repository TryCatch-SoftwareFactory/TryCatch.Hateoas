// <copyright file="IItemModel.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for entity or dto model.
    /// </summary>
    public interface IItemModel
    {
        /// <summary>
        /// Gets the hateoas link collection.
        /// </summary>
        IEnumerable<Link> Links { get; }
    }
}
