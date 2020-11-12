using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ProtoMap.Core.ProjectSystem
{
    /// <summary>   Base class which exposes project meta data of the project.  </summary>
    public abstract class ProjectMetadataBase
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Registers a metadata field. This is required for the underlying implementation to know
        ///     which data could be available and in which form.
        /// </summary>
        ///
        /// <param name="key">          The key.    </param>
        /// <param name="dataType">     Type of the data.   </param>
        /// <param name="defaultValue"> The default value.  </param>
        ///
        /// <seealso cref="M:ProtoMap.Core.ProjectSystem.ProjectMetadataBase.RegisterMetadata(string,Type,object)"/>
        ///-------------------------------------------------------------------------------------------------
        public abstract void RegisterMetadata(string key, Type dataType, object defaultValue);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Query if <typeparamref name="T"/> is a supported data type.  </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   True if supported data type, false if not.  </returns>
        ///-------------------------------------------------------------------------------------------------
        public abstract bool IsSupportedDataType<T>();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Query if <paramref name="type"/> is a supported data type.  </summary>
        ///
        /// <param name="type"> The type.   </param>
        ///
        /// <returns>   True if supported data type, false if not.  </returns>
        ///
        /// <seealso cref="M:ProtoMap.Core.ProjectSystem.ProjectMetadataBase.IsSupportedDataType(Type)"/>
        ///-------------------------------------------------------------------------------------------------
        public abstract bool IsSupportedDataType(Type type);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the registered metadata field with the given value.    </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="key">      The key.    </param>
        /// <param name="value">    The value.  </param>
        /// <param name="replace">  (Optional) True to replace if already existing. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:ProtoMap.Core.ProjectSystem.ProjectMetadataBase.Set{T}(string,T,bool)"/>
        ///-------------------------------------------------------------------------------------------------
        public abstract bool Set<T>(string key, T value, bool replace = false);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets the registered metadata field with the given value.    </summary>
        ///
        /// <param name="key">      The key.    </param>
        /// <param name="value">    The value.  </param>
        /// <param name="replace">  (Optional) True to replace if already existing. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:ProtoMap.Core.ProjectSystem.ProjectMetadataBase.Set(string,object,bool)"/>
        ///-------------------------------------------------------------------------------------------------
        public abstract bool Set(string key, object value, bool replace = false);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the value from the registered metadata field.   </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="key">      The key.    </param>
        /// <param name="value">    [out] The value.    </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public abstract bool Get<T>(string key, out T value);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the value from the registered metadata field.  </summary>
        ///
        /// <param name="key">      The key.    </param>
        /// <param name="value">    [out] The value.    </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public abstract bool Get(string key, out object value);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Copies the meta data into another meta data object. </summary>
        ///
        /// <exception cref="NotSupportedException">    Thrown when the requested operation is not
        ///                                             supported by the implementation.  </exception>
        ///
        /// <param name="meta"> The meta data to copy.  </param>
        ///
        /// <seealso cref="M:ProtoMap.Core.ProjectSystem.ProjectMetadataBase.CopyTo(ProjectMetadataBase)"/>
        ///-------------------------------------------------------------------------------------------------
        public virtual void CopyTo(ProjectMetadataBase meta)
        {
            throw new NotSupportedException("This metadata implementation does not support copying.");
        }
    }
}
