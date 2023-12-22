using ContactBookLibrary.Enums;

namespace ContactBookLibrary.Interfaces;

/// <summary>
/// Interface för ServiceResult
/// </summary>
public interface IServiceResult
{
    object Result { get; set; }
    ServiceStatus Status { get; set; }
}