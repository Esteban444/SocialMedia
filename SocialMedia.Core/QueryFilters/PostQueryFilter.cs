using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.QueryFilters
{
    public class PostQueryFilter  
    {
        public int? UserId { get; set; }
        public DateTime? Dates { get; set; }
        public string Description { get; set; }
        // para Paginacion las sigientes dos propiedades
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 
    }
}
