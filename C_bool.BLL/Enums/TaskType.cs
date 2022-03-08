using System.ComponentModel.DataAnnotations;

namespace C_bool.BLL.Enums
{
    public enum MessageType
    {
        [Display(Name = "Komentarz")]
        Comment,
        [Display(Name = "Alert")]
        Alert,
        [Display(Name = "Prośba")]
        SubmissionRequest,
        [Display(Name = "Zaliczone")]
        SubmissionApproval
    }
}
