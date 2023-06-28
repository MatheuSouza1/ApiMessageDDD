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
        public string PropName { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public List<Notifies> Notify { get; set; }

        public Notifies(string propName, string message) 
        {
            PropName = propName;
            Message = message;
            Notify = new List<Notifies>();
        }

        public bool stringValidation(string value, string propName)
        {
            if (string.IsNullOrWhiteSpace(propName) || string.IsNullOrWhiteSpace(value)) 
            {
                Notify.Add(new Notifies(propName, "Campo Obrigatorio"));
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
                Notify.Add(new Notifies(propName, "Campo Obrigatorio"));
                return false;
            }
            else { return true; }
        }
    }
}
