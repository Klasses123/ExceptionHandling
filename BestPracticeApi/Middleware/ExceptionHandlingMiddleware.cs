using System;
using System.Text;
using System.Threading.Tasks;
using BestPracticeApi.Exceptions.ClientExceptions;
using BestPracticeApi.Exceptions.ServerExceptions;
using BestPracticeApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BestPracticeApi.Middleware
{
    internal sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger = logger;
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var error = DefineHttpError(ex);

            if (error.StatusCode >= 500)
            {
                if (ex is CriticalServerException crEx)
                {
                    _logger.Fatal(crEx.Description, crEx.OriginalException ?? crEx);
                }
                else
                {
                    _logger.Error(ex);
                }
                await WriteHttpResponseAsync(context, error);
            }
            else
            {
                if (error.NeedToLog)
                {
                    _logger.Error(error.Details.Text, ex);
                }
                await WriteHttpResponseAsync(context, error);
            }
        }

        private async Task WriteHttpResponseAsync(HttpContext context, HttpErrorModel error)
        {
            context.Response.StatusCode = error.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error.Details }), Encoding.UTF8);
        }

        private HttpErrorModel DefineHttpError(Exception exception)
        {
            switch (exception)
            {
                case CriticalServerException criticalServerException:
                    return new HttpErrorModel
                    {
                        Details = new HttpErrorModel.ErrorDetails
                        { Text = criticalServerException.Description, Details = criticalServerException.Details },
                        StatusCode = 500
                    };
                case MissingParametersException exMisParams:
                    return new HttpErrorModel
                    {
                        Details = new HttpErrorModel.ErrorDetails
                        { Text = "All fields must be filled!", Details = exMisParams.MissingParameters },
                        StatusCode = 400
                    };
                case InvalidModelException exInvModel:
                    return new HttpErrorModel
                    {
                        Details = new HttpErrorModel.ErrorDetails
                        { Text = exInvModel.Message },
                        StatusCode = 415,
                        NeedToLog = true
                    };
                case NotExistsException notExistsException:
                    return new HttpErrorModel
                    {
                        Details = new HttpErrorModel.ErrorDetails
                        { Text = notExistsException.Message },
                        StatusCode = 404
                    };
                case AlreadyExistException alrExExc:
                    {
                        var description = $"Property {alrExExc.ColumnName} with value {alrExExc.Value} already exists!";

                        return new HttpErrorModel { Details = new HttpErrorModel.ErrorDetails { Text = description }, StatusCode = 409 };
                    }
                default:
                    return new HttpErrorModel
                    {
                        Details = new HttpErrorModel.ErrorDetails
                        { Text = "Undefined server error!" },
                        StatusCode = 500
                    };
            }
        }

        private class HttpErrorModel
        {
            public int StatusCode;
            public bool NeedToLog;
            public ErrorDetails Details = new ErrorDetails();

            public class ErrorDetails
            {
                public string Text;
                public object Details;
            }
        }
    }
}
