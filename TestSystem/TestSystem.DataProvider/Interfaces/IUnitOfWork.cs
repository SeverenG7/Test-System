using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Model.Models;

namespace TestSystem.DataProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
