using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Model.Models;
using TestSystem.Logic.LogicView;
using System.Linq;
using PagedList;
using System.Web.Mvc;
using System.Web;

namespace TestSystem.Logic.Services
{
    public class CommonService : ICommonService
    {
        private IUnitOfWork Database { get; }
        public CommonService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public FiltrationViewModel FilterQuestions(int? IdTheme, 
            string difficult, int? IdQuestion, 
            int? IdTest, string search,
            int? page )
        {
            List<string> stateFilter = new List<string>();
            FiltrationViewModel filterModel = new FiltrationViewModel();
            if (!page.HasValue)
            {
                stateFilter.Add(difficult);
                stateFilter.Add(search);

                HttpContext.Current.Session["StateFilter"] = stateFilter;
                int pageSize = 5;
                int pageNumber = page ?? 1;

                IEnumerable<Question> questions = Database.Questions.GetAll();

                if (IdTheme.HasValue && IdTheme != 0)
                {
                    stateFilter.Add(IdTheme.Value.ToString());
                    questions = questions.Where(x => x.IdTheme == IdTheme);
                }

                if (!String.IsNullOrEmpty(difficult) && !difficult.Equals("All"))
                {
                    questions = questions.Where(x => x.Difficult == difficult);
                }

                if (!String.IsNullOrEmpty(search))
                {
                    questions = questions.Where(x => x.QuestionText.Contains(search));
                }

                List<Theme> themes = Database.Themes.GetAll().ToList();
                themes.Insert(0, new Theme() { IdTheme = 0, ThemeName = "All" });


                if (IdQuestion.HasValue)
                {
                    if (filterModel.Questions == null)
                        filterModel.IdQuestion = IdQuestion.Value;
                    filterModel.Answers = questions.
                        Where(x => x.IdQuestion == IdQuestion).
                        SingleOrDefault().
                        Answers;
                    filterModel.Tests = questions.
                        Where(x => x.IdQuestion == IdQuestion).
                        SingleOrDefault().
                        Tests.ToPagedList(pageNumber,pageSize);
                }
                if (IdTest.HasValue)
                {
                    filterModel.IdTest = IdTest.Value;
                    questions = filterModel.Tests.
                       Where(x => x.IdTest == IdTest).
                       SingleOrDefault().
                       Questions.ToPagedList(pageNumber, pageSize);
                }

                filterModel.Questions = questions.ToPagedList(pageNumber, pageSize);
                filterModel.Themes = new SelectList(themes, "IdTheme", "ThemeName");

                return filterModel;
            }
            else
            {

                int pageSize = 5;
                int pageNumber = page ?? 1;

                stateFilter = (List<string>)HttpContext.Current.Session["StateFilter"];
                difficult = stateFilter[0];
                search = stateFilter[1];


                IEnumerable<Question> questions = Database.Questions.GetAll();
                if (IdTheme.HasValue && IdTheme != 0)
                {
                    questions = questions.Where(x => x.IdTheme == IdTheme);
                }

                if (!String.IsNullOrEmpty(difficult) && !difficult.Equals("All"))
                {
                    questions = questions.Where(x => x.Difficult == difficult);
                }

                if (!String.IsNullOrEmpty(search))
                {
                    questions = questions.Where(x => x.QuestionText.Contains(search));
                }

                List<Theme> themes = Database.Themes.GetAll().ToList();
                themes.Insert(0, new Theme() { IdTheme = 0, ThemeName = "All" });


                if (IdQuestion.HasValue)
                {
                    if (filterModel.Questions == null)
                        filterModel.IdQuestion = IdQuestion.Value;
                    filterModel.Answers = questions.
                        Where(x => x.IdQuestion == IdQuestion).
                        SingleOrDefault().
                        Answers;
                    filterModel.Tests = questions.
                        Where(x => x.IdQuestion == IdQuestion).
                        SingleOrDefault().
                        Tests.ToPagedList(pageNumber, pageSize);
                }
                if (IdTest.HasValue)
                {
                    filterModel.IdTest = IdTest.Value;
                    questions = filterModel.Tests.
                       Where(x => x.IdTest == IdTest).
                       SingleOrDefault().
                       Questions.ToPagedList(pageNumber, pageSize);
                }

                filterModel.Questions = questions.ToPagedList(pageNumber, pageSize);
                filterModel.Themes = new SelectList(themes, "IdTheme", "ThemeName");

                return filterModel;
            }
        }

        public FiltrationViewModel FilterTests(int? IdTheme,
            string difficult, int? IdQuestion, int? IdTest,
            string search,
           int? page )
        {
            List<string> stateFilter = new List<string>();
            FiltrationViewModel filterModel = new FiltrationViewModel();
            if (!page.HasValue)
            {
                stateFilter.Add(difficult);
                stateFilter.Add(search);

                HttpContext.Current.Session["StateFilter"] = stateFilter;
                int pageSizeTests = 5;
                int pageNumberTests = page ?? 1;


                IEnumerable<Test> tests = Database.Tests.GetAll();

                if (IdTheme.HasValue && IdTheme != 0)
                {
                    stateFilter.Add(IdTheme.Value.ToString());
                    tests = tests.Where(x => x.IdTheme == IdTheme);
                }

                if (!String.IsNullOrEmpty(difficult) && !difficult.Equals("All"))
                {
                    tests = tests.Where(x => x.Difficult == difficult);
                }

                if (!String.IsNullOrEmpty(search))
                {
                    tests = tests.Where(x => x.TestName.Contains(search));
                }

                List<Theme> themes = Database.Themes.GetAll().ToList();
                themes.Insert(0, new Theme() { IdTheme = 0, ThemeName = "All" });


                if (IdTest.HasValue)
                {
                    filterModel.IdTest = IdTest.Value;
                    filterModel.Questions = tests.
                       Where(x => x.IdTest == IdTest).
                       SingleOrDefault().
                       Questions.ToPagedList(1, 100);
                }
                filterModel.Tests = tests.ToPagedList(pageNumberTests, pageSizeTests);
                filterModel.Themes = new SelectList(themes, "IdTheme", "ThemeName");

                return filterModel;
            }
            else
            {

                int pageSize = 5;
                int pageNumber = page ?? 1;

                stateFilter = (List<string>)HttpContext.Current.Session["StateFilter"];
                difficult = stateFilter[0];
                search = stateFilter[1];


                IEnumerable<Question> questions = Database.Questions.GetAll();
                if (IdTheme.HasValue && IdTheme != 0)
                {
                    questions = questions.Where(x => x.IdTheme == IdTheme);
                }

                if (!String.IsNullOrEmpty(difficult) && !difficult.Equals("All"))
                {
                    questions = questions.Where(x => x.Difficult == difficult);
                }

                if (!String.IsNullOrEmpty(search))
                {
                    questions = questions.Where(x => x.QuestionText.Contains(search));
                }

                List<Theme> themes = Database.Themes.GetAll().ToList();
                themes.Insert(0, new Theme() { IdTheme = 0, ThemeName = "All" });


                if (IdQuestion.HasValue)
                {
                    if (filterModel.Questions == null)
                        filterModel.IdQuestion = IdQuestion.Value;
                    filterModel.Answers = questions.
                        Where(x => x.IdQuestion == IdQuestion).
                        SingleOrDefault().
                        Answers;
                    filterModel.Tests = questions.
                        Where(x => x.IdQuestion == IdQuestion).
                        SingleOrDefault().
                        Tests.ToPagedList(pageNumber, pageSize);
                }
                if (IdTest.HasValue)
                {
                    filterModel.IdTest = IdTest.Value;
                    questions = filterModel.Tests.
                       Where(x => x.IdTest == IdTest).
                       SingleOrDefault().
                       Questions.ToPagedList(pageNumber, pageSize);
                }

                filterModel.Questions = questions.ToPagedList(pageNumber, pageSize);
                filterModel.Themes = new SelectList(themes, "IdTheme", "ThemeName");

                return filterModel;
            }
        }
    }
}
