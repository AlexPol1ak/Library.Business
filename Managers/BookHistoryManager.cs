using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления историей книг (BookHistory).
    /// </summary>
    public class BookHistoryManager : BaseManager
    {
        private readonly IRepository<BookHistory> bookHistoriesRepository;

        public BookHistoryManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            bookHistoriesRepository = unitOfWork.BookHistoryRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает новую запись в истории книг в системе.
        /// </summary>
        /// <param name="bookHistory">Запись истории книги для добавления.</param>
        public void CreateBookHistory(BookHistory bookHistory)
        {
            bookHistoriesRepository.Create(bookHistory);
        }

        /// <summary>
        /// Обновляет данные существующей записи в истории книг.
        /// </summary>
        /// <param name="bookHistory">Запись истории книги с обновленными данными.</param>
        public void UpdateBookHistory(BookHistory bookHistory)
        {
            bookHistoriesRepository.Update(bookHistory);
        }

        /// <summary>
        /// Удаляет запись в истории книг из системы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи истории книги для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если запись истории книги была успешно удалена, иначе <c>false</c>.</returns>
        public bool DeleteBookHistory(int id)
        {
            return bookHistoriesRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет запись в истории книг из системы.
        /// </summary>
        /// <param name="bookHistory">Запись истории книги для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если запись истории книги была успешно удалена, иначе <c>false</c>.</returns>
        public bool DeleteBookHistory(BookHistory bookHistory)
        {
            if (bookHistory.BookHistoryId <= 0) return false;
            return bookHistoriesRepository.Delete(bookHistory.BookHistoryId);
        }

        /// <summary>
        /// Ищет записи в истории книг, соответствующие заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска записей истории книг.</param>
        /// <returns>Запрос, содержащий записи истории книг, соответствующие условию.</returns>
        public IQueryable<BookHistory> FindBookHistory(Expression<Func<BookHistory, bool>> predicate)
        {
            return bookHistoriesRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает все записи в истории книг в системе.
        /// </summary>
        /// <returns>Коллекция всех записей истории книг.</returns>
        public IEnumerable<BookHistory> GetBookHistories(params string[] includes)
        {
            return bookHistoriesRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает запись в истории книг по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор записи истории книги.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Запись истории книги с указанным идентификатором.</returns>
        public BookHistory? GetBookHistory(int id, params string[] includes)
        {
            return bookHistoriesRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли запись в истории книг в репозитории.
        /// </summary>
        /// <param name="bookHistory">Запись истории книги для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если запись существует, иначе <c>false</c>.</returns>
        public bool ContainsBookHistory(BookHistory bookHistory)
        {
            return bookHistoriesRepository.Contains(bookHistory);
        }

        /// <summary>
        /// Проверяет, существует ли запись в истории книг с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор записи истории книги для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если запись существует, иначе <c>false</c>.</returns>
        public bool ContainsBookHistory(int id)
        {
            return bookHistoriesRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество записей в истории книг в системе.
        /// </summary>
        /// <returns>Количество записей истории книг.</returns>
        public int Count()
        {
            return bookHistoriesRepository.Count();
        }

        #endregion
    }
}
