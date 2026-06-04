namespace DAL.Models
{
    public class StockItem
    {
        public int StockItemId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }
    }
}
