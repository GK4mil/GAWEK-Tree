using gawektree_back.Entities;
using gawektree_back.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gawektree_back.Persistence.Repositories.EF
{
    public class RecordRepository : BaseRepository<Record, Guid>, IRecordRepository
    {
        

        public RecordRepository(TreeContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Record>> GetAllChildren(Guid id)
        {
            var result = await _dbContext.Records.Where(x => x.parentGuid == id).ToListAsync();

            return result.Where(x => x.guid != Guid.Empty).ToList();
        }
    }
}
