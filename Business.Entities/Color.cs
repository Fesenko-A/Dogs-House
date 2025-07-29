using System.Text.RegularExpressions;

namespace Business.Entities {
    public sealed class Color {
        private static readonly Regex AllowedPattern = new(@"^[a-zA-Z&\-]+$");
        private readonly string _value;

        private Color(string value) {
            _value = value;
        }

        public static Color Create(string colorName) {
            if (string.IsNullOrWhiteSpace(colorName))
                throw new ArgumentException("Color cannot be empty.", nameof(colorName));

            colorName = colorName.Trim();

            if (!AllowedPattern.IsMatch(colorName))
                throw new ArgumentException("Color can only contain letters, '&' and '-'. Numbers and other symbols are not allowed.", nameof(colorName));

            return new Color(colorName);
        }

        public override string ToString() => _value;
    }
}
