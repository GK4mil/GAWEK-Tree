using gawektree_back.Entities;

namespace gawektree_back.Persistence.Interfaces
{
    public interface IRecordRepository: IAsyncRepository<Record, Guid>
    {
        Task<List<Record>> GetAllChildren(Guid id);
        
    }
}
