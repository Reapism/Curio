namespace Curio.Persistence.Identity
{
    /// <summary>
    /// A basic end user stripped of sensitive account information.
    /// </summary>
    public interface ISessionUser
    {
        string Handle { get; init; }
        byte[] ProfilePicture { get; init; }
    }

    public class SessionUser : ISessionUser
    {
        /// <summary>
        /// Instantiates a session user with a handle of "Unknown".
        /// </summary>
        public SessionUser()
            : this("Unknown", null) { }
        public SessionUser(string handle, byte[] profilePicture)
        {
            Handle = handle;
            ProfilePicture = profilePicture;
        }
        public string Handle { get; init; }
        public byte[] ProfilePicture { get; init; }
    }
}
