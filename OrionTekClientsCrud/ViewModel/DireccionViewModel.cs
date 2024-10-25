using System.ComponentModel.DataAnnotations;

namespace OrionTekClientsCrud.ViewModel
{
    public class DireccionViewModel
    {
        public int DireccionId { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria.")]
        [StringLength(200, ErrorMessage = "La calle no puede tener más de 200 caracteres.")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(100, ErrorMessage = "La ciudad no puede tener más de 100 caracteres.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(100, ErrorMessage = "El estado no puede tener más de 100 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        public string CodigoPostal { get; set; }

        public int ClienteId { get; set; } // Para relacionar la dirección con el cliente
    }
}
