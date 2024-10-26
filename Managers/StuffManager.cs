using Library.Domain.Entities.Users;
using Library.Domain.Interfaces;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления сотрудниками (Stuff).
    /// </summary>
    public class StuffManager : BaseManager
    {
        private readonly IRepository<Stuff> stuffRepository;

        public StuffManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            stuffRepository = unitOfWork.StuffRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает нового сотрудника в системе.
        /// </summary>
        /// <param name="stuff">Сотрудник для добавления.</param>
        public void CreateStuff(Stuff stuff)
        {
            stuffRepository.Create(stuff);
        }

        /// <summary>
        /// Обновляет данные существующего сотрудника.
        /// </summary>
        /// <param name="stuff">Сотрудник с обновленными данными.</param>
        public void UpdateStuff(Stuff stuff)
        {
            stuffRepository.Update(stuff);
        }

        /// <summary>
        /// Удаляет сотрудника из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если сотрудник был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteStuff(int id)
        {
            return stuffRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет сотрудника из системы.
        /// </summary>
        /// <param name="stuff">Сотрудник для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если сотрудник был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteStuff(Stuff stuff)
        {
            if (stuff.UserId <= 0) return false;
            return stuffRepository.Delete(stuff.UserId);
        }

        /// <summary>
        /// Ищет сотрудников, соответствующих заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска сотрудников.</param>
        /// <returns>Запрос, содержащий сотрудников, соответствующих условию.</returns>
        public IQueryable<Stuff> FindStuff(Expression<Func<Stuff, bool>> predicate)
        {
            return stuffRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает всех сотрудников в системе.
        /// </summary>
        /// <returns>Коллекция всех сотрудников.</returns>
        public IEnumerable<Stuff> GetStuffs(params string[] includes)
        {
            return stuffRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает сотрудника по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Сотрудник с указанным идентификатором.</returns>
        public Stuff? GetStuff(int id, params string[] includes)
        {
            return stuffRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли сотрудник в репозитории.
        /// </summary>
        /// <param name="stuff">Сотрудник для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если сотрудник существует, иначе <c>false</c>.</returns>
        public bool ContainsStuff(Stuff stuff)
        {
            return stuffRepository.Contains(stuff);
        }

        /// <summary>
        /// Проверяет, существует ли сотрудник с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если сотрудник существует, иначе <c>false</c>.</returns>
        public bool ContainsStuff(int id)
        {
            return stuffRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество сотрудников в системе.
        /// </summary>
        /// <returns>Количество сотрудников.</returns>
        public int Count()
        {
            return stuffRepository.Count();
        }

        #endregion
    }
}
