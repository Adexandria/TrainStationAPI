using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO.Train;
using TrainStationAPI.Model.DTO.TrainInfo;
using TrainStationAPI.Services;

namespace TrainStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainStation<Train> _trainDb;
        private readonly ITrainInfo _trainInfoDb;
        public TrainController(ITrainStation<Train> trainDb, ITrainInfo trainInfoDb)
        {
            _trainDb = trainDb;
            _trainInfoDb = trainInfoDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TrainsDTO>> GetAllTrains()
        {
            IEnumerable<Train> trains = _trainDb.GetAll();
            IEnumerable<TrainsDTO> mappedTrains = trains.Adapt<IEnumerable<TrainsDTO>>();
            return Ok(mappedTrains);
        }

        [HttpGet("{trainId}")]
        public async Task<ActionResult<TrainDTO>> GetTrain(Guid trainId)
        {
            Train train = await _trainDb.Get(trainId);
            TrainInfo trainInfo = _trainInfoDb.GetTrainInfoByTrainId(trainId);
            if(train is null)
            {
                return NotFound();
            }
            TrainDTO mappedTrain = train.Adapt<TrainDTO>();
            TrainInfoDTO mappedInfo = trainInfo.Adapt<TrainInfoDTO>();
            mappedTrain.TrainInformation = mappedInfo;
            return Ok(mappedTrain);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrain(TrainCreate newTrain)
        {
            Train mappedTrain = newTrain.Adapt<Train>();
            await _trainDb.Add(mappedTrain);
            return Ok("Added Successfully");
        }

        [HttpPut("{trainId}")]
        public async Task<IActionResult> UpdateTrain(Guid trainId, TrainUpdate trainUpdate)
        {
            Train currentTrain = await _trainDb.Get(trainId);
            if (currentTrain is null)
            {
                return NotFound();
            }
            Train train = trainUpdate.Adapt<Train>();
            train.TrainId = trainId;
            await _trainDb.Update(train);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{trainId}")]
        public async Task<IActionResult> DeleteTrain(Guid trainId)
        {
            Train train = await _trainDb.Get(trainId);
            if (train is null)
            {
                return NotFound();
            }
            await _trainDb.Remove(trainId);
            return Ok("Deleted Successfully");
        }
    }
}
