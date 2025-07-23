
    namespace TrackademiApi.Utilities
    {
        public class OperationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; } = "";
            public Dictionary<string, string[]>? Errors { get; set; }
            public object? Data { get; set; }

            public static OperationResult Ok(string message, object? data = null) =>
                new OperationResult { Success = true, Message = message, Data = data };

            public static OperationResult Fail(string message, Dictionary<string, string[]>? errors = null) 
            => new OperationResult { Success = false, Message = message, Errors = errors };
        }
    }

