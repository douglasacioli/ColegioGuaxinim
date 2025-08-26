namespace ColegioGuaxinim.Application.Options
{
    public class ImportacaoAlunosOptions
    {
        public int DuracaoBloqueioSegundos { get; set; } = 300;
        public string Delimitador { get; set; } = "||";
        public string FormatoData { get; set; } = "dd/MM/yyyy";
        public string Culture { get; set; } = "pt-BR";
    }
}
