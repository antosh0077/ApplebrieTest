using ApplebrieTest.Common.ApiRequest;

namespace ApplebrieTest.Common.ApiResponces
{
    //public class PaginatedUsersResponce : Response
    //{
    //    public PaginatedList<UserDTO>? Users { get; set; }        
    //}

    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }


        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.IsSuccess = true;
            this.Errors = null;
        }

        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PagedRequest request, int totalRecords)
        {            
            var respose = new PagedResponse<List<T>>(pagedData, request.PageNumber, request.PageSize);
            var totalPages = ((double)totalRecords / (double)request.PageSize);
            respose.TotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
