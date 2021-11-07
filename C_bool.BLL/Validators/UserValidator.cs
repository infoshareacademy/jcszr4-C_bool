namespace C_bool.BLL.Validators
{
    public static class UserValidator
    {
        private static readonly string standardValidationMessage =
            "[ERROR] Nieprawidłowa wartość! Czy chcesz spróbować jeszcze raz?";
        
        public static bool ValidatePoints(string input, out string message)
        {
            if (int.TryParse(input, out int points) && points >= 0)
            {
                message = "";
                return true;
            }
            else
            {
                message = standardValidationMessage;
                return false;
            }
        }

        public static bool ValidatePointsRange(int min, int max, out string message)
        {
            if (min < max)
            {
                message = "";
                return true;
            }
            else
            {
                message = "[ERROR] Wartość minimalna jest większa od wartości maksymalnej! Spróbuj jeszcze raz.";
                return false;
            }
        } 
        
        
    }
}