using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class eLogin
    {
        [Required]
        [Display(Name = "Usuario o correo")]
        public string Access { get; set; }

        [Required]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        public string toURL { get; set; }
    }
}
