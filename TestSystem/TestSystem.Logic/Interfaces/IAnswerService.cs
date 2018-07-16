using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IAnswerService
    {
        void Create(int idQuestion, AnswerDTO answerDTO);
        void Remove(int id);
        void Update(AnswerDTO answerDTO);
        void Dispose();
    }
}
