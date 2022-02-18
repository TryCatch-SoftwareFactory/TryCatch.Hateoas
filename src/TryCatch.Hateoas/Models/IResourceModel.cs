// <copyright file="IResourceModel.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for resource model.
    /// </summary>
    public interface IResourceModel
    {
        /// <summary>
        /// Gets the hateoas link collection.
        /// </summary>
        IEnumerable<Link> Links { get; }
    }
}
