using Feedo.Domain;

namespace Feedo.Core.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> GetComments();
        Task<double> GetCommentsAverage();
    }
}