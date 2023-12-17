namespace CQRS.Core.Commands.Abstract;

public class Command: ICommand
{
    public string CommandType => this.GetType().AssemblyQualifiedName;
}