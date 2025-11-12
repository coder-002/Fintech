using Fintech.Data;
using Fintech.Interfaces;
using Fintech.Models;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository;

public class CommentRepository: ICommentRepository
{
    private readonly AppDbContext _context;
    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.Include(a=>a.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync( int id,Comment commentModel)
    {
        var existingComment = await _context.Comments.FindAsync(id);
        if (existingComment == null)
        {
            return null;
        }

        existingComment.Title = commentModel.Title;
        existingComment.Content = commentModel.Content;

        await _context.SaveChangesAsync();
        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x=> x.Id == id);
        if (commentModel == null)
        {
            return null;
        }

        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
}