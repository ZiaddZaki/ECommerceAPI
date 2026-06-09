using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Common
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        [Range(1, int.MaxValue, ErrorMessage ="Page number must be grater than 0")]
        public int PageNumber { get; set; }

        [Range(1, MaxPageSize, ErrorMessage = "Page Size must be grater than 0")]
        public int PageSize {
            get => _pageSize;
            set=> _pageSize = value > MaxPageSize ? MaxPageSize : value; 
        }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }

    }
}
