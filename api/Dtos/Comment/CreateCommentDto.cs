using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
  public class CreateCommentDto
  {

    [Required]
    [MinLength(5, ErrorMessage = "Title must be have 5 characters")]
    [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
    public String Title { get; set; } = string.Empty;

    [Required]
    [MinLength(5, ErrorMessage = "Content must be have 5 characters")]
    [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
    public String Content { get; set; } = string.Empty;

  }
}
