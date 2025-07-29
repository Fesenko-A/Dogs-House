namespace Shared.Exceptions {
    public class NotFoundException : Exception {
        public NotFoundException(Type type) : base($"{type} was not found.") { }
    }
}
