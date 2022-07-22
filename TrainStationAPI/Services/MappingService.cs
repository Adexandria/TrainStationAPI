using Mapster;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO;

namespace TrainStationAPI.Services
{
    public class MappingService
    {
        public static TypeAdapterConfig AdminConfig()
        {
            var map = TypeAdapterConfig<SignUp, UserModel>.NewConfig().Map(dest => dest.PasswordHash, src => src.Password);
            return map.Config;
        }
    }
}
