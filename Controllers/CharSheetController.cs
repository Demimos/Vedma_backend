using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vedma_backend.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vedma_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharSheetController : ControllerBase
    {
        ApplicationContext Db;

        public CharSheetController(ApplicationContext context)
        {
            Db = context;
        }

        // GET: api/<CharSheetController>
        [HttpGet]
        public async Task<IEnumerable<CharSheet>> Get()
        {
            return await Db.CharSheets.Include(c => c.Properties).AsNoTracking().ToListAsync();
        }

        // GET api/<CharSheetController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharSheet>> Get(int id)
        {
            var sheet = await Db.CharSheets.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (sheet == null) return new NotFoundResult();
            return new JsonResult(sheet);
        }

        // POST api/<CharSheetController>
        [HttpPost]
        public async Task Post([FromBody] CharSheet charSheet)
        {
            Db.CharSheets.Add(charSheet);
            await Db.SaveChangesAsync();
        }

        // PUT api/<CharSheetController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CharSheet charSheet)
        {
            var old = await Db.CharSheets.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (old == null) new NotFoundResult();
            charSheet.Id = id;
            Db.CharSheets.Update(charSheet);
            await Db.SaveChangesAsync();
            return new OkResult();
        }

        // DELETE api/<CharSheetController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var charsheet = await Db.CharSheets.FirstOrDefaultAsync(c => c.Id == id);
            if (charsheet == null) new NotFoundResult();
            Db.CharSheets.Remove(charsheet);
            await Db.SaveChangesAsync();
            return new OkResult();
        }
    }
}
