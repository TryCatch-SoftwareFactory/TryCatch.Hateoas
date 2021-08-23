// <copyright file="FakeEntity.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Mocks
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    public class FakeEntity : IItemModel
    {
        public string Id => "123456";

        public FakeEntity()
        {
            this.Links = new HashSet<Link>();
        }

        public IEnumerable<Link> Links { get; set; }
    }
}
