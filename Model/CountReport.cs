namespace FetchDailyReport.Model
{
    public class Pagination
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Start { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Returned { get; set; }
    }

    public class CountReport
    {
        public Pagination pagination { get; set; }
    }
}
