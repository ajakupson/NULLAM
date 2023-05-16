using System.ComponentModel.DataAnnotations;

namespace NULLAM_RIK.Helpers
{
    public class ModelValidation
    {
        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
