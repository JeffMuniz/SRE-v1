using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Persistence.Mongo.Configurations;
using Shared.Persistence.Mongo.Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Shared.Persistence.Mongo
{
    public abstract class MongoContext<TContext, TOptions> : IMongoContext<TOptions>, IDisposable
        where TContext : class, IMongoContext
        where TOptions : class, IMongoOptions
    {
        private readonly IDictionary<Type, string> _collectionNameCache = new ConcurrentDictionary<Type, string>();
        private readonly IDisposable _listener;
        private Lazy<TOptions> _options;
        private Lazy<IMongoDatabase> _database;

        protected virtual string ContextName { get; } =
            NameProvider.GetContextName<TContext>();

        public virtual TOptions Options =>
            _options?.Value;

        public virtual IMongoDatabase Database =>
            _database?.Value;

        protected MongoContext(IOptionsMonitor<TOptions> options)
        {
            _options = new Lazy<TOptions>(() => options.Get(ContextName));
            _database = new Lazy<IMongoDatabase>(() => NewDatabaseConnection());

            _listener = options.OnChange((settings, name) =>
            {
                if (name != ContextName)
                    return;

                _options = new Lazy<TOptions>(() => settings);
                _database = new Lazy<IMongoDatabase>(() => NewDatabaseConnection());
                _collectionNameCache.Clear();
            });
        }

        protected IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName = null, bool? ignorePrefix = null, MongoCollectionSettings settings = null) =>
            Database.GetCollection<TCollection>(GetFormattedCollectionName<TCollection>(collectionName, ignorePrefix.GetValueOrDefault()), settings);

        private IMongoDatabase NewDatabaseConnection()
        {
            var clientSettings = MongoClientSettings.FromConnectionString(Options.ConnectionString);

            clientSettings.ApplicationName = Options.ApplicationName ?? clientSettings.ApplicationName;
            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(Options.DatabaseName);

            return database;
        }

        private string GetFormattedCollectionName<TCollection>(string collectionName, bool ignorePrefix)
        {
            var collectionType = typeof(TCollection);
            if (_collectionNameCache.TryGetValue(collectionType, out var cachedCollectionName))
                return cachedCollectionName;

            var finalCollectionName = string.IsNullOrWhiteSpace(collectionName)
                ? typeof(TCollection).Name.Trim().Camelize()
                : collectionName.Trim();
            var finalCollectionPrefix = ignorePrefix
                ? null
                : Options.CollectionPrefix?.Trim();
            var formattedCollectionName = $"{finalCollectionPrefix}{finalCollectionName}";

            _collectionNameCache[collectionType] = formattedCollectionName;

            return formattedCollectionName;
        }

        #region [Dispose]

        protected bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _collectionNameCache.Clear();
                _listener.Dispose();
            }

            _options = default;
            _database = default;
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MongoContext()
        {
            Dispose(false);
        }

        #endregion [Dispose]
    }
}
