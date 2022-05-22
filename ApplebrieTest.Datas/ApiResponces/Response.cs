namespace ApplebrieTest.Datas.ApiResponces
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public List<Error>? Errors { get; set; }

        public Response()
        {
            IsSuccess = true;
        }

        public static Response Failure(List<Error> errors)
        {
            var response = new Response()
            {
                IsSuccess = false,
                Errors = new List<Error>()
            };
            response.Errors.AddRange(errors);
            return response;
        }

        public static Response Failure(string error)
        {
            var response = new Response()
            {
                IsSuccess = false,
                Errors = new List<Error>()
            };
            response.Errors.Add(new Error(error));
            return response;
        }

        public static Response Success()
        {
            return new Response();
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }        

        public Response()            
        {           
        }
        public Response(T data)
        {
            IsSuccess = true;
            Errors = null;
            Data = data;
        } 
    }
}
