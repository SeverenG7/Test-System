using TestSystem.DataProvider.Interfaces;

namespace TestSystem.Logic.Interfaces
{
    
    public interface IServiceCreator
    { 
        IUserService CreateUserService(IUnitOfWork uow);
    }
}
