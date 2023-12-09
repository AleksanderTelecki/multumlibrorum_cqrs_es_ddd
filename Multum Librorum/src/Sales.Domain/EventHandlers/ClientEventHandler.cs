using Client.Messages.Events;
using CQRS.Core.Commands.Abstract;
using CQRS.Core.Events.Abstract;
using Sales.Domain.Repository;
using Sales.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.EventHandlers
{
    public class ClientEventHandler :
        IEventHandler<ClientRegisteredEvent>
    {

        private readonly ICommandDispatcher _commandDispatcher;

        public ClientEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task Handle(ClientRegisteredEvent @event, CancellationToken cancellation)
        {
            await _commandDispatcher.Dispatch(new CreateCartCommand { ClientId = @event.Id });
        }
    }
}
