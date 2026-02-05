namespace ProSphere.ResultResponse
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? SuccessMessage { get; }
        public string? ErrorMessage { get; }
        public int? ErrorCode { get; }
        public IDictionary<string, List<string>>? ValidationErrors { get; }


        protected Result
            (bool isSucces, string? successMessage = null, string? errorMessage = null, int? errorCode = null, IDictionary<string, List<string>>? validationErrors = null)
        {
            IsSuccess = isSucces;
            SuccessMessage = successMessage;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            ValidationErrors = validationErrors;
        }

        public static Result Success(string successMessage) => new Result(true, successMessage);
        public static Result Failure(string errorMessage, int errorCode) => new Result(false, null, errorMessage, errorCode);
        public static Result ValidationFailure(IDictionary<string, List<string>> validationErrors) => new Result(false, null, "Validation Invalid", StatusCodes.Status400BadRequest, validationErrors!);
    }


    public class Result<T> : Result
    {
        public T? Value { get; }
        protected Result
            (T? value, bool isSucces, string? successMessage = null, string? errorMessage = null, int? errorCode = null, IDictionary<string, List<string>>? validationErrors = null)
            : base(isSucces, successMessage, errorMessage, errorCode, validationErrors)
        {
            Value = value;
        }

        public static Result<T> Success(T value, string? successMessage = null) => new Result<T>(value, true, successMessage);
        public static Result<T> Failure(string errorMessage, int errorCode) => new Result<T>(default, false, null, errorMessage, errorCode);
        public static Result<T> ValidationFailure(IDictionary<string, List<string>> validationErrors) => new Result<T>(default, false, null, "Validation Invalid", StatusCodes.Status400BadRequest, validationErrors!);
    }
}
