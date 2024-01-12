using CQRS.Core.Commands.Abstract;

namespace Client.Messages.Commands;

public class ChangeClientBlockadeStatusCommand: ICommand
{
    public Guid ClientId { get; set; }
    public bool BlockClient { get; set; }
}