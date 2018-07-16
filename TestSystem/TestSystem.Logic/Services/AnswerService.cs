using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;

namespace TestSystem.Logic.Services
{
    public class AnswerService : MapClass<Answer , AnswerDTO> , IAnswerService
    {
        IUnitOfWork Database { get; set; }
        
        public AnswerService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void Create(int idQuestion, AnswerDTO answerDTO)
        {   
            Answer answer = MapperTo.Map<Answer>(answerDTO);
            Database.Answers.Add(answer);
            Database.Complete();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Remove(int id)
        {
            Answer answer = (Answer)Database.Tests.Find(x => x.IdTest == id);
            if (answer != null)
            {
                Database.Answers.Remove(answer);
                Database.Complete();
            }
        }

        public void Update(AnswerDTO answerDTO)
        {
            Answer answer = (Answer)Database.Answers.Find(x => x.IdAnswer == answerDTO.IdAnswer);

            if (answer != null)
            {
                answer = MapperTo.Map<Test>(answer);
                Database.Complete();
            }
        }
    }
}
