using BookdsApi.Modelos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BookdsApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() => _books.Find(book => true).ToList();

        public Book Get(string id) => _books.Find<Book>(_book => _book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string Id, Book bookIn) => _books.ReplaceOne(book => book.Id == Id, bookIn);

        public void Remove(Book bookIn) => _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string Id) => _books.DeleteOne(book => book.Id == Id);
    }
}
