using ClosedXML.Excel;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Excel;
using System.Reflection;

namespace ProdutosReactAPI.Infraestrutura.Excel
{
    public class ExcelService : IExcelService
    {
        public byte[] GerarExcel<T>(IEnumerable<T> dados, string nomePlanilha = "Dados")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(nomePlanilha);

            var propriedades = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Cabeçalhos
            for (int i = 0; i < propriedades.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = propriedades[i].Name;
            }

            // Dados
            int linha = 2;
            foreach (var item in dados)
            {
                for (int col = 0; col < propriedades.Length; col++)
                {
                    var valor = propriedades[col].GetValue(item);
                    worksheet.Cell(linha, col + 1).Value = valor != null ? XLCellValue.FromObject(valor) : XLCellValue.FromObject(string.Empty);
                }
                linha++;
            }


            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
