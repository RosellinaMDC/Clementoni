using Clementoni.Interfaces;

namespace Clementoni.Services
{
    public class PersonaItaliaService : IPersonaService<PersonaItaliaService>
    {
        public string AggiungiPrefisso(string numero)
        {
            return "+39" + numero;
        }
    }
}
