using System;

namespace ProtoMap.Core.PluginSystem
{
    /// <summary>
    /// A host for third-party extensions.
    /// </summary>
    public interface IPluginHost
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Resolves a <see cref="PluginIdentityToken"/> to the plugin's name.   </summary>
        ///
        /// <param name="token">    The token.  </param>
        ///
        /// <returns>   A string.   </returns>
        ///-------------------------------------------------------------------------------------------------
        string ResolveIdentityToName(PluginIdentityToken token);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Resolves a <see cref="PluginIdentityToken"/> to the plugin's identifier.  </summary>
        ///
        /// <param name="token">    The token.  </param>
        ///
        /// <returns>   A GUID. </returns>
        ///-------------------------------------------------------------------------------------------------
        Guid ResolveIdentityToGuid(PluginIdentityToken token);
    }
}
