using AutoMapper;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Domain.RequestModels;

namespace MusalaSoft.Transpotation.Domain
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Drone, DroneDataModel>().ReverseMap();
            CreateMap<DroneTrip, DroneTripDataModel>().ReverseMap();
            CreateMap<Medication, MedicationDataModel>().ReverseMap();

            CreateMap<DroneRegisterRequest, Drone>().ReverseMap();
            CreateMap<MedicationRequest, Medication>().ReverseMap();
        }
    }
}
