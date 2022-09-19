using AutoMapper;
using Automatonymous;
using Automatonymous.Binders;
using MassTransit;
using Microsoft.Extensions.Logging;
using Product.Saga.Worker.Backend.Infrastructure.Persistence.Repositories;
using Product.Saga.Worker.Saga.States;
using Shared.Messaging.Configuration;
using System;
using System.Threading;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Product.Saga.Worker.Saga
{
    public class SkuSaga : MassTransitStateMachine<SkuState>
    {
        private readonly ILogger<SkuSaga> _logger;
        private readonly IMapper _mapper;
        private readonly ISkuSagaHistoryRepository _skuSagaHistoryRepository;

        #region [States]

        public State Failed { get; private set; }

        public State ReceivedToAdd { get; private set; }

        public State ReceivedToUpdate { get; private set; }

        public State ReceivedToUpdateEnriched { get; private set; }

        public State ReceivedToRemove { get; private set; }

        public State NotifedToEnrichment { get; private set; }

        public State ReceivedEnriched { get; private set; }

        public State NotifedToRemove { get; private set; }

        public State ForwardedToCategorization { get; private set; }

        public State ReceivedCategorized { get; private set; }

        public State ForwardedForPersistence { get; private set; }

        public State ReceivedPersistedInStorage { get; private set; }

        public State ForwardedToSearchIndex { get; private set; }

        public State ReceivedIndexedInTheSearch { get; private set; }

        public State ForwardedForRegisterAvailability { get; private set; }

        #endregion [States]

        #region [Events]

        public Event<SagaMessages.Change.AddSku> AddSku { get; private set; }

        public Event<SagaMessages.Change.UpdateSku> UpdateSku { get; private set; }

        public Event<SagaMessages.Enrichment.UpdateSkuEnriched> UpdateSkuEnriched { get; private set; }

        public Event<SagaMessages.Persistence.SkuUpserted> SkuUpsertedInPersistence { get; private set; }

        public Event<SagaMessages.Search.SkuIndexedInTheSearch> SkuIndexedInTheSearch { get; private set; }

        public Event<SagaMessages.Change.RemoveSku> RemoveSku { get; private set; }

        public Event<SagaMessages.Persistence.SkuRemoved> SkuRemovedFromPersistence { get; private set; }

        public Event<SagaMessages.Categorization.SkuCategorized> SkuCategorized { get; private set; }

        public Event<AvailabilityMessages.Manager.SkuRemovedFromAvailability> SkuRemovedFromAvailability { get; private set; }

        public Event<SagaMessages.Search.SkuRemovedFromSearchIndex> SkuRemovedFromSearchIndex { get; private set; }

        public Event RemoveSkuCompleted { get; private set; }

        #endregion [Events]

        #region [Requests]

        public Request<SkuState, ChangeMessages.GetSkuDetail, ChangeMessages.GetSkuDetailResponse, SharedMessages.NotFound> GetSkuDetail { get; private set; }

        #endregion [Requests]

        public SkuSaga(
            ILogger<SkuSaga> logger,
            IMapper mapper,
            ISkuSagaHistoryRepository skuSagaHistoryRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _skuSagaHistoryRepository = skuSagaHistoryRepository;

            #region [Event Correlations]

            Event(() => AddSku,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierSku.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSku.SkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => UpdateSku,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierSku.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSku.SkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => SkuCategorized,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => UpdateSkuEnriched,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => SkuUpsertedInPersistence,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => SkuIndexedInTheSearch,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => RemoveSku,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => SkuRemovedFromPersistence,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => SkuRemovedFromSearchIndex,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            Event(() => SkuRemovedFromAvailability,
                x => x.CorrelateBy((state, context) =>
                    state.SupplierSku.SupplierId == context.Message.SupplierId &&
                    state.SupplierSku.SkuId == context.Message.SupplierSkuId
                )
                .SelectId(context => context.CorrelationId ?? context.InitiatorId ?? NewId.NextGuid())
            );

            #endregion [Event Correlations]

            #region [Requests Configurations]

            Request(
                () => GetSkuDetail,
                x =>
                {
                    x.ServiceAddress = EndpointConfigurator.GetCommandEndpoint<ChangeMessages.GetSkuDetail>();
                    x.Timeout = TimeSpan.FromMinutes(1);
                }
            );

            #endregion [Requests Configurations]

            InstanceState(x => x.CurrentState);

            Initially(
                When(AddSku)
                    .InitializeSaga(_mapper, Saga.States.Models.SkuFlowType.Add)
                    .TransitionTo(ReceivedToAdd),
                When(UpdateSku)
                    .InitializeSaga(_mapper, Saga.States.Models.SkuFlowType.Update)
                    .TransitionTo(ReceivedToUpdate),
                When(UpdateSkuEnriched)
                    .InitializeSaga(_mapper, Saga.States.Models.SkuFlowType.UpdateEnriched)
                    .TransitionTo(ReceivedToUpdateEnriched),
                When(RemoveSku)
                    .InitializeSaga(_mapper, Saga.States.Models.SkuFlowType.Remove)
                    .TransitionTo(ReceivedToRemove)
            );

            BeforeEnterAny(
                x => x.Then(context =>
                {
                    context.Instance.LastUpdatedDate = DateTime.Now;
                })
            );

            CompositeEvent(() => RemoveSkuCompleted,
                x => x.RemoveCompletedStatus,
                SkuRemovedFromPersistence,
                SkuRemovedFromSearchIndex,
                SkuRemovedFromAvailability
            );

            WhenEnter(ReceivedToAdd,
                x => x.SendSkuToCategorization(mapper)
                    .TransitionTo(ForwardedToCategorization)
            );

            WhenEnter(ReceivedToUpdate,
                x => x.SendSkuForPersistence()
                    .TransitionTo(ForwardedForPersistence)
            );

            WhenEnter(ReceivedToUpdateEnriched,
                x => x.Request(GetSkuDetail,
                    context => context.Init<ChangeMessages.GetSkuDetail>(new
                    {
                        SkuIntegrationId = context.Instance.SkuIntegrationId
                    })
                )
                .TransitionTo(GetSkuDetail.Pending)
            );

            WhenEnter(ReceivedToRemove,
                x => x.SendSkuToRemoveFromEnrichment(mapper)
                    .SendSkuToRemoveFromPersistence(mapper)
                    .SendSkuToRemoveSkuFromSearchIndex(mapper)
                    .SendSkuToRemoveFromAvailability(mapper)
                    .TransitionTo(NotifedToRemove)
            );

            During(GetSkuDetail.Pending,
                When(GetSkuDetail.Completed)
                    .Then(context =>
                    {
                        context.Instance.SupplierSku = _mapper.Map(context.Data, context.Instance.SupplierSku);
                    })
                    .TransitionTo(ReceivedEnriched),
                When(GetSkuDetail.Completed2)
                    .TransitionTo(Failed),
                When(GetSkuDetail.Faulted)
                    .TransitionTo(Failed),
                When(GetSkuDetail.TimeoutExpired)
                    .TransitionTo(Failed));

            During(ForwardedToCategorization,
                When(SkuCategorized)
                    .Then(context =>
                    {
                        context.Instance.CategorizedData = _mapper.Map(context.Data, context.Instance.CategorizedData);
                    })
                    .TransitionTo(ReceivedCategorized)
            );

            During(NotifedToEnrichment,
                When(UpdateSkuEnriched, x => x.Data.SkuIntegrationId == x.Instance.SkuIntegrationId)
                    .Then(context =>
                    {
                        context.Instance.EnrichedData = _mapper.Map(context.Data, context.Instance.EnrichedData);
                    })
                    .TransitionTo(ReceivedEnriched)
            );

            During(NotifedToRemove,
                When(RemoveSkuCompleted)
                    .Finalize()
            );

            WhenEnter(ReceivedCategorized,
                 x => x.SendSkuForPersistence()
                 .TransitionTo(ForwardedForPersistence)
             );

            WhenEnter(ReceivedEnriched,
                x => x.SendSkuForPersistence()
                .TransitionTo(ForwardedForPersistence)
            );

            During(ForwardedForPersistence,
                When(SkuUpsertedInPersistence)
                    .Then(context =>
                    {
                        context.Instance.PersistedSku = _mapper.Map(context.Data, context.Instance.PersistedSku);
                    })
                    .TransitionTo(ReceivedPersistedInStorage)
            );

            WhenEnter(ReceivedPersistedInStorage,
                x => x.IfElse(context => context.Instance.PersistedSku.Subcategory is not null,
                        then => then
                            .SendSkuToSearchIndex(mapper)
                            .TransitionTo(ForwardedToSearchIndex),
                        @else => @else
                            .SendSkuForEnrichment(mapper)
                            .TransitionTo(NotifedToEnrichment)
                            .Finalize()
                    )
            );

            During(ForwardedToSearchIndex,
                When(SkuIndexedInTheSearch)
                    .TransitionTo(ReceivedIndexedInTheSearch)
            );

            WhenEnter(ReceivedIndexedInTheSearch,
                x => x.SendSkuToGetAvailabilityForAllContracts()
                    .TransitionTo(ForwardedForRegisterAvailability)
            );

            WhenEnter(ForwardedForRegisterAvailability,
                x => x.If(context => context.Instance.FlowType.NotIs(Saga.States.Models.SkuFlowType.UpdateEnriched),
                        then => then
                            .SendSkuForEnrichment(mapper)
                            .TransitionTo(NotifedToEnrichment)
                    )
                    .Finalize()
            );

            SetCompleted(async instance =>
            {
                var currentState = await this.GetState(instance);
                var isFailed = currentState == Failed;
                var isCompleted = currentState == Final || isFailed;

                if (isCompleted)
                {
                    var logLevel = isFailed ? LogLevel.Error : LogLevel.Information;

                    _logger.Log(logLevel,
                        "The saga is being completed with state {CurrentState} and will be deleted. SkuIntegrationId: {SkuIntegrationId}, SupplierId: {SupplierId}, SupplierSkuId: {SupplierSkuId}",
                        currentState, instance.SkuIntegrationId, instance.SupplierSku.SupplierId, instance.SupplierSku.SkuId
                    );

                    await _skuSagaHistoryRepository.Add(instance, CancellationToken.None);
                }

                return isCompleted;
            });
        }
    }

    public static class SkuSagaExtensions
    {
        public static EventActivityBinder<SkuState, TData> InitializeSaga<TData>(this EventActivityBinder<SkuState, TData> binder, IMapper mapper,
            States.Models.SkuFlowType flowType
        ) =>
            binder.Then(context =>
            {
                _ = mapper.Map(context.Data, context.Instance);
                context.Instance.FlowType = flowType;
                context.Instance.CreatedDate = context.Instance.LastUpdatedDate = DateTime.Now;
            });

        public static EventActivityBinder<SkuState> SendSkuForPersistence(this EventActivityBinder<SkuState> binder) =>
            binder.SendAsync(context => context.Init<SagaMessages.Persistence.UpsertSku>(
                new
                {
                    SupplierSku = context.Instance.SupplierSku,
                    CategorizedData = context.Instance?.CategorizedData,
                    EnrichedData = context.Instance?.EnrichedData
                })
            );

        public static EventActivityBinder<SkuState> SendSkuForEnrichment(this EventActivityBinder<SkuState> binder, IMapper mapper) =>
            binder.SendAsync(context => context.Init<SagaMessages.Enrichment.SendSkuForEnrichment>(
                mapper.Map<SagaMessages.Enrichment.SendSkuForEnrichment>(context.Instance)
            ));

        public static EventActivityBinder<SkuState> SendSkuToSearchIndex(this EventActivityBinder<SkuState> binder, IMapper mapper) =>
            binder.SendAsync(context => context.Init<SagaMessages.Search.SendSkuToSearchIndex>(
                mapper.Map<SagaMessages.Search.SendSkuToSearchIndex>(context.Instance)
            ));

        public static EventActivityBinder<SkuState> SendSkuToGetAvailabilityForAllContracts(this EventActivityBinder<SkuState> binder) =>
            binder.SendAsync(context => context.Init<AvailabilityMessages.Search.GetAvailabilityForAllContracts>(
                new
                {
                    SupplierId = context.Instance.SupplierSku.SupplierId,
                    SupplierSkuId = context.Instance.SupplierSku.SkuId,
                    PersistedSkuId = context.Instance.PersistedSku.SkuId
                })
            );

        public static EventActivityBinder<SkuState> SendSkuToCategorization(this EventActivityBinder<SkuState> binder, IMapper mapper) =>
            binder.SendAsync(context => context.Init<SagaMessages.Categorization.CategorizeSku>(
                mapper.Map<SagaMessages.Categorization.CategorizeSku>(context.Instance)
            ));

        public static EventActivityBinder<SkuState> SendSkuToRemoveFromEnrichment(this EventActivityBinder<SkuState> binder, IMapper mapper) =>
            binder.SendSkuForEnrichment(mapper);

        public static EventActivityBinder<SkuState> SendSkuToRemoveFromPersistence(this EventActivityBinder<SkuState> binder, IMapper mapper) =>
            binder.SendAsync(context => context.Init<SagaMessages.Persistence.RemoveSku>(
                mapper.Map<SagaMessages.Persistence.RemoveSku>(context.Instance)
            ));

        public static EventActivityBinder<SkuState> SendSkuToRemoveFromAvailability(this EventActivityBinder<SkuState> binder, IMapper mapper) =>
            binder.SendAsync(context => context.Init<AvailabilityMessages.Manager.RemoveSku>(
                mapper.Map<AvailabilityMessages.Manager.RemoveSku>(context.Instance)
            ));

        public static EventActivityBinder<SkuState> SendSkuToRemoveSkuFromSearchIndex(
            this EventActivityBinder<SkuState> binder
            , IMapper mapper
        ) =>
            binder.SendAsync(context => context.Init<SagaMessages.Search.RemoveSkuFromSearchIndex>(
                mapper.Map<SagaMessages.Search.RemoveSkuFromSearchIndex>(context.Instance)
            ));
    }
}
