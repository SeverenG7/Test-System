using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IAnswerService
    {
        void Create(int idQuestion, AnswerDto answerDTO);
        void Remove(int id);
        void Update(AnswerDto answerDTO);
        void Dispose();
    }
}
