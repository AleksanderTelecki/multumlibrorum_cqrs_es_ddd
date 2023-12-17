namespace CQRS.Core.Queries.Abstract;

public class Query<TResult>: IQuery<TResult>
{
    public string QueryType => this.GetType().AssemblyQualifiedName;
}