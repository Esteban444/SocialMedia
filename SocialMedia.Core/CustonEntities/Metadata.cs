using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustonEntities
{
    public class Metadata
    {
        // agregada para la paginacion con Metadata en el objeto del get Post
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public string HasNextPageUrl { get; set; }
        public string HasPreviousPageUrl { get; set; } 
    }

}

