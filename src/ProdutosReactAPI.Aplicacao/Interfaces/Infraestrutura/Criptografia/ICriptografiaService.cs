namespace ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Criptografia
{
    public interface ICriptografiaService
    {
        string Hash(string senha);
        bool Verificar(string senha, string hash);
    }
}
