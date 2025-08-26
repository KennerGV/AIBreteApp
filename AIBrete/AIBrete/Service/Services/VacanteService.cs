using AIBrete.Shared.Model;
using AIBrete.Shared.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIBrete.Service.Services
{
    public class VacanteService : IVacanteService
    {
        private readonly List<Vacante> _vacantes = new()
    {
        new Vacante("Desarrollador .NET", "TechCorp", "San José", 90),
        new Vacante("Ingeniero de Software", "SoftSolutions", "Heredia", 72),
        new Vacante("QA Tester", "QualityApps", "Alajuela", 55),
        new Vacante("Frontend Developer", "WebWorks", "Cartago", 40),
        new Vacante("Backend Developer", "CodeBase", "San José", 78),
        new Vacante("DevOps Engineer", "CloudOps", "Heredia", 65),
        new Vacante("Data Analyst", "DataCorp", "Alajuela", 82),
        new Vacante("Project Manager", "PMGroup", "Cartago", 70),
        new Vacante("UX Designer", "DesignHub", "San José", 100)
    };

        public IEnumerable<Vacante> GetVacantes(string searchTerm, int minCompatibilidad, bool ascendente)
        {
            var query = _vacantes
                .Where(v => v.Compatibilidad >= minCompatibilidad
                    && (string.IsNullOrWhiteSpace(searchTerm)
                        || v.Titulo.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));

            return ascendente
                ? query.OrderBy(v => v.Compatibilidad)
                : query.OrderByDescending(v => v.Compatibilidad);
        }
    }

}
