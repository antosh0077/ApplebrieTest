using ApplebrieTest.Datas.Models;

namespace ApplebrieTest.Common.ApiRequest
{
    public class FilterRequest : PagedRequest
    {
        public UserType FilterByUserType { get; set; }

        public FilterRequest()
        {
            PageNumber = 1;
            PageSize = 10;
            FilterByUserType = UserType.Undefined;
        }
    }
}
