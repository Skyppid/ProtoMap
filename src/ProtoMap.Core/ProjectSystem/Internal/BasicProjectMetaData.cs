using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ImTools;

namespace ProtoMap.Core.ProjectSystem.Internal
{
    /// <summary>
    /// A simple meta data implementation.
    /// </summary>
    internal sealed class BasicProjectMetaData : ProjectMetadataBase
    {
        private readonly Dictionary<string, object> _valueDict = new Dictionary<string, object>();

        /// <summary>   Values that represent data complexities.    </summary>
        private enum DataComplexity
        {
            /// <summary>   Represents simple data (basic CLR types).    </summary>
            Simple,
            /// <summary>   Represents complex data (non-basic CLR structs or classes).   </summary>
            Complex
        }

        public override void RegisterMetadata(string key, Type dataType, object defaultValue)
        {
            throw new NotImplementedException();
        }

        public override bool IsSupportedDataType<T>()
        {
            throw new NotImplementedException();
        }

        public override bool IsSupportedDataType(Type type)
        {
            throw new NotImplementedException();
        }

        public override bool Set<T>(string key, T value, bool replace = false)
        {
            throw new NotImplementedException();
        }

        public override bool Set(string key, object value, bool replace = false)
        {
            throw new NotImplementedException();
        }

        public override bool Get<T>(string key, out T value)
        {
            throw new NotImplementedException();
        }

        public override bool Get(string key, out object value)
        {
            throw new NotImplementedException();
        }
    }
}
