using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using System.Data.Entity;

namespace TestSystem.DataProvider.Repositories
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly TestContext context;

        public QuestionRepository(TestContext _context)
        {
            context = _context;
        }

        public IEnumerable<Question> GetAll() => context.Questions;

        public Question Get(int id) => context.Questions.Find(id);

        public IEnumerable<Question> Find(Func<Question, bool> predicate)
        {
            return context.Questions.Where(predicate).ToList();
        }

        public void Create(Question question) => context.Questions.Add(question);

        public void Update(Question question)
        {
            context.Entry(question).State = EntityState.Modified;

        }

        public void Delete(int id)
        {
            Question question = context.Questions.Find(id);
            if (question != null)
                context.Questions.Remove(question);

        }
    }
}
