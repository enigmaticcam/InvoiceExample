namespace Invoice_Logic.Data.DTOs;

public class InvoicePermissionsDTO
{
    public bool CanDelete { get; set; }
    public bool CanEdit { get; }
    public List<int> StatusChanges { get; set; } = new();
}
