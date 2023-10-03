using Server.Net.Common.Specifications;

namespace Server.Net.Common.DTOs.Filter
{
    public abstract class CompositeSearchFilterInput
    {
        public List<FilterCriteria> Filters { get; set; } = new List<FilterCriteria>();
        public PaginationCriteria Pagination { get; set; } = new PaginationCriteria();
    }
}
