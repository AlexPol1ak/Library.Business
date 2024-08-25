using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления книгами (Book).
    /// </summary>
    public class BookManager : BaseManager
    {
        private readonly IRepository<Book> booksRepository;

        public BookManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            booksRepository = unitOfWork.BooksRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает новую книгу в системе.
        /// </summary>
        /// <param name="book">Книга для добавления.</param>
        public void CreateBook(Book book)
        {
            booksRepository.Create(book);
        }

        /// <summary>
        /// Обновляет данные существующей книги.
        /// </summary>
        /// <param name="book">Книга с обновленными данными.</param>
        public void UpdateBook(Book book)
        {
            booksRepository.Update(book);
        }

        /// <summary>
        /// Удаляет книгу из системы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если книга была успешно удалена, иначе <c>false</c>.</returns>
        public bool DeleteBook(int id)
        {
            return booksRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет книгу из системы.
        /// </summary>
        /// <param name="book">Книга для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если книга была успешно удалена, иначе <c>false</c>.</returns>
        public bool DeleteBook(Book book)
        {
            if (book.BookId <= 0) return false;
            return booksRepository.Delete(book.BookId);
        }

        /// <summary>
        /// Ищет книги, соответствующие заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска книг.</param>
        /// <returns>Запрос, содержащий книги, соответствующие условию.</returns>
        public IQueryable<Book> FindBook(Expression<Func<Book, bool>> predicate)
        {
            return booksRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает все книги в системе.
        /// </summary>
        /// <returns>Коллекция всех книг.</returns>
        public IEnumerable<Book> GetBooks(params string[] includes)
        {
            return booksRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает книгу по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Книга с указанным идентификатором.</returns>
        public Book? GetBook(int id, params string[] includes)
        {
            return booksRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли книга в репозитории.
        /// </summary>
        /// <param name="book">Книга для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если книга существует, иначе <c>false</c>.</returns>
        public bool ContainsBook(Book book)
        {
            return booksRepository.Contains(book);
        }

        /// <summary>
        /// Проверяет, существует ли книга с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор книги для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если книга существует, иначе <c>false</c>.</returns>
        public bool ContainsBook(int id)
        {
            return booksRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество книг в системе.
        /// </summary>
        /// <returns>Количество книг.</returns>
        public int Count()
        {
            return booksRepository.Count();
        }

        #endregion
    }
}
