
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TradeZeroProcessor.Utilities
{
    public class Exporter
    {
        public void ExportToExcel(List<Trade> trades, string filePath)
        {
            // Create a new spreadsheet document.
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                // Add a workbook part to the document.
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Add a worksheet part to the workbook part.
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add a sheet to the workbook.
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Trades" };
                sheets.Append(sheet);

                // Create the header row.
                Row headerRow = new Row();
                headerRow.Append(CreateCell("Account"));
                headerRow.Append(CreateCell("Trade Date"));
                headerRow.Append(CreateCell("Settlement Date"));
                headerRow.Append(CreateCell("Symbol"));
                headerRow.Append(CreateCell("Quantity"));
                headerRow.Append(CreateCell("Price"));
                headerRow.Append(CreateCell("Commission"));
                headerRow.Append(CreateCell("Gross Proceeds"));
                headerRow.Append(CreateCell("Net Proceeds"));
                worksheetPart.Worksheet.GetFirstChild<SheetData>().AppendChild(headerRow);

                // Add the trades to the worksheet.
                foreach (Trade trade in trades)
                {
                    Row dataRow = new Row();
                    dataRow.Append(CreateCell(trade.Account));
                    dataRow.Append(CreateCell(trade.TradeDate.ToShortDateString()));
                    dataRow.Append(CreateCell(trade.SettlementDate.ToShortDateString()));
                    dataRow.Append(CreateCell(trade.Symbol));
                    dataRow.Append(CreateCell(trade.Quantity));
                    dataRow.Append(CreateCell(trade.Price, "#,##0.00"));
                    dataRow.Append(CreateCell(trade.Commission, "#,##0.00"));
                    dataRow.Append(CreateCell(trade.GrossProceeds, "#,##0.00"));
                    dataRow.Append(CreateCell(trade.NetProceeds, "#,##0.00"));
                    worksheetPart.Worksheet.GetFirstChild<SheetData>().AppendChild(dataRow);
                }

                // Save the document.
                workbookPart.Workbook.Save();
            }
        }

        // Helper function to create a new cell with a string value.
        private Cell CreateCell(string value)
        {
            return new Cell(new InlineString(new Text(value)));
        }

        // Helper function to create a new cell with a numeric value.
        private Cell CreateCell(decimal value, string format)
        {
            return new Cell(new CellValue(value.ToString(format)), new CellFormat() { NumberFormatId = 4 });
        }

        // Helper function to create a new cell with an integer value.
        private Cell CreateCell(int value)
        {
            return new Cell(new CellValue(value.ToString()));
        }
        //    public void ExportToExcel(List<Trade> trades, string filePath)
        //    {
        //        // Create a new spreadsheet document.
        //        using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
        //        {
        //            // Add a workbook part to the document.
        //            WorkbookPart workbookPart = document.AddWorkbookPart();
        //            workbookPart.Workbook = new Workbook();

        //            // Add a worksheet part to the workbook part.
        //            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        //            worksheetPart.Worksheet = new Worksheet(new SheetData());

        //            // Add a sheet to the workbook.
        //            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
        //            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Trades" };
        //            sheets.Append(sheet);

        //            // Create the header row.
        //            Row headerRow = new Row();
        //            headerRow.Append(CreateCell("Account"));
        //            headerRow.Append(CreateCell("Trade Date"));
        //            headerRow.Append(CreateCell("Settlement Date"));
        //            headerRow.Append(CreateCell("Symbol"));
        //            headerRow.Append(CreateCell("Quantity"));
        //            headerRow.Append(CreateCell("Price"));
        //            headerRow.Append(CreateCell("Commission"));
        //            headerRow.Append(CreateCell("Gross Proceeds"));
        //            headerRow.Append(CreateCell("Net Proceeds"));
        //            worksheetPart.Worksheet.GetFirstChild<SheetData>().AppendChild(headerRow);

        //            // Add the trades to the worksheet.
        //            foreach (Trade trade in trades)
        //            {
        //                Row dataRow = new Row();
        //                dataRow.Append(CreateCell(trade.Account));
        //                dataRow.Append(CreateCell(trade.TradeDate.ToShortDateString()));
        //                dataRow.Append(CreateCell(trade.SettlementDate.ToShortDateString()));
        //                dataRow.Append(CreateCell(trade.Symbol));
        //                dataRow.Append(CreateCell(trade.Quantity));
        //                dataRow.Append(CreateCell(trade.Price));
        //                dataRow.Append(CreateCell(trade.Commission));
        //                dataRow.Append(CreateCell(trade.GrossProceeds));
        //                dataRow.Append(CreateCell(trade.NetProceeds));
        //                worksheetPart.Worksheet.GetFirstChild<SheetData>().AppendChild(dataRow);
        //            }

        //            // Save the document.
        //            workbookPart.Workbook.Save();
        //        }
        //    }

        //    // Helper function to create a new cell with a string value.
        //    private Cell CreateCell(string value)
        //    {
        //        return new Cell(new InlineString(new DocumentFormat.OpenXml.Drawing.Text(value)));
        //    }

        //    // Helper function to create a new cell with a numeric value.
        //    private Cell CreateCell(decimal value)
        //    {
        //        return new Cell(new CellValue(value.ToString()));
        //    }

        //    // Helper function to create a new cell with an integer value.
        //    private Cell CreateCell(int value)
        //    {
        //        return new Cell(new CellValue(value.ToString()));
        //    }
    }
}
