namespace TryCatch.Hateoas.UnitTests.Services
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.UnitTests.Mocks;

    public class Given
    {
        public static FakeEntity ValidEntity() => new FakeEntity();

        public static IEnumerable<Link> ExpectedLinksForEntity() => new HashSet<Link>()
            {
                new Link()
                {
                    Action = "GET",
                    Href = "https://localhost:5001/api/fakes/123456",
                    Rel = "self_read"
                },
                new Link()
                {
                    Action = "PUT",
                    Href = "https://localhost:5001/api/fakes/123456",
                    Rel = "self_update"
                },
                new Link()
                {
                    Action = "DELETE",
                    Href = "https://localhost:5001/api/fakes/123456",
                    Rel = "self_delete"
                }
            };

        public static IEnumerable<Link> ExpectedLinksForSummary() => new HashSet<Link>()
            {
                new Link()
                {
                    Action = "GET",
                    Href = "https://localhost:5001/api/fakes/summary/123456",
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
                    Href = $"https://localhost:5001/api/fakes?offset=1&orderBy=Date&sortAs=ASC&limit=",
                    Rel = "list_limit"
                },
                new Link()
                {
                    Action = "GET",
                    Href = $"https://localhost:5001/api/fakes?offset=1&limit={limit}&orderBy=Date&sortAs=ASC&search=",
                    Rel = "list_search"
                },
                new Link()
                {
                    Action = "GET",
                    Href = $"https://localhost:5001/api/fakes?offset={offset}&limit={limit}&sortAs=ASC&orderBy=",
                    Rel = "list_order_by"
                },
                new Link()
                {
                    Action = "GET",
                    Href = $"https://localhost:5001/api/fakes?offset={offset}&limit={limit}&orderBy=Date&sortAs=",
                    Rel = "list_sort_as"
                },
                new Link()
                {
                    Action = "GET",
                    Href = $"https://localhost:5001/api/fakes?offset=1&limit={limit}&orderBy=Date&sortAs=ASC",
                    Rel = "list_first"
                },                
            };

            for (var i = 0; i < offsets.Length; i++)
            {
                list.Add(new Link()
                {
                    Action = "GET",
                    Href = $"https://localhost:5001/api/fakes?offset={offsets[i]}&limit={limit}&orderBy=Date&sortAs=ASC",
                    Rel = $"list_page_{(offsets[i] / limit) + 1}"
                });
            }

            list.Add(new Link()
            {
                Action = "GET",
                Href = $"https://localhost:5001/api/fakes?offset={lastOffset}&limit={limit}&orderBy=Date&sortAs=ASC",
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
                    Href = $"https://localhost:5001/api/fakes?offset={offset1}&limit={limit}&orderBy=Date&sortAs=ASC",
                    Rel = "list_next"
                });
            }

            if (offset2 > 0)
            {
                list.Add(
                    new Link()
                    {
                        Action = "GET",
                        Href = $"https://localhost:5001/api/fakes?offset={offset2}&limit={limit}&orderBy=Date&sortAs=ASC",
                        Rel = "list_prev"
                    }
                );
            }

            return list;
        }
    }
}
