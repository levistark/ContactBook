using ContactBookLibrary.Enums;

namespace ContactBookLibrary.Interfaces;

public interface IServiceResult
{
    object Result { get; set; }
    ServiceStatus Status { get; set; }
}