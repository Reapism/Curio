namespace Curio.SharedKernel.Interfaces
{
    public class Validation
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public ValidationType ValidationType { get; set; }
    }

    public enum ValidationType
    {
        Warning,
        Failure,

    }
}