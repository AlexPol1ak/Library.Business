using Library.Business.Managers;
using Library.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Business.Infastructure.DbFakeData
{
    /// <summary>
    /// Базовый клаас нчальных данных приложения
    /// </summary>
    public class DbFakeDataBase
    {

        #region Managers
        protected UserManager _userManager;
        protected StuffManager _stuffManager;
        protected RequestManager _requestManager;
        protected TermManager _termManager;
        protected RackManager _rackManager;
        protected GenreManager _genreManager;
        protected AuthorManager _authorManager;
        protected BookManager _bookManager;
        protected BookHistoryManager _bookHistoryManager;
        #endregion
        protected string _jsonDataName;
        protected string _jsonString;

        public DbFakeDataBase(ManagersFactory factory, string jsonDataName)
        {
            _userManager = factory.UserManager;
            _stuffManager = factory.StuffManager;
            _requestManager = factory.RequestManager;
            _termManager = factory.TermManager;
            _authorManager = factory.AuthorManager;
            _bookManager = factory.BookManager;
            _rackManager = factory.RackManager;
            _bookHistoryManager = factory.BookHistoryManager;
            _genreManager = factory.GenreManager;
            _jsonDataName = jsonDataName;

            _parseJson(jsonDataName);
        }

        /// <summary>
        /// Парсинг данных из  файла JSON.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void _parseJson(string name)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", name);
            _jsonString = File.ReadAllText(path);
            if (_jsonString == string.Empty || _jsonString == null) throw new NullReferenceException(path);
        }
    }
}
