using CasaDoCodigo.Models;

public interface ICadastroRepository
{
    Cadastro UpdateCadastro(int cadastroId, Cadastro novoCadastro);
}