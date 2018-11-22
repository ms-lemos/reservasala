using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mensageria.Infra.Attributes;

namespace Mensageria.Domain.Entities.Mensageria
{
    [FulltechTable("TemplateAgendamento", Schema = "jarvis", Context = "BMPContext")]
    public class TemplateAgendamento : EntityBase<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }

        [NotMapped]
        public override int Codigo
        {
            get => ID;
            set => ID = value;
        }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        /// <inheritdoc />
        protected override void InsertValidate()
        {
        }

        /// <inheritdoc />
        protected override void UpdateValidate()
        {
        }
    }
}