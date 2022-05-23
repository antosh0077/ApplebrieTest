using ApplebrieTest.Datas.Models;

namespace ApplebrieTest.Common.ApiRequest
{
    public class PagedRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public UserType FilterByUserType { get; set; }

        public PagedRequest()
        {
            PageNumber = 1;
            PageSize = 10;
            FilterByUserType = UserType.Undefined;
        }
    }
}
