namespace BLL.DTOs
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        public List<CreateOrderItemDto>? OrderItems { get; set; }
        public CreateCustomerDto? CustomerDto { get; set; }
    }

    public class CreateOrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateCustomerDto
    {
        public string FullName { get; set; } = "";
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
