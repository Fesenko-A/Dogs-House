namespace Shared.Exceptions {
    public class NotFoundException : Exception {
        public NotFoundException(Type type) : base($"{type.Name} was not found.") { }
    }
}
