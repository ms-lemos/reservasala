namespace Reserva.Web.ViewModels
{
    public class TabelaVm
    {
        private string _actionErro;
        private string _actionFila;
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public int QtdeErro { get; set; }
        public int QtdeFila { get; set; }

        public string ActionErro
        {
            get => _actionErro ?? "DetalheErro";
            set => _actionErro = value;
        }

        public string ActionFila
        {
            get => _actionFila ?? "DetalheFila";
            set => _actionFila = value;
        }
    }
}