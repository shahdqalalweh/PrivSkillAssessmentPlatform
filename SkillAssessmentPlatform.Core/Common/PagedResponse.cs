using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Common
{
    public class PagedResponse<T> 
    {
        public IEnumerable<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedResponse(IEnumerable<T> data, int page, int pageSize, int totalCount)
        {

            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
