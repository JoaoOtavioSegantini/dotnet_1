using api.Dtos.Comment;
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
    private readonly IStockRepository _stockRepo;

    public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
    {
      _commentRepo = commentRepo;
      _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {

      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      var comments = await _commentRepo.GetAllAsync();
      var commentDto = comments.Select(x => x.ToCommentDto());
      return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {

      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      var comment = await _commentRepo.GetByIdAsync(id);

      if (comment is null)
      {
        return NotFound();
      }

      return Ok(comment.ToCommentDto());

    }

    [HttpPost("{stockId:int}")]
    public async Task<ActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
    {

      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      if (!await _stockRepo.StocksExists(stockId))
      {
        return BadRequest("Stock does not exists");
      }

      var commentModel = commentDto.ToCommentFromCreate(stockId);
      await _commentRepo.CreateAsync(commentModel);

      return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
    {

      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      var comment = await _commentRepo.UpdateAsync(id, commentDto.ToCommentFromUpdate()); 
      if (comment == null)
      {
        return NotFound("Comment not found");
      }

      return Ok(comment.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {

      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      var commentModel = await _commentRepo.DeleteAsync(id);
      if (commentModel is null)
      {
        return NotFound("Comment does not exists");
      }
      return Ok(commentModel);
    }

  }
}
