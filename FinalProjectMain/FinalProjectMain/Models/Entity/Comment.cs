﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class Comment : BaseEntity
    {

        public string CommentText { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}