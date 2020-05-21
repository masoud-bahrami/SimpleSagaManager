using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSagaManager
{
    public class Notification
    {
        private readonly IList<string> _errors = new List<string>();
        private readonly IList<Exception> _exceptions = new List<Exception>();
        public void AddError(string message) => _errors.Add(message);
        public void AddError(Exception ex) => _exceptions.Add(ex);
        public void AddError(string message, Exception ex) {
            AddError(message);
            AddError(ex);
        }
        public bool HasError() => _errors.Any() || _exceptions.Any();
        public bool HasExceptionOf<T>() => _exceptions.Any(_ => _.GetType() == typeof(T));

        public string Errors() {
            if (!HasError()) return string.Empty;

            var result = _errors.Any() ? ErrorsMessage() : string.Empty;

            if (_exceptions.Any())
                result = !string.IsNullOrWhiteSpace(result)
                          ? $"{result} - {EceptionsMessage()}"
                          : EceptionsMessage();

            return result;
        }
        private string EceptionsMessage() => $"{_exceptions?.Select(_ => _.Message).Aggregate((a, b) => $"{a}-{b}")}";
        private string ErrorsMessage() => $"{_errors?.Aggregate((x, y) => $"{x}-{y}")}";
    }
}