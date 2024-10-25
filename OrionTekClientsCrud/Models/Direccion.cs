namespace OrionTekClientsCrud.Models
{
    public class Direccion
    {
        public int DireccionId { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }

}
