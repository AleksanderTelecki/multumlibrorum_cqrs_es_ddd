﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Core.Messages;

namespace CQRS.Core.Queries.Abstract
{
    public interface IQuery<TResult>: ICqrsMessage
    {
        
    }
}
