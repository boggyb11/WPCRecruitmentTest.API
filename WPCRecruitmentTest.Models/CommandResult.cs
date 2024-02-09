namespace WPCRecruitmentTest.Models
{
    public sealed class CommandResult<T>
    {
        public bool Succeeded { get; set; }
        public Error? Error { get; set; }
        public T? Content { get; set; }
        public static CommandResult<T> Success(T Content) => new() { Content = Content, Succeeded = true };
        public static CommandResult<T> Failure(Error error) => new() { Error = error, Succeeded = false };

    }
}
