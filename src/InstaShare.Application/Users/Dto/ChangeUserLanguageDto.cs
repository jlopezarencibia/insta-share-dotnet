using System.ComponentModel.DataAnnotations;

namespace InstaShare.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}