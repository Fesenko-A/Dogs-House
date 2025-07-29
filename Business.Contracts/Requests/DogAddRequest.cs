namespace Business.Contracts.Requests {
    public record DogAddRequest(string Name, string Color, uint TailLength, uint Weight);
}
