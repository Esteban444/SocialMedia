using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SocialMedia.Core.Entities
{
    public partial class Post: BaseEntity
    {
        //public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Dates { get; set; }
        public string Descriptions { get; set; }
        public string Images { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
