using Availability.Api.Backend.Application.UseCases.Shared.Models;
using CSharpFunctionalExtensions;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Api.Backend.Application.UseCases.Shared.Validations
{
    public static class SkuAvailabilitySearchFilterValidation
    {
        public static UnitResult<SharedUsecases.Models.Error> CheckInput(this Domain.ValueObjects.SkuAvailabilitySearchFilter skuAvailabilitySearchFilter)
        {
            var checkResult = skuAvailabilitySearchFilter.Check();
            if (checkResult.IsFailure)
            {
                var checkSkuAvailabilitySearchFilterError = checkResult.Error;

                if (checkSkuAvailabilitySearchFilterError.SkuAvailabilityId.IsFailure)
                {
                    var checkSkuAvailabilityIdError = checkSkuAvailabilitySearchFilterError.SkuAvailabilityId.Error;

                    if (checkSkuAvailabilityIdError.SupplierId.IsFailure)
                        return ErrorBuilder.CreateInvalidBusinessRuleWithDefaumacessage(nameof(Domain.ValueObjects.SkuAvailabilitySearchFilter.SkuAvailabilityId.SupplierId));

                    if (checkSkuAvailabilityIdError.SupplierSkuId.IsFailure)
                        return ErrorBuilder.CreateInvalidBusinessRuleWithDefaumacessage(nameof(Domain.ValueObjects.SkuAvailabilitySearchFilter.SkuAvailabilityId.SupplierSkuId));

                    if (checkSkuAvailabilityIdError.ContractId.IsFailure)
                        return ErrorBuilder.CreateInvalidBusinessRuleWithDefaumacessage(nameof(Domain.ValueObjects.SkuAvailabilitySearchFilter.SkuAvailabilityId.ContractId));
                }

                if (checkSkuAvailabilitySearchFilterError.PersistedSkuId.IsFailure)
                    return ErrorBuilder.CreateInvalidBusinessRuleWithDefaumacessage(nameof(Domain.ValueObjects.SkuAvailabilitySearchFilter.PersistedSkuId));

                if (checkSkuAvailabilitySearchFilterError.ShardId.IsFailure)
                    return ErrorBuilder.CreateInvalidBusinessRuleWithDefaumacessage(nameof(Domain.ValueObjects.SkuAvailabilitySearchFilter.ShardId));

                return ErrorBuilder.CreateInvalidBusinessRule(Domain.ValueObjects.ErrorType.InvalidInput.Error);
            }

            return UnitResult.Success<SharedUsecases.Models.Error>();
        }
    }
}
