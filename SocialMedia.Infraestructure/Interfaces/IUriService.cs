using SocialMedia.Core.QueryFilters;
using System;

namespace SocialMedia.Infraestructure.Interfaces
{
    /*interfas agregada en paginacion para la creacion de url, interfas de UriService*/
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}