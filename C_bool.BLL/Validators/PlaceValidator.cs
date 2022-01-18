
namespace C_bool.BLL.Validators
{
    public static class PlaceValidator
    {
        private static readonly string _standardValidationMessage =
            "[ERROR] Nieprawidłowa wartość! Czy chcesz spróbować jeszcze raz?";

        public static bool ValidateLatitude(string input, out string message)
        {
            if (double.TryParse(input.Replace(',', '.'), out double latitude) && latitude is >= -90 and <= 90)
            {
                message = "";
                return true;
            }
            else
            {
                message = _standardValidationMessage;
                return false;
            }
        }

        public static bool ValidateLongitude(string input, out string message)
        {
            if (double.TryParse(input.Replace(',', '.'), out double longitude) && longitude is >= -180 and <= 180)
            {
                message = "";
                return true;
            }
            else
            {
                message = _standardValidationMessage;
                return false;
            }
        }

        public static bool ValidateRadius(string input, out string message)
        {
            if (double.TryParse(input.Replace(',', '.'), out double radius))
            {
                message = "";
                return true;
            }
            else
            {
                message = _standardValidationMessage;
                return false;
            }
        }
    }
}