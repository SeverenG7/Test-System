using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using System.Data.Entity;

namespace TestSystem.DataProvider.Repositories
{
    public class UnitOfWorkTests : IUnitOfWorkTests
    {
        private TestContext context;
        private TestRepository testRepository;
        private QuestionRepository questionRepository;
        private AnswerRepository answerRepository;

        public UnitOfWorkTests() => context = new TestContext();

        public IRepository<Test> Tests
        {
            get => testRepository ?? 
                (testRepository = new TestRepository(context));
        }

        public IRepository<Question> Questions
        {
            get => questionRepository ??
                (questionRepository = new QuestionRepository(context));
        }

        public IRepository<Answer> Answers
        {
            get => answerRepository ??
                (answerRepository = new AnswerRepository(context));
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
