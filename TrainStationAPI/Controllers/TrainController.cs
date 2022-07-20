using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO.Train;
using TrainStationAPI.Services;

namespace TrainStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainStation<Train> _trainDb;
        public TrainController(ITrainStation<Train> trainDb)
        {
            _trainDb = trainDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TrainDTO>> GetAllTrains()
        {
            IEnumerable<Train> trains = _trainDb.GetAll();
            IEnumerable<TrainDTO> mappedTrains = trains.Adapt<IEnumerable<TrainDTO>>();
            return Ok(mappedTrains);
        }

        [HttpGet("{trainId}")]
        public async Task<ActionResult<TrainDTO>> GetTrain(Guid trainId)
        {
            Train train = await _trainDb.Get(trainId);
            if(train is null)
            {
                return NotFound();
            }
            TrainDTO mappedTrain = train.Adapt<TrainDTO>();
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
            train.TrainId.Adapt(trainId);
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
