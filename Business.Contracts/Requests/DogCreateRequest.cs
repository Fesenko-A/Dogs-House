namespace Business.Contracts.Requests {
    public record DogCreateRequest(string Name, string Color, uint TailLength, uint Weight);
}
