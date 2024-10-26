using Library.Domain.Entities.Users;

namespace Library.Business.Infastructure.DbFakeData
{
    /// <summary>
    /// Класс установки начальных данных пользователя и персонала.
    /// </summary>
    public class UserFakeData : BookFakeData
    {
        public UserFakeData(ManagersFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Создает данные пользователей и персонал
        /// </summary>
        public override bool InstallData()
        {
            bool flag = base.InstallData();
            if (!flag) return false;

            installStuff();
            installUsers();

            return true;
        }

        /// <summary>
        /// Устанавливает персонал.
        /// </summary>
        private void installStuff()
        {
            List<Stuff> stuffsDb = _stuffManager.GetStuffs().ToList();
            List<Stuff> stuffsFake = UserFakeData.getStuffs();

            foreach (Stuff Fstuff in stuffsFake)
            {
                if (stuffsDb.FindAll(sDB => sDB.Email == Fstuff.Email).Count > 0) continue;
                _stuffManager.CreateStuff(Fstuff);
            }
            _stuffManager.SaveChanges();
        }

        /// <summary>
        /// Устанавливает пользователей
        /// </summary>
        private void installUsers()
        {
            List<User> usersDb = _userManager.GetUsers().ToList();
            List<User> userFake = UserFakeData.getUsers();

            foreach (User Fuser in userFake)
            {
                if (usersDb.FindAll(uDB => uDB.Email == Fuser.Email).Count > 0) continue;
                _userManager.CreateUser(Fuser);
            }
            _userManager.SaveChanges();
        }

        /// <summary>
        /// Генерирует фальшивые данные персонала
        /// </summary>
        /// <returns></returns>
        private static List<Stuff> getStuffs()
        {
            List<Stuff> stuff = new()
            {
                new("admin1@example.com", "password123", "Иван", "Иванов", "Иванович", true),
                new("admin2@example.com", "password456", "Петр", "Петров", "Петрович", true),
                new("admin3@example.com", "password789", "Сергей", "Сергеев", "Сергеевич", true),
                new("admin4@example.com", "password012", "Андрей", "Андреев", "Андреевич", true),
                new("admin5@example.com", "password345", "Максим", "Максимов", "Максимович", true)
            };
            return stuff;
        }

        /// <summary>
        /// Генерирует фальшивые данные пользователя
        /// </summary>
        /// <returns></returns>
        private static List<User> getUsers()
        {
            List<User> users = new()
            {
            new("user1@example.com", "Иван", "Петров", "Андреевич"),
            new("user2@example.com", "Ольга", "Смирнова", "Владимировна"),
            new("user3@example.com", "Николай", "Кузнецов", "Александрович"),
            new("user4@example.com", "Мария", "Васильева", "Игоревна"),
            new("user5@example.com", "Сергей", "Михайлов", "Павлович"),
            new("user6@example.com", "Анна", "Фёдорова", "Сергеевна"),
            new("user7@example.com", "Дмитрий", "Соколов", "Николаевич"),
            new("user8@example.com", "Екатерина", "Попова", "Петровна"),
            new("user9@example.com", "Александр", "Лебедев", "Геннадьевич"),
            new("user10@example.com", "Елена", "Новикова", "Анатольевна")
            };

            return users;
        }
    }
}
