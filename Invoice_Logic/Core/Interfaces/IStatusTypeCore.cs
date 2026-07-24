using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Core.Interfaces;

public interface IStatusTypeCore
{
    Task<List<StatusTypeEntity>> Get();
}
