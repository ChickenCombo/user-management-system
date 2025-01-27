using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Helper
{
    public class PaginationHelper
    {
        public static void SetPaginationHeader(
            HttpResponse response,
            int totalCount,
            int pageNumber,
            int pageSize
        )
        {
            var pagination = new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var camelCaseSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            response.Headers["X-Pagination"] = JsonConvert.SerializeObject(pagination, camelCaseSettings);
        }
    }
}