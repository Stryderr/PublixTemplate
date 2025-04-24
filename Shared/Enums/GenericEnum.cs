using System.ComponentModel;

namespace Shared.Enums
{
    public enum GenericEnum
    {
        [AmbientValue("GenericValue")]
        GenericValue,

        [AmbientValue("Default")]
        Default
    }
}
