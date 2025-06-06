using Newtonsoft.Json;

namespace ClientesCRUD.Models
{
    public class Telefone
    {
        public virtual int Id { get; set; }
        public virtual string Numero { get; set; }
        public virtual bool Ativo { get; set; }

        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
    }
}
