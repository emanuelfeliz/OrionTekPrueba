namespace OrionTekClientsCrud.Models
{
    public class Cliente
    {

        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Direccion> Direcciones { get; set; }
    }
}
