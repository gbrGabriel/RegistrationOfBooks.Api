namespace RegistrationOfBooks.Api.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string title { get; set; }
        public string gender { get; set; }
        public DateTime release_date { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public int number_pages { get; set; }
        public string language { get; set; }
        public string publishing_company { get; set; }
    }
}
