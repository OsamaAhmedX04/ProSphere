using System.Text.Json.Serialization;

namespace ProSphere.ResultResponse
{
    public class Result
    {
        [JsonPropertyOrder(1)]
        public bool IsSuccess { get; }

        [JsonPropertyOrder(2)]
        public string? SuccessMessage { get; }

        [JsonPropertyOrder(3)]
        public bool IsFailure => !IsSuccess;

        [JsonPropertyOrder(4)]
        public string? ErrorMessage { get; }

        [JsonPropertyOrder(5)]
        public int StatusCode { get; }

        [JsonPropertyOrder(6)]
        public IDictionary<string, List<string>>? ValidationErrors { get; }


        protected Result
            (bool isSucces, int statusCode, string? successMessage = null, string? errorMessage = null, IDictionary<string, List<string>>? validationErrors = null)
        {
            IsSuccess = isSucces;
            StatusCode = statusCode;
            SuccessMessage = successMessage;
            ErrorMessage = errorMessage;
            ValidationErrors = validationErrors;
        }

        public static Result Success(string successMessage) => new Result(true, StatusCodes.Status200OK, successMessage);
        public static Result Failure(string errorMessage, int errorCode) => new Result(false, errorCode, null, errorMessage);
        public static Result ValidationFailure(IDictionary<string, List<string>> validationErrors) => new Result(false, StatusCodes.Status400BadRequest, null, "Validation Invalid", validationErrors!);
    }


    public class Result<T> : Result
    {
        [JsonPropertyOrder(7)]
        public T? Value { get; }
        protected Result
            (T? value, bool isSucces, int statusCode, string? successMessage = null, string? errorMessage = null, IDictionary<string, List<string>>? validationErrors = null)
            : base(isSucces, statusCode, successMessage, errorMessage, validationErrors)
        {
            Value = value;
        }

        public static Result<T> Success(T value, string? successMessage = null) => new Result<T>(value, true, StatusCodes.Status200OK, successMessage);
        public static Result<T> Failure(string errorMessage, int errorCode) => new Result<T>(default, false, errorCode, null, errorMessage);
        public static Result<T> ValidationFailure(IDictionary<string, List<string>> validationErrors) => new Result<T>(default, false, StatusCodes.Status400BadRequest, null, "Validation Invalid", validationErrors!);



    }
}
