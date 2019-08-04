using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class answerComment : BaseEntity
    {
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public int? ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public string answeComment { get; set; }
    }
}