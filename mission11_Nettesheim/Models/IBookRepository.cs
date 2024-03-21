namespace mission11_Nettesheim.Models
{
    public interface IBookRepository
    {
        public IQueryable<Book> Books { get; }
    }
}
