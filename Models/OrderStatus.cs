namespace Dynasy.Models
{
    public enum OrderStatus
    {
        Pending,       // Ожидает обработки
        Paid,          // Оплачен
        Shipped,       // Отправлен
        Delivered,     // Доставлен
        Canceled       // Отменен
    }
}