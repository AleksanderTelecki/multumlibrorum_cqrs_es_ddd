﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public DateTime RegDate { get; set; }
    }
}
