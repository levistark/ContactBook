using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;

namespace ContactBookLibrary.Models.Responses;

/// <summary>
/// ServiceResult används för att returnera service-resultat och status-koder
/// </summary>

public class ServiceResult : IServiceResult
{
    public ServiceStatus Status { get; set; }
    public object Result { get; set; } = null!;
}
