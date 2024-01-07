namespace Product.Messages.Events.Models;

public class BookModel
{
    public Guid Id { get; set; }
    public string Title { get;  set; }
    public string Author { get;  set; }
    public string Description { get;  set; }
    public int PageCount { get;  set; }
    public DateTime RegDate { get;  set; }
    public decimal Price { get;  set; }
    public decimal? PromotedPrice { get;  set; }
    public int Quantity { get;  set; }
    public bool IsHidden { get;  set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();


    public BookModel()
    {
        
    }
}