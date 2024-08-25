using Library.Domain.Entities.Users;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Managers
{
    /// <summary>
    /// Менеджер управления читатетлями.
    /// </summary>
    public class UserManager : BaseManager
    {
        private IRepository<User> usersRepository;

        public UserManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            usersRepository = unitOfWork.UserRepository;
        }

        #region CRUD operations
        /// <summary>
        /// Создает нового пользователя в системе.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        public void CreateUser(User user)
        {
            usersRepository.Create(user);
        }

        /// <summary>
        /// Обновляет данные существующего пользователя.
        /// </summary>
        /// <param name="user">Пользователь с обновленными данными.</param>
        public void UpdateUser(User user)
        {
            usersRepository.Update(user);
        }

        /// <summary>
        /// Удаляет пользователя из системы по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если пользователь был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteUser(int id)
        {
            return usersRepository.Delete(id);
        }

        /// <summary>
        /// Удаляет пользователя из системы.
        /// </summary>
        /// <param name="user">Пользователь для удаления.</param>
        /// <returns>Возвращает <c>true</c>, если пользователь был успешно удален, иначе <c>false</c>.</returns>
        public bool DeleteUser(User user)
        {
            if (user.UserId <= 0) return false;
            return usersRepository.Delete(user.UserId);
        }

        /// <summary>
        /// Ищет пользователей, соответствующих заданному условию.
        /// </summary>
        /// <param name="predicate">Условие для поиска пользователей.</param>
        /// <returns>Запрос, содержащий пользователей, соответствующих условию.</returns>
        public IQueryable<User> FindUser(Expression<Func<User, bool>> predicate)
        {
            IQueryable<User> users = usersRepository.Find(predicate);
            LoadRequests(users);
            return users;
        }

        /// <summary>
        /// Возвращает всех пользователей в системе.
        /// </summary>
        /// <returns>Коллекция всех пользователей.</returns>
        public IEnumerable<User> GetUsers(params string[] includes)
        {
            return usersRepository.GetAll(includes);
        }

        /// <summary>
        /// Получает пользователя по идентификатору с возможностью включения связанных данных.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="includes">Имена связанных данных, которые следует включить.</param>
        /// <returns>Пользователь с указанным идентификатором.</returns>
        public User? GetUser(int id, params string[] includes)
        {
            return usersRepository.Get(id, includes);
        }

        /// <summary>
        /// Проверяет, существует ли пользователь в репозитории.
        /// </summary>
        /// <param name="user">Пользователь для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если пользователь существует, иначе <c>false</c>.</returns>
        public bool ContainsUser(User user)
        {
            return usersRepository.Contains(user);
        }

        /// <summary>
        /// Проверяет, существует ли пользователь с указанным идентификатором в репозитории.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если пользователь существует, иначе <c>false</c>.</returns>
        public bool ContainsUser(int id)
        {
            return usersRepository.Get(id) != null;
        }

        /// <summary>
        /// Возвращает количество пользователей в системе.
        /// </summary>
        /// <returns>Количество пользователей.</returns>
        public int Count()
        {
            return usersRepository.Count();
        }
        #endregion
        #region Request operations
        /// <summary>
        /// Добавляет читателю запрос на книгу
        /// </summary>
        /// <param name="idUser">ID читателя</param>
        /// <param name="request">Запрос</param>
        /// <returns>True- если запрос добавлен, иначе False</returns>
        public bool AddRequest(int idUser, Request request)
        {
            User? user = usersRepository.Get(idUser);
            // Если читатель не найден
            if (user is null) throw new ArgumentNullException(nameof(user));
            // Если запрос принадлжеит другому читателю
            if (request.UserId <= 0) throw new ArgumentException($"Запрос {request} пренадлежит другому читателю");
            // Если у читателя уже есть запрос на эту книгу
            if (user.Requests.Contains(request)) return false;

            request.UserId = user.UserId;
            user.Requests.Add(request);
            return true;
        }

        /// <summary>
        /// Удаялет запрос на книгу у читателя.
        /// </summary>
        /// <param name="idUser">ID читателя</param>
        /// <param name="request">Запрос</param>
        /// <returns>True- если запрос добавлен, иначе False</returns>
        public bool DeleteRequest(int idUser, Request request)
        {
            User? user = usersRepository.Get(idUser);
            // Если читатель не найден
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (request.UserId != user.UserId) throw new ArgumentException($"Запрос {request} пренадлежит другому читателю");
            if (!user.Requests.Contains(request)) return false;

            user.Requests.Remove(request);
            return true;
        }
        #endregion

    }
}
