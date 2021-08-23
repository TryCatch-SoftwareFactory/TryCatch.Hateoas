namespace TryCatch.Hateoas.UnitTests.Services
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.UnitTests.Mocks;

    public class Given
    {
        public static IEnumerable<object[]> GetPagesInput()
        {
            /// long offset, long limit, long total, int maxPages, IEnumerable<PageInfo> pages
            yield return new object[] { 1, 10, 100, 5, new HashSet<PageInfo> {
                new PageInfo(1, 1),
                new PageInfo(11, 2),
                new PageInfo(21, 3),
                new PageInfo(31, 4),
                new PageInfo(41, 5),
            } };

            yield return new object[] { 10, 10, 100, 5, new HashSet<PageInfo> {
                new PageInfo(10, 2),
                new PageInfo(20, 3),
                new PageInfo(30, 4),
                new PageInfo(40, 5),
                new PageInfo(50, 6),
            } };

            yield return new object[] { 61, 10, 100, 5, new HashSet<PageInfo> {
                new PageInfo(61, 7),
                new PageInfo(71, 8),
                new PageInfo(81, 9),
                new PageInfo(91, 10),
            } };
        }

        public static IDictionary<string, string> DefaultQueryParams() =>
            new Dictionary<string, string>()
            {
                { "search", string.Empty },
                { "orderBy", "Id" },
                { "sortAs", "ASC" },
                { "offset", "1" },
                { "limit", "10"  },
            };

        public static IEnumerable<LinkInfo> EntityTemplates() =>
            new HashSet<LinkInfo>()
            {
                new LinkInfo("self_read", "GET"),
                new LinkInfo("self_update", "PUT"),
                new LinkInfo("self_delete", "DELETE"),
            };

        public static IEnumerable<LinkInfo> ListTemplates() =>
            new HashSet<LinkInfo>()
            {
                new LinkInfo("list_limit", "GET", new Dictionary<string, string> { { "limit", string.Empty }, }),
                new LinkInfo("list_search", "GET", new Dictionary<string, string> { { "search", string.Empty }, }),
                new LinkInfo("list_order_by", "GET", new Dictionary<string, string> { { "orderBy", string.Empty }, }),
                new LinkInfo("list_sort_as", "GET", new Dictionary<string, string> { { "sortAs", string.Empty }, }),
            };

        public static IDictionary<string, string> CurrentQueryParams =>
            new Dictionary<string, string>()
            {
                { "offset", "1200" },
                { "limit", "50" },
                { "orderBy", "Date" },
                { "sortAs", "ASC" },
            };


        public static FakeEntity ValidEntity() => new FakeEntity();

        public static IEnumerable<Link> ExpectedLinksForEntity() => new HashSet<Link>()
            {
                new Link()
                {
                    Action = "GET",
                    //Href = "https://localhost:5001/api/fakes/123456",
                    Rel = "self_read"
                },
                new Link()
                {
                    Action = "PUT",
                    //Href = "https://localhost:5001/api/fakes/123456",
                    Rel = "self_update"
                },
                new Link()
                {
                    Action = "DELETE",
                    //Href = "https://localhost:5001/api/fakes/123456",
                    Rel = "self_delete"
                }
            };

        public static IEnumerable<Link> ExpectedLinksForSummary() => new HashSet<Link>()
            {
                new Link()
                {
                    Action = "GET",
                    //Href = "https://localhost:5001/api/fakes/summary/123456",
                    Rel = "self_read"
                },
            };

        public static IEnumerable<Link> ExpectedLinksForPageResult(int offset, int limit, int lastOffset, int[] offsets)
        {
            var list = new HashSet<Link>()
            {
                new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset=1&orderBy=Date&sortAs=ASC&limit=",
                    Rel = "list_limit"
                },
                new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset=1&limit={limit}&orderBy=Date&sortAs=ASC&search=",
                    Rel = "list_search"
                },
                new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset={offset}&limit={limit}&sortAs=ASC&orderBy=",
                    Rel = "list_order_by"
                },
                new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset={offset}&limit={limit}&orderBy=Date&sortAs=",
                    Rel = "list_sort_as"
                },
                new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset=1&limit={limit}&orderBy=Date&sortAs=ASC",
                    Rel = "list_first"
                },                
            };

            for (var i = 0; i < offsets.Length; i++)
            {
                list.Add(new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset={offsets[i]}&limit={limit}&orderBy=Date&sortAs=ASC",
                    Rel = $"list_page_{(offsets[i] / limit) + 1}"
                });
            }

            list.Add(new Link()
            {
                Action = "GET",
                //Href = $"https://localhost:5001/api/fakes?offset={lastOffset}&limit={limit}&orderBy=Date&sortAs=ASC",
                Rel = "list_last"
            });

            return list;
        }

        public static IEnumerable<Link> ExpectedLinksForListResult(int offset1, int offset2, int limit)
        {
            var list = new HashSet<Link>();

            if (offset1 > 0)
            {
                list.Add(new Link()
                {
                    Action = "GET",
                    //Href = $"https://localhost:5001/api/fakes?offset={offset1}&limit={limit}&orderBy=Date&sortAs=ASC",
                    Rel = "list_next"
                });
            }

            if (offset2 > 0)
            {
                list.Add(
                    new Link()
                    {
                        Action = "GET",
                        //Href = $"https://localhost:5001/api/fakes?offset={offset2}&limit={limit}&orderBy=Date&sortAs=ASC",
                        Rel = "list_prev"
                    }
                );
            }

            return list;
        }
    }
}
