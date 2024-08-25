using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления сроками (Term).
    /// </summary>
    public class TermManager : BaseManager
    {
        private readonly IRepository<Term> termsRepository;

        public TermManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            termsRepository = unitOfWork.TermRepository;
        }

        #region CRUD operations

        /// <```csharp
        /// Создает новый срок в системе.
        /// </summary>
        /// <param name="term">Срок для добавления.</param>
        public void CreateTerm(Term term)
        {
            termsRepository.Create(term);
        }

        /// <summary>
        /// Обновляет данные существующего срока.
        /// </summary>
        /// <param name="term">Срок с обновленными данными.</param>
        public void UpdateTerm(Term term)
        {
            termsRepository.Update(term);
        }

        /// <summary>
        /// Удаляет срок из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор срока для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если срок был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteTerm(int id)
        {
            return termsRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет срок из системы.
        /// </summary>
        /// <param name="term">Срок для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если срок был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteTerm(Term term)
        {
            if (term.TermId <= 0) return false;
            return termsRepository.Delete(term.TermId);
        }

        /// <summary>
        /// Ищет сроки, соответствующие заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска сроков.</param>
        /// <returns>Запрос, содержащий сроки, соответствующие условию.</returns>
        public IQueryable<Term> FindTerm(Expression<Func<Term, bool>> predicate)
        {
            return termsRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает все сроки в системе.
        /// </summary>
        /// <returns>Коллекция всех сроков.</returns>
        public IEnumerable<Term> GetTerms()
        {
            return termsRepository.GetAll();
        }

        /// <summary>
        /// Получает срок по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор срока.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Срок с указанным идентификатором.</returns>
        public Term? GetTerm(int id, params string[] includes)
        {
            return termsRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли срок в репозитории.
        /// </summary>
        /// <param name="term">Срок для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если срок существует, иначе <c>false</c>.</returns>
        public bool ContainsTerm(Term term)
        {
            return termsRepository.Contains(term);
        }

        /// <summary>
        /// Проверяет, существует ли срок с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор срока для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если срок существует, иначе <c>false</c>.</returns>
        public bool ContainsTerm(int id)
        {
            return termsRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество сроков в системе.
        /// </summary>
        /// <returns>Количество сроков.</returns>
        public int Count()
        {
            return termsRepository.Count();
        }

        #endregion
    }
}
