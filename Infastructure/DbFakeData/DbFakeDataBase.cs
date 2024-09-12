using Library.Business.Managers;
using Library.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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
        protected string _jsonString = string.Empty;

        public DbFakeDataBase(ManagersFactory factory)
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
            
            _parseJson();
        } 
        /// <summary>
        /// Парсинг данных из  файла JSON.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void _parseJson()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "books_and_authors.json");
                _jsonString = File.ReadAllText(path);
            }
            catch(Exception ex) { };
     
        }
    }
}
