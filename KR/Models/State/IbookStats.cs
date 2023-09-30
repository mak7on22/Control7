namespace KR.Models.State
{
    public interface IbookStats
    {
        void TakeTheBook(Book book);
        void ReturnTheBook(Book book);
    }
}
