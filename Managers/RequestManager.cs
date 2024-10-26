using Library.Domain.Entities.Users;
using Library.Domain.Interfaces;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления запросами на выдачу книг (Request).
    /// </summary>
    public class RequestManager : BaseManager
    {
        private readonly IRepository<Request> requestsRepository;

        public RequestManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            requestsRepository = unitOfWork.RequestsRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает новый запрос на выдачу книги в системе.
        /// </summary>
        /// <param name="request">Запрос для добавления.</param>
        public void CreateRequest(Request request)
        {
            requestsRepository.Create(request);
        }

        /// <summary>
        /// Обновляет данные существующего запроса.
        /// </summary>
        /// <param name="request">Запрос с обновленными данными.</param>
        public void UpdateRequest(Request request)
        {
            requestsRepository.Update(request);
        }

        /// <summary>
        /// Удаляет запрос из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор запроса для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если запрос был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteRequest(int id)
        {
            return requestsRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет запрос из системы.
        /// </summary>
        /// <param name="request">Запрос для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если запрос был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteRequest(Request request)
        {
            if (request.RequestId <= 0) return false;
            return requestsRepository.Delete(request.RequestId);
        }

        /// <summary>
        /// Ищет запросы, соответствующие заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска запросов.</param>
        /// <returns>Запрос, содержащий запросы, соответствующие условию.</returns>
        public IQueryable<Request> FindRequest(Expression<Func<Request, bool>> predicate)
        {
            return requestsRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает все запросы в системе.
        /// </summary>
        /// <returns>Коллекция всех запросов.</returns>
        public IEnumerable<Request> GetRequests(params string[] includes)
        {
            return requestsRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает запрос по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор запроса.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Запрос с указанным идентификатором.</returns>
        public Request? GetRequest(int id, params string[] includes)
        {
            return requestsRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли запрос в репозитории.
        /// </summary>
        /// <param name="request">Запрос для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если запрос существует, иначе <c>false</c>.</returns>
        public bool ContainsRequest(Request request)
        {
            return requestsRepository.Contains(request);
        }

        /// <summary>
        /// Проверяет, существует ли запрос с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор запроса для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если запрос существует, иначе <c>false</c>.</returns>
        public bool ContainsRequest(int id)
        {
            return requestsRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество запросов в системе.
        /// </summary>
        /// <returns>Количество запросов.</returns>
        public int Count()
        {
            return requestsRepository.Count();
        }

        #endregion
    }
}
