using gawektree_back.Entities;
using gawektree_back.Persistence.Interfaces;
using gawektree_back.Persistence.Repositories.EF;

namespace gawektree_back.Services
{
    public class TreeService:ITreeService
    {
        private readonly IRecordRepository rr;

        public TreeService(IServiceProvider serviceProvider)
        {
            this.rr = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRecordRepository>();
        }


        public async Task<bool> CheckIfNameExistsAmongSiblings(Guid parentGuid, string name)
        {
            List<Record> list = await rr.GetAllChildren(parentGuid);
            if (list != null)
            {
                foreach (Record record in list)
                {
                    if (record.name.ToLowerInvariant().Trim() == name.ToLowerInvariant().Trim())
                        return true;
                }
                return false;
            }
            return false;
        }

        public  async Task<bool> RecordExists(Guid id)
        {
            return (await rr.GetByIdAsync(id)) != null;
        }

        public async Task DeleteAllChildRecursively(Guid id)
        {

            var listOfChildren = await rr.GetAllChildren(id);

            foreach (var child in listOfChildren)
            {
                List<Record> sublist = await rr.GetAllChildren(child.guid);
                if (sublist != null && sublist?.Count() > 0)
                    await DeleteAllChildRecursively(child.guid);

                await rr.DeleteAsync(child);
            }

        }
        public bool checkIfIsRoot(Record record)
        {
            return (record.parentGuid == Guid.Empty && record.guid == Guid.Empty);
        }
    }
}
