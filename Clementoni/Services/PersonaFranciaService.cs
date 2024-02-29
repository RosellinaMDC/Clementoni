using Clementoni.Interfaces;

namespace Clementoni.Services
{
    public class PersonaFranciaService : IPersonaService<PersonaFranciaService>
    {
        public string AggiungiPrefisso(string numero)
        {
        return "+33" + numero;  
        }
    }
}
