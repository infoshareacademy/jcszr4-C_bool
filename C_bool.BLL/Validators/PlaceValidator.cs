
namespace C_bool.BLL.Validators
{
    public static class PlaceValidator
    {
        private static readonly string _standardValidationMessage =
            "[ERROR] Nieprawidłowa wartość! Czy chcesz spróbować jeszcze raz?";

        public static bool ValidateLatitude(string input, out string message)
        {
            if (double.TryParse(input.Replace(',', '.'), out double latitude) && latitude >= -90 && latitude <= 90)
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
            if (double.TryParse(input.Replace(',', '.'), out double longitude) && longitude >= -180 && longitude <= 180)
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