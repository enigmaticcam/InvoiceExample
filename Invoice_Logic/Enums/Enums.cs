namespace Invoice_Logic.Enums;

public enum enumStatusType
{
    Draft = 1,
    Approved,
    Finished
}

public enum enumnResultStatusType
{
    New = 0,
    Pass,
    ItemCodeLookupFail,
    InvalidRate
}