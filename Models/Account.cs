using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITransferencias.Models
{

    public class Account
    {

        public string id_cta { get; set; }

   
        public string num_cta { get; set; }

  
        public string moneda { get; set; }

 
        public double saldo { get; set; }

        public string cedula_cliente { get; set; }  

        public string cod_banco { get; set; }

        [JsonIgnore]
        public virtual Client cedula { get; set; }

        [JsonIgnore]
        public virtual Bank codigo_banco { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Transfer> numero_cta_origen { get; set; } = new List<Transfer>();

        [JsonIgnore]
        public virtual ICollection<Transfer> numero_cta_destino { get; set; } = new List<Transfer>();
    }
}
