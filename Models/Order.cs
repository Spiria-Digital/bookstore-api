namespace BookStoreApi.Models
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerID { get; set; }
    public string? ShippingAddress { get; set; }
    public required string OrderStatus { get; set; }
    public List<OrderItem>? OrderItems { get; set; }

    public Order()
    {
      OrderItems = [];
    }
  }
}