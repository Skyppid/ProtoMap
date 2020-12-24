using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProtoMap.Core.PluginSystem;
using Serilog;

namespace ProtoMap.Core.ProjectSystem.Internal
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A simple meta data implementation.  </summary>
    ///
    /// <seealso cref="T:ProtoMap.Core.ProjectSystem.ProjectMetadataBase"/>
    ///-------------------------------------------------------------------------------------------------
    internal sealed class BasicProjectMetaData : ProjectMetadataBase
    {
        private readonly ILogger _logger;
        private readonly IPluginHost _host;
        private readonly object _lock = new();
        private readonly List<MetaData> _dataList = new();

        private static readonly Type[] SupportedTypes = new[]
        {
            typeof(byte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long),
            typeof(float), typeof(double), typeof(decimal)
        };

        public BasicProjectMetaData(ILogger logger, IPluginHost host)
        {
            _logger = logger;
            _host = host;
        }

        /// <inheritdoc />
        public override void RegisterMetadata(string key, Type dataType, PluginIdentityToken token, object defaultValue)
        {
            EnsureDataTypeIntegrity(dataType);

            lock (_lock)
            {
                if (_dataList.Any(r => r.Key == key || r.Owner == token))
                    throw new InvalidOperationException(
                        "Another registration with the same ID already exists or it was already registered by this plugin.");
                _dataList.Add(new MetaData(key, token, defaultValue, dataType));
            }
        }

        /// <inheritdoc />
        internal override void RegisterMetadata(string key, Type dataType, object defaultValue)
        {
            EnsureDataTypeIntegrity(dataType);

            lock (_lock)
            {
                if (_dataList.Any(r => r.Key == key))
                    throw new InvalidOperationException("Data with the same ID is already registered.");
                _dataList.Add(new MetaData(key, null, defaultValue, dataType));
            }
        }

        /// <inheritdoc />
        public override bool DeregisterAllMetadata(PluginIdentityToken token)
        {
            lock (_lock)
            {
                try
                {
                    var selection = _dataList.Where(d => d.Owner == token);
                    foreach (var data in selection)
                        _dataList.Remove(data);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Failed to deregister all metadata for plugin {Id} ({Name})",
                        _host.ResolveIdentityToGuid(token), _host.ResolveIdentityToName(token));
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool DeregisterMetadata(string key, PluginIdentityToken token)
        {
            lock (_lock)
            {
                try
                {
                    var selected = _dataList.FirstOrDefault(d => d.Key == key && d.Owner == token);
                    if (selected != null)
                        return _dataList.Remove(selected);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Failed to deregister metadata {Key} for plugin {Id} ({Name})",
                        key, _host.ResolveIdentityToGuid(token), _host.ResolveIdentityToName(token));
                    return false;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public override bool IsSupportedDataType<T>() => CheckTypeSupport(typeof(T));

        /// <inheritdoc />
        public override bool IsSupportedDataType(Type type) => CheckTypeSupport(type);

        public override bool Set<T>(string key, T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            lock (_lock)
            {
                var existing = _dataList.FirstOrDefault(d => d.Key == key);
                if (existing != null)
                {
                    existing = existing with { Value = value };
                    return true;
                }

                return false;
            }
        }

        public override bool Get<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Checks whether the type is supported. </summary>
        ///
        /// <param name="type"> The type.   </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        private bool CheckTypeSupport(Type type)
        {
            if (type == typeof(string)) return true;
            if (type.IsValueType && SupportedTypes.Any(s => s == type))
                return true;
            return type.GetCustomAttribute<SerializableAttribute>() != null;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Ensures data type integrity. Throws an <see cref="NotSupportedException"/> if unsupported.
        /// </summary>
        ///
        /// <exception cref="NotSupportedException">    Thrown when the requested operation is not
        ///                                             supported.  </exception>
        ///
        /// <param name="dataType"> Type of the data.   </param>
        ///
        /// <seealso cref="M:ProtoMap.Core.ProjectSystem.Internal.BasicProjectMetaData.EnsureDataTypeIntegrity(Type)"/>
        ///-------------------------------------------------------------------------------------------------
        private void EnsureDataTypeIntegrity(Type dataType)
        {
            if (!CheckTypeSupport(dataType))
                throw new NotSupportedException(
                    $"Type {dataType.Name} is not a supported by this metadata implementation.");
        }

        /// <summary>   Values that represent data complexities.    </summary>
        private enum DataComplexity
        {
            /// <summary>   Represents simple data (basic CLR types).    </summary>
            Simple,
            /// <summary>   Represents complex data (non-basic CLR structs or classes).   </summary>
            Complex
        }

        private sealed record MetaData(string Key, PluginIdentityToken? Owner, object Value, Type DataType)
        {
        }
    }
}
