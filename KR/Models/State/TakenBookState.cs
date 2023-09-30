namespace KR.Models.State
{
    public class TakenBookState : IbookStats
    {
        public void ReturnTheBook(Book book)
        {
            book.State = "В наличии";
            book.BookState = new BookInStockState();
        }

        public void TakeTheBook(Book book)
        {
            throw new Exception();
        }
    }
}
