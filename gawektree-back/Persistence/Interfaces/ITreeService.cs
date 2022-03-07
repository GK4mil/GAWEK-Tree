using gawektree_back.Entities;

namespace gawektree_back.Persistence.Interfaces
{
    public interface ITreeService
    {
        public Task<bool> CheckIfNameExistsAmongSiblings(Guid parentGuid, string name);
        public Task<bool> RecordExists(Guid id);

        public Task DeleteAllChildRecursively(Guid id);
        public bool checkIfIsRoot(Record record);
        public Task<bool> CheckIfNextParentIsChild(Guid nextParent, Guid id);
    }
}
