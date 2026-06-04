namespace BLL.DTOs
{
    public class UpdateOrderDto
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public string? Status { get; set; }

        public List<UpdateOrderItemDto>? OrderItems { get; set; }
    }

    public class UpdateOrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}