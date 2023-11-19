using CQRS.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Messages.Models;

namespace User.Messages.Queries
{
    public class GetUserByEmailQuery: IQuery<UserInfo>
    {
        public string Email { get; set; }
    }
}
