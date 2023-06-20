using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notifies
    {
        [NotMapped]
        public string _propName { get; set; }
        [NotMapped]
        public string _message { get; set; }
        [NotMapped]
        public List<Notifies> notifies { get; set; }

        public Notifies(string propName, string message) 
        {
            _propName = propName;
            _message = message;
            notifies = new List<Notifies>();
        }

        public bool stringValidation(string value, string propName)
        {
            if (string.IsNullOrWhiteSpace(propName) || string.IsNullOrWhiteSpace(value)) 
            {
                notifies.Add(new Notifies(propName, "Campo Obrigatorio"));
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool intValidation(int value, string propName)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(propName))
            {
                notifies.Add(new Notifies(propName, "Campo Obrigatorio"));
                return false;
            }
            else { return true; }
        }
    }
}
