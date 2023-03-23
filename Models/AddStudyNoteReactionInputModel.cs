namespace DevStudyNotes.API.Models
{
    public class AddStudyNoteReactionInputModel
    {
        public int StudyNoteId { get; set; }
        public bool IsPositive { get; set; }
    }
}