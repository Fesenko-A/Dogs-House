namespace Business.Entities {
    public class Dog {
        public int Id { get; init; }
        public Color Color { get; init; } = null!;
        public DogName Name { get; init; } = null!;
        public uint TailLength { get; init; }
        public uint Weight { get; init; }

        private Dog() { }

        public static Dog Create(Color color, DogName name, uint tailLength, uint weight) {
            if (tailLength > 100)
                throw new ArgumentException("Tail length cannot exceed 100 cm.", nameof(tailLength));
            if (weight > 100)
                throw new ArgumentException("Weight cannot exceed 100 kg.", nameof(weight));

            return new Dog {
                Color = color, 
                Name = name, 
                TailLength = tailLength, 
                Weight = weight
            };
        }
    }
}
