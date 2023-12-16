using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;

namespace ContactBookLibrary.Models.Responses;

public class ServiceResult : IServiceResult
{
    public ServiceStatus Status { get; set; }
    public object Result { get; set; } = null!;
}
