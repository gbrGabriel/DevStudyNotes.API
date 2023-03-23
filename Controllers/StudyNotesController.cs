using DevStudyNotes.API.Entities;
using DevStudyNotes.API.Models;
using DevStudyNotes.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DevStudyNotes.API.Controllers
{
    [ApiController]
    [Route("api/study-notes")]
    public class StudyNotesController : ControllerBase
    {
        private readonly StudyNoteDbContext _context;
        public StudyNotesController(StudyNoteDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<StudyNote>>> GetAll()
        {
            var studyNotes = await _context.StudyNotes.Include(s => s.Reactions).ToListAsync();

            Log.Information("GetAll is called");

            return Ok(studyNotes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudyNote>> GetById(int id)
        {
            var studyNote = await _context.StudyNotes.Include(s => s.Reactions).SingleOrDefaultAsync(s => s.Id == id);

            if (studyNote == null) return NotFound();

            Log.Information("GetById is called");

            return Ok(studyNote);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddStudyNoteInputModel model)
        {
            var studyNote = new StudyNote(model.Title, model.Description, model.IsPublic);

            _context.StudyNotes.Add(studyNote);
            await _context.SaveChangesAsync();

            Log.Information("Post is called");

            return CreatedAtAction("GetById", new { id = studyNote.Id }, model);
        }

        [HttpPost("{id}/reactions")]
        public async Task<ActionResult> PostReaction(int id, AddStudyNoteReactionInputModel model)
        {
            var studyNote = await _context.StudyNotes.SingleOrDefaultAsync(s => s.Id == id);

            if (studyNote == null) return BadRequest();

            studyNote.AddReaction(model.IsPositive);

            await _context.SaveChangesAsync();

            Log.Information("Post is called");

            return CreatedAtAction("PostReaction", new { id = studyNote.Reactions.Where(n => n.StudyNoteId == id).FirstOrDefault().Id }, model);
        }
    }
}