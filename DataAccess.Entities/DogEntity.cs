namespace DataAccess.Entities {
    public class DogEntity {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required int TailLength { get; set; }
        public required int Weigth { get; set; }
    }
}
