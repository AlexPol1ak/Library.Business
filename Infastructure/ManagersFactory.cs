using Library.Business.Managers;
using Library.DAL.Repositories;
using Library.Domain.Entities.Books;
using Library.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Business.Infastructure
{
    /// <summary>
    /// Класс фабрики менеджеров. Создает и предоставляет доступ к менеджерам
    /// </summary>
    public class ManagersFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserManager UserManager { get; private set; }
        public StuffManager StuffManager { get; private set; }
        public RequestManager RequestManager { get; private set; }
        public AuthorManager AuthorManager { get; private set; }
        public GenreManager GenreManager { get; private set; }
        public TermManager TermManager { get; private set; }
        public BookManager BookManager { get; private set; }
        public RackManager RackManager { get; private set; }
        public BookHistoryManager BookHistoryManager { get; private set; }

        public ManagersFactory(string connectionStringName, string mySqlVersionStringName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            //Строка подключения
            string? connectionString = config.GetConnectionString(connectionStringName);
            if (connectionString == null)
                throw new KeyNotFoundException($"Ключ {connectionStringName} не найден");
            // Версия базы данных.
            string? sqlVersion = config[mySqlVersionStringName];
            if (sqlVersion == null)
                throw new KeyNotFoundException($"Ключ {mySqlVersionStringName} не найден");
           
            _unitOfWork =  new EfUnitOfWork(connectionString, sqlVersion);
            initManagers();
        }

        /// <summary>
        /// Инициализация менедежеров.
        /// </summary>
        private void initManagers()
        {
            UserManager = new UserManager(_unitOfWork);
            StuffManager = new StuffManager(_unitOfWork);
            RequestManager = new RequestManager(_unitOfWork);
            AuthorManager = new AuthorManager(_unitOfWork);
            GenreManager = new GenreManager(_unitOfWork);
            TermManager = new TermManager(_unitOfWork);
            BookManager = new BookManager(_unitOfWork);
            BookHistoryManager = new BookHistoryManager(_unitOfWork);
            RackManager = new RackManager(_unitOfWork);
        }

    }
}
