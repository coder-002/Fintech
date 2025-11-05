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
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync();
    }
}