using System;
using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestPassService
    {
        QuestionDto StartTest(int IdResult);     
    }
}
