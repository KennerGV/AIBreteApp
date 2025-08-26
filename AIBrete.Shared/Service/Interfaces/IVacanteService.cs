using AIBrete.Shared.Model;

namespace AIBrete.Shared.Service.Interfaces
{
    public interface IVacanteService
    {
        IEnumerable<Vacante> GetVacantes(string searchTerm, int minCompatibilidad, bool ascendente);
    }
}
