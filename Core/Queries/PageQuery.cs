using System.ComponentModel;

namespace Core.Queries
{
    public class PageQuery
    {  
        [DefaultValue(1)]
        public int PageNumber { get; set; }

        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}