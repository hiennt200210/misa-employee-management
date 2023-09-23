using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System;
using MISA.AspNetCore.Domain;
using System.Diagnostics.Eventing.Reader;
using MISA.AspNetCore.Domain.Exceptions;
using System.Runtime.Versioning;
using System.ComponentModel.DataAnnotations;

namespace MISA.AspNetCore.Web
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is BadRequestException badRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = badRequestException.ErrorCode,
                        UserMessage = Domain.Resources.Errors.BadRequest,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = badRequestException.Errors,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else if (exception is ValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        UserMessage = Domain.Resources.Errors.BadRequest,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = "Hello",
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else if (exception is UnauthorizedException unauthorizedException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = unauthorizedException.ErrorCode,
                        UserMessage = Domain.Resources.Errors.Unauthorized,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = unauthorizedException.Errors,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else if (exception is ForbiddenException forbiddenException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = forbiddenException.ErrorCode,
                        UserMessage = Domain.Resources.Errors.Forbidden,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = forbiddenException.Errors,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else if (exception is NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = notFoundException.ErrorCode,
                        UserMessage = Domain.Resources.Errors.NotFound,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = notFoundException.Errors,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            else if (exception is ConflictException conflictException)
            {

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = conflictException.ErrorCode,
                        UserMessage = Domain.Resources.Errors.Conflict,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = conflictException.Errors,
                        MoreInfo = exception.HelpLink

                    }.ToString() ?? ""
                );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        UserMessage = Domain.Resources.Errors.InternalServerError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        Errors = exception.StackTrace,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
        }
    }
}
