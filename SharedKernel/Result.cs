namespace SharedKernel
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public static Result Success(string? message = null)
            => new Result { Succeeded = true, Message = message };

        public static Result Failure(List<string>? errors = null)
            => new Result { Succeeded = false, Errors = errors };

        public static Result Failure(string error)
            => new Result { Succeeded = false, Errors = new List<string> { error } };
    }
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public static Result<T> Success(T data, string? message = null)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static Result<T> Failure(List<string>? errors = null)
        {
            return new Result<T> { Succeeded = false, Errors = errors };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T> { Succeeded = false, Errors = new List<string> { error } };
        }
    }
}
