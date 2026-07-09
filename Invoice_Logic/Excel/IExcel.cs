namespace Invoice_Logic.Excel;

public interface IExcel
{
    DateTime GetCellValueAsDateTime(Cell cell);
    decimal GetCellValueAsDecimal(Cell cell);
    int GetCellValueAsInt32(Cell cell);
    string GetCellValueAsString(Cell cell);
    List<string> GetWorksheetNames();
    void OpenFile(string fileName);
    void SelectWorksheet(string name);
}

public class Cell
{
    public Cell() { }
    public Cell(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public int Row { get; set; }
    public int Col { get; set; }
}
