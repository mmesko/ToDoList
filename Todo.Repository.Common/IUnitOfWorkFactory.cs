
namespace Todo.Repository.Common
{
    /// <summary>
    /// Interface for creating unit of work
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
