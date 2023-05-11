using FileRead.Domain.Entities;

namespace FileRead.Application.ViewModels
{
    public class FuncionarioViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double TotalReceber { get; set; }
        public double HorasExtras { get; set; }
        public double HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public int DiasTrabalhados { get; set; }

        private List<Leitura> Leituras { get; set; }

        public FuncionarioViewModel(int codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
            Leituras = new List<Leitura>();
        }

        public void AdicionarLeituras(IEnumerable<Leitura> leituras)
        {
            if (leituras.Any())
            {
                foreach (var leitura in leituras)
                {
                    Leituras.Add(leitura);

                    //Horas trabalhadas é hora de saída - hora de entrada - tempo de almoço
                    //Ex: 18:00 - 08:00 = 10:00 horas registradas
                    //    13:00 - 12:00 = 01:00 hora de almoço
                    //    10:00 - 01:00 = 09:00 horas trabalhadas
                    TimeSpan horasTrabalhadas = (leitura.Saida - leitura.Entrada) - (leitura.VoltaAlmoco - leitura.SaidaAlmoco);

                    //Horáio regular de trabalho com 8 horas
                    TimeSpan horarioRegular = new(8, 0, 0);

                    //Se passar do tempo regular
                    if (horasTrabalhadas > horarioRegular)
                    {
                        HorasExtras += (horasTrabalhadas - horarioRegular).TotalHours;
                    }
                    //Se for menor do que o tempo regular
                    else if (horasTrabalhadas < horarioRegular)
                    {
                        HorasDebito += (horarioRegular - horasTrabalhadas).TotalHours;
                    }

                    //O total a receber é: Horas trabalhadas * valor hora
                    TotalReceber += (horasTrabalhadas.TotalHours * leitura.ValorHora);

                    //Se for domingo ou sábado, adiciona um dia extra na contagem
                    if (leitura.Data.DayOfWeek == DayOfWeek.Sunday || leitura.Data.DayOfWeek == DayOfWeek.Saturday)
                    {
                        DiasExtras++;
                    }
                }

                DiasTrabalhados = leituras.Select(e => e.Data).Distinct().Count();

                //Começar com uma data no início do mês
                DateTime datePivot = new(leituras.First().Data.Year, leituras.First().Data.Month, 1);
                //Itera enquanto estiver no mesmo mês
                while (datePivot.Month == leituras.First().Data.Month)
                {
                    switch (datePivot.DayOfWeek)
                    {
                        //Caso a iteração esteja em um dia de semana
                        case DayOfWeek.Monday:
                        case DayOfWeek.Tuesday:
                        case DayOfWeek.Wednesday:
                        case DayOfWeek.Thursday:
                        case DayOfWeek.Friday:
                            //E não houver leituras para este dia
                            if (leituras.FirstOrDefault(l => l.Data.Equals(datePivot)) == null)
                            {
                                //Aumenta os dias de falta
                                DiasFalta++;
                            }
                            break;
                    }

                    datePivot = datePivot.AddDays(1);
                }
            }
        }
    }
}
