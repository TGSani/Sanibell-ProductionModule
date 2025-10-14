using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockPlannerRepository : IPlannerRepository
{
    private static readonly IReadOnlyList<Planning> _plannings =
        [
        new Planning {AdviesId = 1, ArtikelNummer = "14-234B", ArtikelOmschrijving = "Wasbak", Status = "Nieuw advies", Soort = "Voorraadtekort", HuidigAantal = 0, AanbevolenAantal = 200, MaximaalAantal = 500 },
        new Planning {AdviesId = 2, ArtikelNummer = "15-987A", ArtikelOmschrijving = "Kraan", Status = "Goedgekeurd", Soort = "Voorraadtekort", HuidigAantal = 50, AanbevolenAantal = 100, MaximaalAantal = 200 },
        new Planning {AdviesId = 3, ArtikelNummer = "12-543C", ArtikelOmschrijving = "Toilet", Status = "In productie", Soort = "Seizoensvraag", HuidigAantal = 30, AanbevolenAantal = 80, MaximaalAantal = 150 },
        new Planning {AdviesId = 4, ArtikelNummer = "18-765D", ArtikelOmschrijving = "Bad", Status = "Correctie gevraagd", Soort = "Voorraadtekort", HuidigAantal = 5, AanbevolenAantal = 20, MaximaalAantal = 50 },
        new Planning {AdviesId = 5, ArtikelNummer = "11-234E", ArtikelOmschrijving = "Douchekop", Status = "Afgerond", Soort = "Nieuwe vraag", HuidigAantal = 60, AanbevolenAantal = 60, MaximaalAantal = 100 },
        new Planning {AdviesId = 6, ArtikelNummer = "13-876F", ArtikelOmschrijving = "Spoelbak", Status = "Nieuw advies", Soort = "Voorraadtekort", HuidigAantal = 0, AanbevolenAantal = 150, MaximaalAantal = 300 },
        new Planning {AdviesId = 7, ArtikelNummer = "17-432G", ArtikelOmschrijving = "Mengkraan", Status = "Goedgekeurd", Soort = "Seizoensvraag", HuidigAantal = 20, AanbevolenAantal = 60, MaximaalAantal = 120 },
        new Planning {AdviesId = 8, ArtikelNummer = "16-555H", ArtikelOmschrijving = "Badmeubel", Status = "In productie", Soort = "Nieuwe vraag", HuidigAantal = 10, AanbevolenAantal = 40, MaximaalAantal = 80 },
        new Planning {AdviesId = 9, ArtikelNummer = "19-321J", ArtikelOmschrijving = "Handdoekrek", Status = "Correctie gevraagd", Soort = "Voorraadtekort", HuidigAantal = 2, AanbevolenAantal = 15, MaximaalAantal = 30 },
        new Planning {AdviesId = 10, ArtikelNummer = "20-654K", ArtikelOmschrijving = "WC-bril", Status = "Afgerond", Soort = "Seizoensvraag", HuidigAantal = 25, AanbevolenAantal = 25, MaximaalAantal = 50 },
        new Planning {AdviesId = 11, ArtikelNummer = "21-111L", ArtikelOmschrijving = "Wastafel", Status = "Nieuw advies", Soort = "Voorraadtekort", HuidigAantal = 0, AanbevolenAantal = 100, MaximaalAantal = 200 },
        new Planning {AdviesId = 12, ArtikelNummer = "22-222M", ArtikelOmschrijving = "Badkraan", Status = "Goedgekeurd", Soort = "Seizoensvraag", HuidigAantal = 40, AanbevolenAantal = 90, MaximaalAantal = 150 },
        new Planning {AdviesId = 13, ArtikelNummer = "23-333N", ArtikelOmschrijving = "Doucheput", Status = "In productie", Soort = "Nieuwe vraag", HuidigAantal = 15, AanbevolenAantal = 60, MaximaalAantal = 120 },
        new Planning {AdviesId = 14, ArtikelNummer = "24-444O", ArtikelOmschrijving = "Badkuip", Status = "Correctie gevraagd", Soort = "Voorraadtekort", HuidigAantal = 5, AanbevolenAantal = 25, MaximaalAantal = 50 },
        new Planning {AdviesId = 15, ArtikelNummer = "25-555P", ArtikelOmschrijving = "Toiletbril", Status = "Afgerond", Soort = "Nieuwe vraag", HuidigAantal = 20, AanbevolenAantal = 20, MaximaalAantal = 40 },
        new Planning {AdviesId = 16, ArtikelNummer = "26-666Q", ArtikelOmschrijving = "Handdouche", Status = "Nieuw advies", Soort = "Voorraadtekort", HuidigAantal = 0, AanbevolenAantal = 80, MaximaalAantal = 150 },
        new Planning {AdviesId = 17, ArtikelNummer = "27-777R", ArtikelOmschrijving = "Wastafelkraan", Status = "Goedgekeurd", Soort = "Seizoensvraag", HuidigAantal = 30, AanbevolenAantal = 70, MaximaalAantal = 100 },
        new Planning {AdviesId = 18, ArtikelNummer = "28-888S", ArtikelOmschrijving = "Badmeubelset", Status = "In productie", Soort = "Nieuwe vraag", HuidigAantal = 10, AanbevolenAantal = 50, MaximaalAantal = 100 },
        new Planning {AdviesId = 19, ArtikelNummer = "29-999T", ArtikelOmschrijving = "Handdoekhouder", Status = "Correctie gevraagd", Soort = "Voorraadtekort", HuidigAantal = 3, AanbevolenAantal = 20, MaximaalAantal = 40 },
        new Planning {AdviesId = 20, ArtikelNummer = "30-000U", ArtikelOmschrijving = "WC-rolhouder", Status = "Afgerond", Soort = "Seizoensvraag", HuidigAantal = 15, AanbevolenAantal = 15, MaximaalAantal = 30 },
        new Planning {AdviesId = 21, ArtikelNummer = "31-111V", ArtikelOmschrijving = "Regendouche", Status = "Nieuw advies", Soort = "Nieuwe vraag", HuidigAantal = 0, AanbevolenAantal = 120, MaximaalAantal = 250 },
        new Planning {AdviesId = 22, ArtikelNummer = "32-222W", ArtikelOmschrijving = "Badrand", Status = "Goedgekeurd", Soort = "Voorraadtekort", HuidigAantal = 25, AanbevolenAantal = 60, MaximaalAantal = 100 },
        new Planning {AdviesId = 23, ArtikelNummer = "33-333X", ArtikelOmschrijving = "Douchewand", Status = "In productie", Soort = "Seizoensvraag", HuidigAantal = 8, AanbevolenAantal = 30, MaximaalAantal = 60 },
        new Planning {AdviesId = 24, ArtikelNummer = "34-444Y", ArtikelOmschrijving = "Badfolie", Status = "Correctie gevraagd", Soort = "Nieuwe vraag", HuidigAantal = 2, AanbevolenAantal = 10, MaximaalAantal = 20 },
        new Planning {AdviesId = 25, ArtikelNummer = "35-555Z", ArtikelOmschrijving = "Toiletpapierhouder", Status = "Afgerond", Soort = "Voorraadtekort", HuidigAantal = 12, AanbevolenAantal = 12, MaximaalAantal = 25 }
    ];

    public Task<IReadOnlyList<Planning>> GetPlanningAsync(CancellationToken ct = default)
        => Task.FromResult(_plannings);

    public Task<Planning?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var plannings = _plannings.FirstOrDefault(u => u.AdviesId == id);
        return Task.FromResult(plannings);
    }
}