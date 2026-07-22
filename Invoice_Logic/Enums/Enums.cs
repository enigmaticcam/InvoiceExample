namespace Invoice_Logic.Enums;

public enum enumInvoiceActionType
{
    Delete,
    RefreshResults
}

public enum enumnResultStatusType
{
    New = 0,
    Pass,
    ItemCodeLookupFail,
    InvalidRate
}

public enum enumStatusType
{
    Draft = 1,
    Approved,
    Finished
}