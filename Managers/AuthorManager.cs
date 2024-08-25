using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления авторами книг (Author).
    /// </summary>
    public class AuthorManager : BaseManager
    {
        private readonly IRepository<Author> authorRepository;

        public AuthorManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            authorRepository = unitOfWork.AuthorsRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает нового автора в системе.
        /// </summary>
        /// <param name="author">Автор для добавления.</param>
        public void CreateAuthor(Author author)
        {
            authorRepository.Create(author);
        }

        /// <summary>
        /// Обновляет данные существующего автора.
        /// </summary>
        /// <param name="author">Автор с обновленными данными.</param>
        public void UpdateAuthor(Author author)
        {
            authorRepository.Update(author);
        }

        /// <summary>
        /// Удаляет автора из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автора для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если автор был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteAuthor(int id)
        {
            return authorRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет автора из системы.
        /// </summary>
        /// <param name="author">Автор для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если автор был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteAuthor(Author author)
        {
            if (author.AuthorId <= 0) return false;
            return authorRepository.Delete(author.AuthorId);
        }

        /// <summary>
        /// Ищет авторов, соответствующих заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска авторов.</param>
        /// <returns>Запрос, содержащий авторов, соответствующих условию.</returns>
        public IQueryable<Author> FindAuthor(Expression<Func<Author, bool>> predicate)
        {
            return authorRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает всех авторов в системе.
        /// </summary>
        /// <returns>Коллекция всех авторов.</returns>
        public IEnumerable<Author> GetAuthors(params string[] includes)
        {
            return authorRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает автора по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор автора.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Автор с указанным идентификатором.</returns>
        public Author? GetAuthor(int id, params string[] includes)
        {
            return authorRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли автор в репозитории.
        /// </summary>
        /// <param name="author">Автор для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если автор существует, иначе <c>false</c>.</returns>
        public bool ContainsAuthor(Author author)
        {
            return authorRepository.Contains(author);
        }

        /// <summary>
        /// Проверяет, существует ли автор с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор автора для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если автор существует, иначе <c>false</c>.</returns>
        public bool ContainsAuthor(int id)
        {
            return authorRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество авторов в системе.
        /// </summary>
        /// <returns>Количество авторов.</returns>
        public int Count()
        {
            return authorRepository.Count();
        }

        #endregion
    }
}
