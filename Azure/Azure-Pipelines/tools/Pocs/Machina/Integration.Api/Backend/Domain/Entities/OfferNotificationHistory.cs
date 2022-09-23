using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Integration.Api.Backend.Domain.Entities
{
    public interface IUpdatedOfferNotificationHistoryBuilder
    {
        IBuildOfferNotificationHistoryBuilder Bind(OfferNotification updated);
    }

    public interface IBuildOfferNotificationHistoryBuilder
    {
        OfferNotificationHistory Build();
    }

    public class OfferNotificationHistory : Entity<string>,
        IUpdatedOfferNotificationHistoryBuilder,
        IBuildOfferNotificationHistoryBuilder
    {
        private const string OutdatedChanges = nameof(OutdatedChanges);
        private const string UpdatedChanges = nameof(UpdatedChanges);

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.PrivateSetterContractResolver()
        };
        private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(JsonSettings);

        private readonly IDictionary<string, string> _changesBuilder = new Dictionary<string, string>();

        public OfferNotification Changes { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        private OfferNotificationHistory()
        {
        }

        public static OfferNotificationHistory Create(OfferNotification updated)
        {
            var history = new OfferNotificationHistory
            {
                Changes = updated
            };

            return history;
        }

        public static IUpdatedOfferNotificationHistoryBuilder CreateDiff(OfferNotification outdated)
        {
            var history = new OfferNotificationHistory();

            history._changesBuilder[OutdatedChanges] = JsonConvert.SerializeObject(outdated);

            return history;
        }

        IBuildOfferNotificationHistoryBuilder IUpdatedOfferNotificationHistoryBuilder.Bind(OfferNotification updated)
        {
            _changesBuilder[UpdatedChanges] = JsonConvert.SerializeObject(updated);

            return this;
        }

        OfferNotificationHistory IBuildOfferNotificationHistoryBuilder.Build()
        {
            //var left = JToken.Parse(_changesBuilder[OutdatedChanges]);
            //var right = JToken.Parse(_changesBuilder[UpdatedChanges]);
            //var patch = new JsonDiffPatch.JsonDiffer().Diff(left, right, true);

            //if (!patch.Operations.Any())
            //    return this;

            ////var output = JToken.Parse("{}");
            //var output = JToken.FromObject(new OfferNotification());
            //var patcher = new JsonDiffPatch.JsonPatcher();
            //patcher.Patch(ref output, patch);
            //var changes = output.ToObject<OfferNotification>(JsonSerializer);

            //var jdp = new JsonDiffPatchDotNet.JsonDiffPatch();
            //var patch = jdp.Diff(_changesBuilder[OutdatedChanges], _changesBuilder[UpdatedChanges]);

            //if (patch == null)
            //    return this;

            //var empty = "{ EnrichedOffer = { } }";
            //var output = jdp.Patch(empty, patch);
            //var changes = JsonConvert.DeserializeObject<OfferNotification>(output, JsonSettings);

            //var left = JToken.Parse(_changesBuilder[OutdatedChanges]);
            //var right = JToken.Parse(_changesBuilder[UpdatedChanges]);
            //var jdp = new JsonDiffPatchDotNet.JsonDiffPatch();
            //var patch = jdp.Diff(left, right);

            //if (patch == null)
            //    return this;

            //var output = jdp.Patch(left, patch);
            //var changes = output.ToObject<OfferNotification>(JsonSerializer);

            var changes = JsonConvert.DeserializeObject<OfferNotification>(_changesBuilder[UpdatedChanges], JsonSettings);
            Changes = changes;

            return this;
        }
    }
}
