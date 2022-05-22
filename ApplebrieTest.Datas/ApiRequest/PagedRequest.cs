using ApplebrieTest.Datas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplebrieTest.Datas.ApiRequest
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
