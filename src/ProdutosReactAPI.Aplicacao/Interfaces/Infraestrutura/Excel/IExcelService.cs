namespace ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Excel
{
    public interface IExcelService
    {
        byte[] GerarExcel<T>(IEnumerable<T> dados, string nomePlanilha = "Dados");
    }
}
