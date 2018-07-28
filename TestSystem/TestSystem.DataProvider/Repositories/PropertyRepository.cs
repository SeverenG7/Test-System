using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class PropertyRepository : Repository<Property>
    {
        /// <summary>
        /// Realization of generic Repository class.
        /// Also, here can add specific IRepository interfaces for entites,
        /// if this will be needed.
        /// </summary>
        public PropertyRepository(ApplicationContext context) : base(context)
        { }

        public ApplicationContext testContext
        {
            get => context as ApplicationContext;
        }
    }
}

