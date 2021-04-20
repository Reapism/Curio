namespace Curio.SharedKernel.Constants
{
    public class EnvironmentConstants
    {
        public const string Development = nameof(Development);
        ///<summary>Staging environment before code ships normally to production</summary>
        public const string Staging = nameof(Staging);
        ///<summary>Beta environment before code ships normally to production
        /// which is made available to some users</summary>
        public const string Beta = nameof(Beta);
        public const string Production = nameof(Production);
    }
}
