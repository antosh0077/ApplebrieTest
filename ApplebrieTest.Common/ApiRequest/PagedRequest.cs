namespace ApplebrieTest.Common.ApiRequest
{
    public class PagedRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PagedRequest()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
