using System.ComponentModel.DataAnnotations;


namespace OrionTekClientsCrud.ViewModel
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        public List<DireccionViewModel> Direcciones { get; set; } = new List<DireccionViewModel>();
    }
}
