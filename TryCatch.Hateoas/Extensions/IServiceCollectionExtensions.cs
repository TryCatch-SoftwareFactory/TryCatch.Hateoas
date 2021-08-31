// <copyright file="IServiceCollectionExtensions.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using TryCatch.Hateoas.Services;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Allows setting the common services for Hateoas hypermedia link building.
        /// Add the <see cref="IPagingEngine"/> implementation as Singleton and <see cref="ILinksService"/> implementation as Transient.
        /// </summary>
        /// <param name="service">A <see cref="IServiceCollection"/> reference.</param>
        /// <returns>The <see cref="IServiceCollection"/> reference.</returns>
        public static IServiceCollection AddHateoasServices(this IServiceCollection service) =>
            service
                .AddHttpContextAccessor()
                .AddSingleton<IPagingEngine, PagingEngine>()
                .AddTransient<ILinksService, LinksService>();
    }
}
