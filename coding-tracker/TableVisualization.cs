using ConsoleTableExt;
internal class TableVisualization
{
    internal static void ShowTable<T>(List<T> tableData) where T : class
    {
        System.Console.WriteLine("\n\n");

        ConsoleTableBuilder.From(tableData).WithTitle("Coding").ExportAndWriteLine();
        System.Console.WriteLine("\n\n");
    }
}