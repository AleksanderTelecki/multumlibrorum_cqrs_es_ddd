namespace CQRS.Core.Messages;

public interface ICqrsMessage
{
    public string Type => this.GetType().AssemblyQualifiedName;
}