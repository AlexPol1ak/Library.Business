using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления жанрами книг (Genre).
    /// </summary>
    public class GenreManager : BaseManager
    {
        private readonly IRepository<Genre> genresRepository;

        public GenreManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            genresRepository = unitOfWork.GenresRepository;
        }

        #region CRUD operations

        /// <summary>
        /// Создает новый жанр в системе.
        /// </summary>
        /// <param name="genre">Жанр для добавления.</param>
        public void CreateGenre(Genre genre)
        {
            genresRepository.Create(genre);
        }

        /// <summary>
        /// Обновляет данные существующего жанра.
        /// </summary>
        /// <param name="genre">Жанр с обновленными данными.</param>
        public void UpdateGenre(Genre genre)
        {
            genresRepository.Update(genre);
        }

        /// <summary>
        /// Удаляет жанр из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор жанра для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если жанр был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteGenre(int id)
        {
            return genresRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет жанр из системы.
        /// </summary>
        /// <param name="genre">Жанр для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если жанр был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteGenre(Genre genre)
        {
            if (genre.GenreId <= 0) return false;
            return genresRepository.Delete(genre.GenreId);
        }

        /// <summary>
        /// Ищет жанры, соответствующие заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска жанров.</param>
        /// <returns>Запрос, содержащий жанры, соответствующие условию.</returns>
        public IQueryable<Genre> FindGenre(Expression<Func<Genre, bool>> predicate)
        {
            return genresRepository.Find(predicate);
        }

        /// <summary>
        /// Возвращает все жанры в системе.
        /// </summary>
        /// <returns>Коллекция всех жанров.</returns>
        public IEnumerable<Genre> GetGenres(params string[] includes)
        {
            return genresRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает жанр по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Жанр с указанным идентификатором.</returns>
        public Genre? GetGenre(int id, params string[] includes)
        {
            return genresRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли жанр в репозитории.
        /// </summary>
        /// <param name="genre">Жанр для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если жанр существует, иначе <c>false</c>.</returns>
        public bool ContainsGenre(Genre genre)
        {
            return genresRepository.Contains(genre);
        }

        /// <summary>
        /// Проверяет, существует ли жанр с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор жанра для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если жанр существует, иначе <c>false</c>.</returns>
        public bool ContainsGenre(int id)
        {
            return genresRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество жанров в системе.
        /// </summary>
        /// <returns>Количество жанров.</returns>
        public int Count()
        {
            return genresRepository.Count();
        }

        #endregion
    }
}
