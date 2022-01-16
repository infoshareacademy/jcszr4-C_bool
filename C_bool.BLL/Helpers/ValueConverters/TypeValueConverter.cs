using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace C_bool.BLL.Helpers
{
    class TypeValueConverter : ValueConverter<string[], string>
    {
        public TypeValueConverter()
            : base((src) => To(src), dest => From(dest)) { }

        private static string To(string[] src)
        {
            if (src is null || src.Length == 0)
            {
                return null;
            }

            return string.Join(";", src);
        }
        private static string[] From(string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return null;
            }

            return src.Split(";");
        }
    }
}
