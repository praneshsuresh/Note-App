using Notes.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Core
{
    public class NotesServices : INotesServices
    {
        private AppDbContext context;

        public NotesServices(AppDbContext context)
        {
            this.context = context;
        }

        public Note CreateNote(Note note)
        {
            context.Add(note);
            context.SaveChanges();

            return note;
        }

        public Note GetNote(int id)
        {
            var note = context.Notes.FirstOrDefault(n => n.Id == id);

            if(note.Value.Any())
            {
                return note;
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

        public List<Note> GetNotes()
        {
            var notes = context.Notes.ToList();

            if(notes.Any())
            {
                return notes;
            }
            else
            {
                throw new Exception("Notes not found");
            }
        }

        public void DeleteNote(int id)
        {
            var note = context.Notes.FirstOrDefault(n => n.Id == id);
            
            if (note.Value.Any())
            {
                context.Notes.Remove(note);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

        public void EditNote(Note note)
        {
            var editedNote = context.Notes.FirstOrDefault(n => n.Id == note.Id);
            
            if(editedNote.Value.Any())
            {
                editedNote.Value = note.Value;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Note not found");
            }
        }

    }
}
