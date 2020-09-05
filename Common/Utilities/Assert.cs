using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Common.Utilities
{
    public static class ValidationHelper
    {
        public static List<string> GetValidationErrors(this Exception ex)
        {
            var exception = ex as DbEntityValidationException;
            var errorMessages = new List<string>();
            if (exception != null)
            {
                var errors = exception;
                errorMessages = errors.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => $"Property: {x.PropertyName} - Error: {x.ErrorMessage}").ToList();
            }
            return errorMessages;
        }
    }
    public static class Assert
    {
        public static void NotNull<T>(T obj, string name, string message = null)
            where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void NotNull<T>(T? obj, string name, string message = null)
            where T : struct
        {
            if (!obj.HasValue)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);

        }

        public static void NotEmpty<T>(T obj, string name, string message = null, T defaultValue = null)
            where T : class
        {
            if (obj == defaultValue
                || (obj is string str && string.IsNullOrWhiteSpace(str))
                || (obj is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
            }
        }
    }
}
