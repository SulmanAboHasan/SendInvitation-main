namespace DemoUsersManagementCommandSide.Domain
{
    public record Permission(    
       bool Transfer,
       bool PurchaseCards,
       bool ManageDevices
    );
}
