namespace WPCRecruitmentTest.Models
{
    public sealed class QueryResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }   
        public static QueryResult Success() => new QueryResult { Succeeded = true };
        public static QueryResult Failure(string error) => new QueryResult { Error = error, Succeeded = false }; 
    }
}
