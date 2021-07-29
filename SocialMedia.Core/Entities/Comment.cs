using System;
using System.Collections.Generic;



namespace SocialMedia.Core.Entities
{
    public partial class Comment : BaseEntity
    {
        //public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Descriptions { get; set; }
        public DateTime Dates { get; set; }
        public bool IsActive { get; set; }

        public virtual Post Post { get; set; }
        public virtual Users User { get; set; }
    }
}
