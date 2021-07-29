using SocialMedia.Core.CustonEntities;


namespace SocialMedia.Api.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data {get; set;}
        public Metadata Meta { get; set; } // agregada para la paginacion con el objeto metadata
    }
}
