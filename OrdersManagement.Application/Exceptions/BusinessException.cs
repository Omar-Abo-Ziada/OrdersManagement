namespace OrdersManagement.Application.Exceptions
{
    public enum BusinessExceptionType
    {
        // General (0+)
        None = 0,

        // Orders Module (1000+)
        Orders_InsufficientStock = 1001,
        Orders_OrderAlreadyShipped = 1002,
        Orders_OrderAlreadyCancelled = 1003,
        Orders_InvalidOrderState = 1004,
        Orders_InvalidOrderItem = 1005,
        Orders_OrderNotFound = 1006,

        // Payments Module (2000+)
        Payments_PaymentFailed = 2001,
        Payments_RefundFailed = 2002,
        Payments_InvalidPaymentState = 2003,
        Payments_PaymentMethodNotSupported = 2004,

        // Inventory Module (3000+)
        Inventory_ProductNotFound = 3001,
        Inventory_StockUpdateFailed = 3002,
        Inventory_StockBelowThreshold = 3003,

        // Product Catalog Module (4000+)
        ProductCatalog_ProductNotFound = 4001,
        ProductCatalog_CategoryNotFound = 4002,
        ProductCatalog_InvalidProductVariant = 4003,

        // Shipping & Delivery Module (5000+)
        Shipping_InvalidAddress = 5001,
        Shipping_ShipmentAlreadyDispatched = 5002,
        Shipping_DeliveryAgentNotFound = 5003,

        // User & Role Management (6000+)
        Users_UserNotFound = 6001,
        Users_RoleNotFound = 6002,
        Users_InvalidCredentials = 6003,
        Users_UnauthorizedAccess = 6004,

        // Reporting & Analytics (7000+)
        Reporting_ReportGenerationFailed = 7001
    }

    public class BusinessException : Exception
    {
        public BusinessExceptionType Type { get; }

        public BusinessException(BusinessExceptionType type, string message)
            : base(message)
        {
            Type = type;
        }
    }
}