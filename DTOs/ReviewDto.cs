namespace Dynasy.DTOs; 

// ReviewDto.cs
public class ReviewDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateReviewDto
{
    public string Content { get; set; }
    public int Rating { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
}

public class UpdateReviewDto
{
    public string Content { get; set; }
    public int Rating { get; set; }
}