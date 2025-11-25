using BarberShopManager.Domain.Reports.Resources;

namespace BarberShopManager.Domain.Entities.Enums;
public enum ServiceType
{
    MaleHairCut,
    FemaleHairCut,
    Beard,
    HairCutAndBeard,
}

public static class ServiceTypeExtensions
{
    public static string AsString(this ServiceType serviceType)
    {
        return serviceType switch
        {
            ServiceType.FemaleHairCut => ResourceReportMessages.SERVICE_TYPE_FEMALE_HAIRCUT,
            ServiceType.MaleHairCut => ResourceReportMessages.SERVICE_TYPE_MALE_HAIRCUT,
            ServiceType.HairCutAndBeard => ResourceReportMessages.SERVICE_TYPE_HAIRCUT_AND_BEAR,
            ServiceType.Beard => ResourceReportMessages.SERVICE_TYPE_BEARD,
            _ => serviceType.ToString(),
        };
    }
}
