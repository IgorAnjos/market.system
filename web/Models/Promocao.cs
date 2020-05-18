using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mktSystem.Models
{
    public class Promocao
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Produto Produto { get; set; }
        public float Porcentagem { get; set; }
        public bool Status { get; set; }
    }
}
