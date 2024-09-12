using Library.Business.Managers;
using Library.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;


namespace Library.Business.Infastructure.DbFakeData
{
    /// <summary>
    /// Класс устнановки данных книг,авторов, жанров, стелажей, правил выдачи.
    /// </summary>
    public class BookFakeData: DbFakeDataBase
    {
        public BookFakeData(ManagersFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Устанавливает начальные данные книг,авторов, жанров, стелажей, правил выдачи
        /// </summary>
        public virtual bool InstallData()
        {
            if(_jsonString == string.Empty) return false;

            List<Genre> tempData = _convertJsonData();
            List<Term> termsDb = getOrCreateTerms();
            List<Rack> raksDb = getOrCreateRacks(tempData);
            List<Author> authorsDb = getOrCreateAuthors(tempData);
            List<Genre> genresDb = getOrCreateGenre(tempData);
            List<Book> booksDb = getOrCreateBook(tempData, termsDb, genresDb);
            installBooksRelationship(tempData, booksDb, raksDb, authorsDb);

            return true;
        }

        /// <summary>
        /// Извлекает данные из считанной JSON строки.
        /// </summary>
        /// <returns>Жанры с книгками и авторами</returns>
        /// <exception cref="NullReferenceException">Если JSON строка пуста</exception>
        private List<Genre> _convertJsonData()
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, List<Book>>>(_jsonString);
            if (data == null) throw new NullReferenceException();

            List<Genre> genres = new List<Genre>();
            foreach (var kvp in data)
            {
                Genre genre = new(kvp.Key);
                foreach (var book in kvp.Value)
                {
                    genre.Books.Add(book);
                }
                genres.Add(genre);
            }
            return genres;
        }

        /// <summary>
        /// Получает или  создает список правил выдачи книг.
        /// </summary>
        /// <returns></returns>
        private List<Term> getOrCreateTerms()
        {
            List<Term> terms = new List<Term>();
            terms = _termManager.GetTerms().ToList();

            if (terms.Find(ft => ft.ReadLocation == "Читальный зал") == null)
            {
                Term readingRoomTerm = new("Читальный зал");
                _termManager.CreateTerm(readingRoomTerm);
                _termManager.SaveChanges();
                terms.Add(readingRoomTerm);
            }

            if (terms.Find(ft => ft.ReadLocation == "Домашнее чтение") == null)
            {
                Term homeReadingTerm = new("Домашнее чтение", 10);
                _termManager.CreateTerm(homeReadingTerm);
                _termManager.SaveChanges();
                terms.Add(homeReadingTerm);
            }
            return terms;
        }

        /// <summary>
        /// Получает или создает список стелажей.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Rack> getOrCreateRacks(List<Genre> data)
        {
            string suff1 = "_1";
            string suff2 = "_2";

            foreach (var genre in data)
            {
                string nameRack1 = genre.Name + suff1;
                string nameRack2 = genre.Name + suff2;

                if (!(_rackManager.FindRack(fr1 => fr1.Name == nameRack1).Count() > 0))
                {
                    Rack rack1 = new(nameRack1);
                    _rackManager.CreateRack(rack1);
                }

                if (!(_rackManager.FindRack(fr2 => fr2.Name == nameRack2).Count() > 0))
                {
                    Rack rack2 = new(nameRack2);
                    _rackManager.CreateRack(rack2);
                }


            }
            _rackManager.SaveChanges();
            return _rackManager.GetRacks().ToList();
        }

        /// <summary>
        /// Получает или создает список авторов
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Author> getOrCreateAuthors(List<Genre> data)
        {
            foreach (var genre in data)
            {
                foreach (Book book in genre.Books)
                {
                    foreach (Author author in book.Authors)
                    {
                        List<Author> authorList = new List<Author>();
                        authorList = _authorManager.FindAuthor(
                            fa => fa.FirstName == author.FirstName &&
                            fa.LastName == author.LastName
                            ).ToList();

                        if (authorList.Count == 0)
                        {
                            Author authorDb = new(author.FirstName, author.LastName, author.Patronymic);
                            _authorManager.CreateAuthor(authorDb);
                            _authorManager.SaveChanges();
                        }
                    }
                }
            }
            return _authorManager.GetAuthors().ToList();
        }

        /// <summary>
        /// Получает или создает список жанров.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Genre> getOrCreateGenre(List<Genre> data)
        {
            foreach (var genre in data)
            {
                List<Genre> genresList = _genreManager.FindGenre(fg => fg.Name == genre.Name).ToList();
                if (genresList.Count == 0)
                {
                    Genre genreDb = new(genre.Name);
                    _genreManager.CreateGenre(genreDb);
                    _genreManager.SaveChanges();
                }
            }
            return _genreManager.GetGenres().ToList();
        }

        /// <summary>
        /// Получает или создает список жанров
        /// </summary>
        /// <param name="genresData"></param>
        /// <param name="termsDb"></param>
        /// <param name="genresDb"></param>
        /// <returns></returns>
        private List<Book> getOrCreateBook(List<Genre> genresData, List<Term> termsDb, List<Genre> genresDb)
        {
            foreach (var genre in genresData)
            {
                foreach (Book b in genre.Books)
                {
                    List<Book> booksList = _bookManager.FindBook(fb =>
                                              fb.Name == b.Name &&
                                              fb.PublicationDate == b.PublicationDate &&
                                              fb.NumberPages == b.NumberPages
                                              ).ToList();
                    if (booksList.Count == 0)
                    {
                        Book book = new(b.Name, b.NumberPages, b.PublicationDate, b.Description);

                        if (book.NumberPages < 200)
                        {
                            book.Term = termsDb.Find(ft => ft.MaximumDays == null);
                        }
                        else { book.Term = termsDb.Find(ft => !(ft.MaximumDays == null)); }

                        Genre bookGenre = genresDb.FirstOrDefault(
                            fg => fg.Name == genre.Name,
                            new Genre(genre.Name)
                            );

                        book.Genre = bookGenre;
                        _bookManager.CreateBook(book);
                        _bookManager.SaveChanges();

                    }
                }
            }
            return _bookManager.GetBooks().ToList();
        }

        /// <summary>
        /// Устаналвивает связь между книгами и авторами, и стелажами. (m:m, 1:1)
        /// </summary>
        /// <param name="data">Данные из JSON</param>
        /// <param name="booksDb">Данные книг из базы данных</param>
        /// <param name="raksDb">Данные о стелажах из базы данных</param>
        /// <param name="authorsDb">Данные о авторах</param>
        private void installBooksRelationship(List<Genre> data, List<Book> booksDb, List<Rack> raksDb, List<Author> authorsDb)
        {           

            foreach (Book bookDb in booksDb)
            {   
                // Установка книг на полки.
                if(bookDb.Rack == null)
                {
                    List<Rack> actualRaks = raksDb.FindAll(r => r.Name.Contains(bookDb.Genre.Name));
                    Random rd = new Random();
                    bookDb.Rack = actualRaks[rd.Next(0, actualRaks.Count)];
                    _bookManager.UpdateBook(bookDb);
                }
                

                //Подгружает связанные сущности  авторов.                
                _bookManager.LoadRelatedEntities(bookDb, b => b.Authors);
                if (bookDb.Authors.Count != 0) continue; // Пропустить если у книги авторы установлены
                // Установка авторов
                var authorsLisData = from genre in data
                                     from book in genre.Books
                                     from authors in book.Authors
                                     where genre.Name == bookDb.Genre.Name
                                     where book.Name == bookDb.Name && book.PublicationDate == bookDb.PublicationDate
                                     select book.Authors.ToList();

                foreach (var authors in authorsLisData)
                {
                    foreach (var a in authors)
                    {
                        Author authorBookDb = authorsDb.First(af => af.FirstName == a.FirstName && af.LastName == a.LastName);
                        authorBookDb.Books.Clear();
                        bookDb.Authors.Add(authorBookDb);
                    }
                }
                _bookManager.UpdateBook(bookDb);
            }
            _bookManager.SaveChanges();
        }

    }
}
