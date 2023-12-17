using CQRS.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Messages.Models;

namespace Client.Messages.Queries
{
    public class GetClientByEmailQuery: Query<ClientInfo>
    {
        public string Email { get; set; }
    }
}
