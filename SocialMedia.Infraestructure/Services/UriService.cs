using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
using System;


namespace SocialMedia.Infraestructure.Services
{
    /*Agregada para el manejo de url en la paginacion*/

    /*servicio encargado de generar urls*/
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
