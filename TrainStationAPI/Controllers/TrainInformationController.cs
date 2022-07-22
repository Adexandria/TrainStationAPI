using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO.TrainInfo;
using TrainStationAPI.Services;

namespace TrainStationAPI.Controllers
{
    [Route("api/{trainId}/[controller]")]
    [ApiController]
    public class TrainInformationController : ControllerBase
    {
        private readonly ITrainInfo _trainInfoDb;
        private readonly ITrainStation<Train> _trainDb;
        public TrainInformationController(ITrainInfo trainInfoDb, ITrainStation<Train> trainDb)
        {
            _trainInfoDb = trainInfoDb;
            _trainDb = trainDb;
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainInformation(Guid trainId, [FromBody] TrainInfoCreate newTrainInfo)
        {
            Train train = await _trainDb.Get(trainId);
            if (train is null)
            {
                return NotFound();
            }
            TrainInfo trainInfo = newTrainInfo.Adapt<TrainInfo>();
            trainInfo.Train = train;
            await _trainInfoDb.Add(trainInfo);
            return Ok("Added successfully");
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateExistingTrainInformation(Guid trainId, Guid infoId, [FromBody] TrainInfoUpdate newTrainInfo)
        {
            Train train = await _trainDb.Get(trainId);
            
            bool currentTrainInfo =  _trainInfoDb.IsExist(infoId);
            
            if (train is null || !currentTrainInfo)
            {
                return NotFound();
            }
            
            TrainInfo trainInfo = newTrainInfo.Adapt<TrainInfo>();
            trainInfo.Train = train;
            trainInfo.InfoId = infoId;
            
            await _trainInfoDb.Update(trainInfo);
            return Ok("Updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrainInformation(Guid trainId, Guid infoId)
        {
            Train train = await _trainDb.Get(trainId);

            bool currentTrainInfo =  _trainInfoDb.IsExist(infoId);

            if (train is null || !currentTrainInfo)
            {
                return NotFound();
            }
            await _trainInfoDb.Remove(infoId);
            return Ok("Deleted successfully");
        }
    }
}
