namespace Dynasy.DTOs; 

// ReviewDto.cs
public class ReviewDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int UserId { get; set; }
}