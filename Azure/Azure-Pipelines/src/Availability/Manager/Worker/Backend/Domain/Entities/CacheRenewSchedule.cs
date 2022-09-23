using CSharpFunctionalExtensions;
using System;

namespace Availability.Manager.Worker.Backend.Domain.Entities
{
    public class CacheRenewSchedule : Entity<ValueObjects.CacheRenewScheduleId>
    {
        public Guid ScheduleId { get; private set; }

        public DateTime ScheduleDate { get; private set; }

        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

        public DateTime LatestUpdatedDate { get; private set; } = DateTime.UtcNow;

        public static CacheRenewSchedule Create(ValueObjects.CacheRenewScheduleId id) =>
            new() { Id = id };

        public Result<CacheRenewSchedule, ValueObjects.ErrorType> ChangeSchedule(Guid newScheduleId, DateTime newScheduleDate)
        {
            if (ScheduleId == newScheduleId && ScheduleDate == newScheduleDate)
                return ValueObjects.ErrorType.ThereIsNoChange;

            ScheduleId = newScheduleId;
            ScheduleDate = newScheduleDate;
            LatestUpdatedDate = DateTime.UtcNow;

            return this;
        }

        public override string ToString() =>
            $"{Id}";
    }
}
