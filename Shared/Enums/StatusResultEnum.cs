using System.ComponentModel;

namespace Shared.Enums
{
    public enum EnumOne
    {
        [AmbientValue("One")]
        One,

        [AmbientValue("Two")]
        Two,

        [AmbientValue("Three")]
        Three,

        [AmbientValue("Default")]
        Default
    }
}
