using Invoice_Logic.Excel;

namespace Invoice_Logic.Core.Objects;

public static class ExcelInvoiceTemplate
{
    public static Cell CellCustomer => new Cell(row: 2, col: 2);
    public static Cell CellDate => new Cell(row: 3, col: 2);
    public static Cell CellDesc => new Cell(row: 4, col: 2);
    public static int RowStart => 7;
    public static int ColumnCustItemCode => 1;
    public static int ColumnCustItemDesc => 2;
    public static int ColumnCustRate => 3;
    public static int ColumnCases => 4;
}
