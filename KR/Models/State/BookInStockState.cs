namespace KR.Models.State
{
    public class BookInStockState : IbookStats
    {
        public void ReturnTheBook(Book book)
        {
            throw new Exception();
        }

        public void TakeTheBook(Book book)
        {
            book.State = "Взята";
            book.BookState = new TakenBookState();
        }
    }
}
