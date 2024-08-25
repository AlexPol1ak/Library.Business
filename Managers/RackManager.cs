using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления полками (Rack).
    /// </summary>
    public class RackManager : BaseManager
    {
        private readonly IRepository<Rack> racksRepository;

        public RackManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            racksRepository = unitOfWork.RackRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает новую полку в системе.
        /// </summary>
        /// <param name="rack">Полка для добавления.</param>
        public void CreateRack(Rack rack)
        {
            racksRepository.Create(rack);
        }

        /// <summary>
        /// Обновляет данные существующей полки.
        /// </summary>
        /// <param name="rack">Полка с обновленными данными.</param>
        public void UpdateRack(Rack rack)
        {
            racksRepository.Update(rack);
        }

        /// <summary>
        /// Удаляет полку из системы по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полки для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если полка была успешно удалена, иначе <c>false</c>.</returns>
        public bool DeleteRack(int id)
        {
            return racksRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет полку из системы.
        /// </summary>
        /// <param name="rack">Полка для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если полка была успешно удалена, иначе <c>false</c>.</returns>
        public bool DeleteRack(Rack rack)
        {
            if (rack.RackId <= 0) return false;
            return racksRepository.Delete(rack.RackId);
        }

        /// <summary>
        /// Ищет полки, соответствующие заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска полок.</param>
        /// <returns>Запрос, содержащий полки, соответствующие условию.</returns>
        public IQueryable<Rack> FindRack(Expression<Func<Rack, bool>> predicate)
        {
            return racksRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает все полки в системе.
        /// </summary>
        /// <returns>Коллекция всех полок.</returns>
        public IEnumerable<Rack> GetRacks(params string[] includes)
        {
            return racksRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает полку по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор полки.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Полка с указанным идентификатором.</returns>
        public Rack? GetRack(int id, params string[] includes)
        {
            return racksRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли полка в репозитории.
        /// </summary>
        /// <param name="rack">Полка для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если полка существует, иначе <c>false</c>.</returns>
        public bool ContainsRack(Rack rack)
        {
            return racksRepository.Contains(rack);
        }

        /// <summary>
        /// Проверяет, существует ли полка с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор полки для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если полка существует, иначе <c>false</c>.</returns>
        public bool ContainsRack(int id)
        {
            return racksRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество полок в системе.
        /// </summary>
        /// <returns>Количество полок.</returns>
        public int Count()
        {
            return racksRepository.Count();
        }

        #endregion
    }
}
