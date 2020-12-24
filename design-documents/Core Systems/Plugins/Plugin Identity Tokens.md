# Identity Tokens

In order for subsystems to know which plugin exposes which registered functionality and furthermore to ensure even improperly designed plugins can be unloaded completely, a token is generated for each plugin that's loaded which identifies any registration with it.



When the application notifies a plugin to unload itself and it fails to do so, all subsystems will be informed to force-release any registrations made by the given token (plugin) to ensure that the `ApplicationLoadContext` can successfully unload the assembly.



# Generation

The identity token is a `struct` and thus should not store more than 16 bytes as it becomes expensive when being copied. Since plugin registrations occur only in small amounts (usually less than 20 plugins) the range of 4 bytes is more than sufficient. The token is defined as `ushort` and represents the *CRC-16* value of the plugins ID (which is defined as a `Guid`). Since the ID is a constant but unique value the token can be regenerated multiple times with the same result.



# Usage

The token exposes an implict conversion to `ushort` and can be compared with either another token or an `ushort` value. It cannot be constructed other than using the exposed factory method `CreateFor(IPlugin)` which requires a plugin to be provided.



Subsystems must take registrations by token *publicly* and without *internally*.

