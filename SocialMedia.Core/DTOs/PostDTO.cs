﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class PostDTO
    {
        /// <summary>
        /// Autogenerated id for post entity
        /// </summary>
        public int Id { get; set; } 
        public int UserId { get; set; }
        public DateTime Dates { get; set; }
        public string Descriptions { get; set; }
        public string Images { get; set; }
    }
}
