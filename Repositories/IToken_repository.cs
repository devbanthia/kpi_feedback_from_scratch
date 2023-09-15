using kpi_feedback_from_scratch.Models.Domain.User;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IToken_repository
    {
        public string GenerateToken(User user);
    }
}
