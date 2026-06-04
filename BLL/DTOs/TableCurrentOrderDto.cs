namespace BLL.DTOs
{
    public class TableCurrentOrderDto
    {
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? OrderStatus { get; set; }
        public bool IsPaid { get; set; }
        public List<MenuItemDto> Items { get; set; } = [];
    }

    public class MenuItemDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class MostOrderedMenuItemDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int TotalQuantitySold { get; set; }
    }
}