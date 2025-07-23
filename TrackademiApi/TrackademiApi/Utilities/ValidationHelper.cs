using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TrackademiApi.Utilities
{
    public static class ValidationHelper
    {
        public static OperationResult FormatearErroresModelo(ModelStateDictionary modelState)
        {
            var errores = modelState
                .Where(m => m.Value.Errors.Any())
                .ToDictionary(
                    m => m.Key,
                    m => m.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return OperationResult.Fail("Error de validación", errores);
        }
    }
}
