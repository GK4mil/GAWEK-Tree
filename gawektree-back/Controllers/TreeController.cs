
using Microsoft.AspNetCore.Mvc;
using gawektree_back.Entities;
using gawektree_back.Persistence.Interfaces;

namespace gawektree_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly IRecordRepository rr;
        private readonly ITreeService ts;

        public TreeController(IRecordRepository rr, ITreeService ts)
        {
            this.rr = rr;
            this.ts = ts;
        }

        /// <summary>
        /// Return full record for provided id.
        /// </summary>

        // GET: api/Tree/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(Guid id)
        {
            var record = await rr.GetByIdAsync(id);

            if (record == null)
            {
                return NotFound(new {message="No record with provided id"});
            }

            return @record;
        }
        /// <summary>
        /// Return root children
        /// </summary>
        [HttpGet("Root")]
        public async Task<ActionResult<List<Record>>> GetRootChildren()
        {
            return await GetChildren(Guid.Empty);
        }
        /// <summary>
        /// Return children for provided parent id
        /// </summary>
        [HttpGet("child/{id}")]
        public async Task<ActionResult<List<Record>>> GetChildren(Guid id)
        {
            var records = await rr.GetAllChildren(id);

            if (records.Count()==0)
            {
                return NotFound(new {message="Not found children"});
            }

            return records;
        }
        /// <summary>
        /// Return count of children for provided parent id
        /// </summary>
        [HttpGet("child/count/{id}")]
        public async Task<ActionResult<List<Record>>> GetCountOfChildren(Guid id)
        {
            var records = await rr.GetAllChildren(id);
            return Ok(new { count = records.Count });
        }
        /// <summary>
        /// Add new child
        /// </summary>
        [HttpPost("child")]
        public async Task<ActionResult<Record>> PostRecord(RecordDTO record)
        {


            if (record.name.Length == 0)
                return BadRequest(new { message = "No name provided!" });
            

            if ((await ts.RecordExists(record.parentGuid)))
            {
                if (!await ts.CheckIfNameExistsAmongSiblings(record.parentGuid, record.name))
                {
                    var toDBrecord = new Record() { guid = Guid.NewGuid(), name = record.name, parentGuid = record.parentGuid };
                    await rr.AddAsync(toDBrecord);
                    return CreatedAtAction("GetRecord", new { id = toDBrecord.guid }, toDBrecord);
                }
                else
                {
                    return BadRequest(new { message = "Name duplication among siblings" });
                }
            }
            else
                return BadRequest(new { message = "Parent does not exist" });

                
        }
        /// <summary>
        /// Update node
        /// </summary>
        [HttpPatch("child/{id}")]
        public async Task<ActionResult> ChangeDataInRecord(Guid id, RecordDTO record)
        {
            var fromDatabase = await rr.GetByIdAsync(id);

            if (id == record.parentGuid)
                return BadRequest(new { message = "Parent and child can not have the same ID" });
            else if (record.name.Length == 0)
                return BadRequest(new { message = "Empty name field" });
            else if(ts.checkIfIsRoot(fromDatabase))
                return BadRequest(new { message = "Cannot change root" });

            else if (fromDatabase != null)
            {
                if (fromDatabase.parentGuid != record.parentGuid)
                {
                    if (!await ts.CheckIfNameExistsAmongSiblings(record.parentGuid, record.name))
                    {
                        fromDatabase.parentGuid = record.parentGuid;
                        fromDatabase.name = record.name.Trim();
                        await rr.UpdateAsync(fromDatabase);
                        return CreatedAtAction("GetRecord", new { id = fromDatabase.guid }, fromDatabase);
                    }
                    else
                    {
                        return BadRequest(new { message = "Name duplication among siblings" });
                    }
                }
                else
                {
                    if (!await ts.CheckIfNameExistsAmongSiblings(record.parentGuid, record.name))
                    {
                        fromDatabase.name = record.name.Trim();
                        await rr.UpdateAsync(fromDatabase);
                        return CreatedAtAction("GetRecord", new { id = fromDatabase.guid }, fromDatabase);
                    }
                    else
                        return BadRequest(new { message = "Name duplication among siblings" });

                }
            }
            else
                return BadRequest(new { message = "No record with provided id" });
            

        }
        /// <summary>
        /// Delete node with all children
        /// </summary>
        // DELETE: api/Tree/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(Guid id)
        {
            
            var record = await rr.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound(new {message="Not found node"});
            }
            else
            {
                if (ts.checkIfIsRoot(record))
                    return BadRequest(new { message = "Cannot delete root!" });
            }

            await ts.DeleteAllChildRecursively(id);

            await rr.DeleteAsync(record);

            return NoContent();
        }


     
    }
    
    
    public record RecordDTO(string name, Guid parentGuid);
}
