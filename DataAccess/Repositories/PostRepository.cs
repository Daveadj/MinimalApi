using Application.Abtrastions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _context;

        public PostRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(Post toCreate)
        {
            toCreate.DateCreated = DateTime.Now;
            toCreate.LastModified = DateTime.Now;
            _context.Posts.Add(toCreate);
            await _context.SaveChangesAsync();
            return toCreate;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return;

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPost()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<Post> UpdatePost(string updatedContent, int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            post.LastModified = DateTime.Now;
            post.Content = updatedContent;
            await _context.SaveChangesAsync();
            return post;
        }
    }
}