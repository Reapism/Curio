using System.ComponentModel;

namespace Curio.Core.Enums
{
    public enum UserPostSourceType
    {
        [Description("Curio Web")]
        WebApp,
        [Description("Curio Android")]
        Android,
        [Description("Curio iPhone")]
        IPhone,
        [Description("Curio API")]
        CurioApi,
        [Description("Promoted")]
        Advertisment
    }
}
