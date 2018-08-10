using System;
using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestPassService
    {
        void StartTest(int IdResult, int IdTest);
    }
}
