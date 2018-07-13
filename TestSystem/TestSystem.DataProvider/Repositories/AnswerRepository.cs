using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using System.Data.Entity;

namespace TestSystem.DataProvider.Repositories
{
    public class AnswerRepository : IRepository<Answer>
    {
        private readonly TestContext context;

        public AnswerRepository(TestContext _context)
        {
            context = _context;
        }

        public IEnumerable<Answer> GetAll() => context.Answers;

        public Answer Get(int id) => context.Answers.Find(id);

        public IEnumerable<Answer> Find(Func<Answer, bool> predicate)
        {
            return context.Answers.Where(predicate).ToList();
        }

        public void Create(Answer answer) => context.Answers.Add(answer);

        public void Update(Answer answer)
        {
            context.Entry(answer).State = EntityState.Modified;

        }

        public void Delete(int id)
        {
            Answer answer = context.Answers.Find(id);
            if (answer != null)
                context.Answers.Remove(answer);

        }
    }
}
