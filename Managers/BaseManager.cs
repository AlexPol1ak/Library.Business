using Library.DAL.Repositories;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Managers
{
    public class BaseManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges() => _unitOfWork.SaveChanges();


        /// <summary>
        /// Загружает связанные сущности.
        /// </summary>
        /// <typeparam name="T">Основной тип сущности.</typeparam>
        /// <typeparam name="TProperty">Тип зависимой сущности.</typeparam>
        /// <param name="entity"></param>
        /// <param name="navigationProperty">
        /// Выражение, указывающее на коллекцию связанных сущностей
        /// </param>
        public void LoadRelatedEntities<T, TProperty>
            (T entity, Expression<Func<T, IEnumerable<TProperty>>> navigationProperty)
               where T : class
               where TProperty : class
        {
           _unitOfWork.LoadRelatedEntities(entity, navigationProperty);
        }
    }
}
