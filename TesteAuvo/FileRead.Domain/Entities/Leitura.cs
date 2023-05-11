namespace FileRead.Domain.Entities
{
    public class Leitura : BaseEntity<Guid>
    {
        public int DepartamentoId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double ValorHora { get; set; }
        public DateTime Data { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public DateTime SaidaAlmoco { get; set; }
        public DateTime VoltaAlmoco { get; set; }

        public Leitura(int departamentoId, int codigo, string nome, double valorHora, DateTime data, DateTime entrada, DateTime saida, DateTime saidaAlmoco, DateTime voltaAlmoco)
        {
            DepartamentoId = departamentoId;
            Codigo = codigo;
            Nome = nome;
            ValorHora = valorHora;
            Data = data;
            Entrada = entrada;
            Saida = saida;
            SaidaAlmoco = saidaAlmoco;
            VoltaAlmoco = voltaAlmoco;
        }
    }
}
