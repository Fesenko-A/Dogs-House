namespace Shared.Exceptions {
    public class AlreadyExistsException : Exception {
        public AlreadyExistsException(Type type) : base($"{type} already exists.") { }
    }
}
