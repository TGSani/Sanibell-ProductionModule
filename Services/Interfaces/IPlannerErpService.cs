using Sanibell_ProductionModule.ViewModels;

namespace Sanibell_ProductionModule.Services.Interfaces;

public interface IPlannerErpService
{
    Task<string> SendProductionOrderToErpAsync(PlanningViewModel planning);
    Task UnlockProductionOrderAsync(string productieorderNummer);
    Task ProductionOrderCreatedByAsync(string productieorderNummer, string gebruiker);
    Task ProductionOrderUrgencyAsync(string productieorderNummer, bool Urgency);
    Task ProductionOrderVerwerkenAsync(string productieorderNummer);
    Task ProductionOrderActiveStatusAsync(string productieorderNummer);
}