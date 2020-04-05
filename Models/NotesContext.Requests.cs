using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Notes.Models
{
    public partial class NotesContext
    {
        public async Task<User> GetUserById(int id)
        {
            return await Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Note> GetNote(int noteId)
        {
            return await Notes.FindAsync(noteId);
        }

        public async Task<List<Note>> GetNotes(int userId)
        {
            return await Notes.Where(n => n.Userid == userId).ToListAsync();
        }

        public async Task<List<Note>> GetNotes(int userId, string subStr)
        {
            return await Notes.Where(n => n.Userid == userId &&
            (n.Text.ToLower().Contains(subStr.ToLower()) || n.Title.ToLower().Contains(subStr.ToLower()))).ToListAsync();
        }

        public async Task<bool> AddUser(User user)
        {
            Users.Add(user);
            return await SaveChangesAsync() != 0;
        }

        public async Task<bool> AddNote(Note note)
        {
            Notes.Add(note);
            return await SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteNote(int id)
        {
            Notes.Remove(Notes.Find(id));
            return await SaveChangesAsync() != 0;
        }
    }
}
