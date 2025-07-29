using System.Text.RegularExpressions;

namespace Business.Entities {
    public class DogName {
        private static readonly Regex AllowedPattern = new(@"^[a-zA-Z]+(?:[ '-][a-zA-Z]+)*$");
        private readonly string _value;

        private DogName(string value) {
            _value = value;
        }

        public static DogName Create(string dogName) {
            if (string.IsNullOrWhiteSpace(dogName))
                throw new ArgumentException("Dog name cannot be empty.", nameof(dogName));

            dogName = dogName.Trim();

            if (!AllowedPattern.IsMatch(dogName))
                throw new ArgumentException("Name can only contain letters, spaces, apostrophes and '-'. It must start and end with a letter.", nameof(dogName));

            return new DogName(dogName);
        }

        public override string ToString() => _value;
    }
}
