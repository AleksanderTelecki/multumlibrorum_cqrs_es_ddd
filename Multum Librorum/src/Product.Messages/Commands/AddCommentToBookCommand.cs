﻿using CQRS.Core.Commands.Abstract;
using Product.Messages.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Commands
{
    public class AddCommentToBookCommand: ICommand
    {
        public Guid Id { get; set; }
        public Comment Comment { get; set; }
    }
}
