﻿using System.Collections.Generic;

namespace ClientesCRUD.Models
{
    public class Cliente
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sexo { get; set; }
        public virtual string Endereco { get; set; }
        public virtual IList<Telefone> Telefones { get; set; } = new List<Telefone>();
    }
}
