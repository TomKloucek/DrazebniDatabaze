using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drazebni_databaze
{
    public class SimpleSetValidator : IValidator<string>
    {
        private static IValidator<string> validator = null;

        public static IValidator<string> GetInstance(List<Uzivatel> uzivatels)
        {
            if (validator == null)
            {
                validator = new SimpleSetValidator(uzivatels);
            }
            return validator;
        }
        private SimpleSetValidator(List<Uzivatel> list)
        {
            this.list = list;
        }

        private List<Uzivatel> list;
        public void Validate(string value)
        {
            if(list.Any(p=>p.Jmeno == value))
            {
                throw new Exception("Repeated value");
            }
        }
    }
}
