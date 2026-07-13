namespace Invoice_BlazorWASM.Services.Entities;

public interface IEntity<TId> : ICopy<IEntity<TId>>
{
    TId Id { get; }
}