using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;

namespace MauiContactBook.Services;
public class MauiViewServices
{
    public string HandleServiceResult(IServiceResult serviceResult)
    {
        switch (serviceResult.Status)
        {
            case ServiceStatus.SUCCESS:
                return "Contact was successfully updated";

            case ServiceStatus.CREATED:
                return "Contact was successfully updated";

            case ServiceStatus.UPDATED:
                return "Contact was successfully updated";

            case ServiceStatus.FAILED:
                return "Something went wrong. Check debug message";

            case ServiceStatus.NOT_FOUND:
                return "Not found. Check debug message";
        }
        return null!;
    }
}
