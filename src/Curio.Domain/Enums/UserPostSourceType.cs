using System.ComponentModel;

namespace Curio.Core.Enums
{
    /// <summary>
    /// The source originating in the creation of a specific post.
    /// </summary>
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
