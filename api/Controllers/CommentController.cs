using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/comment")]
  [ApiController]
  public class CommentController : ControllerBase
  {
    private readonly ICommentRepository _commentRepo;
    public CommentController(ICommentRepository commentRepo)
    {
        _commentRepo = commentRepo;
    }

    public async Task<ActionResult> GetAll()
    {
      var comments = await _commentRepo.GetAllAsync();
      var commentDto = comments.Select(x => x.ToCommentDto());
      return Ok(commentDto);
    }
  }
}
