using Domain.Models;

namespace Application.Abtrastions
{
    public interface IPostRepository
    {
        Task<ICollection<Post>> GetAllPost();

        Task<Post> GetPostById(int postId);

        Task<Post> CreatePost(Post toCreate);

        Task<Post> UpdatePost(string updatedContent, int postId);

        Task DeletePost(int postId);
    }
}