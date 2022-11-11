using MusalaSoft.Transpotation.Domain.Enums;

namespace MusalaSoft.Transpotation.Helpers
{
    public static class DroneHelpers
    {
        public static decimal GetMaxWeightLimit(int modelId)
        {
            if (modelId == (int)DroneModelType.Lightweight)
                return 125;
            else if (modelId == (int)DroneModelType.Middleweight)
                return 250;
            else if (modelId == (int)DroneModelType.Cruiserweight)
                return 375;
            else if (modelId == (int)DroneModelType.Heavyweight)
                return 500;
            else
                return 0;
        }
    }
}