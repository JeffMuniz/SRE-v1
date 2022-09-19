using Availability.Api.Backend.Application.UseCases.Shared.Models;
using Microsoft.AspNetCore.Http;
using System;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Api.Backend.Infrastructure.Web.Extensions
{
    public static class ErrorModelExtensions
    {
        public static int GetHttpStatus(this SharedUsecases.Models.Error model)
        {
            if (!Enum.TryParse<ErrorBuilder.Codes>(model.Code, out var errorCode))
                errorCode = ErrorBuilder.Codes.UnexpectedResult;

            return errorCode switch
            {
                ErrorBuilder.Codes.RegisterNotFound => StatusCodes.Status404NotFound,
                ErrorBuilder.Codes.InvalidBusinessRule => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };
        }
    }
}
