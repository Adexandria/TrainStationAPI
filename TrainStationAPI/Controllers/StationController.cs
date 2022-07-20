using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainStationAPI.Services;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO.Station;
using Mapster;

namespace TrainStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly ITrainStation<Station> _stationDb;

        public StationController(ITrainStation<Station> stationDb)
        {
            _stationDb = stationDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StationDTO>> GetAllStations()
        {
            IEnumerable<Station> stations = _stationDb.GetAll();
            IEnumerable<StationDTO> mappedStations = stations.Adapt<IEnumerable<StationDTO>>();
            return Ok(mappedStations);
        }

        [HttpGet("{stationId}")]
        public async Task<ActionResult<StationDTO>> GetStationById(Guid stationId)
        {
            Station station = await _stationDb.Get(stationId);
            if(station is null)
            {
                return NotFound();
            }
            StationDTO mappedStation = station.Adapt<StationDTO>();
            return Ok(mappedStation);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStation(StationCreate newStation)
        {
            Station station = newStation.Adapt<Station>();
            await _stationDb.Add(station);
            return Ok("Added Successfully");
        }

        [HttpPut("{stationId}")]
        public async Task<IActionResult> UpdateExistingStation(Guid stationId, StationUpdate updatedStation)
        {
            Station currentStation = await _stationDb.Get(stationId);
            if (currentStation is null)
            {
                return NotFound();
            }
            Station station = updatedStation.Adapt<Station>();
            await _stationDb.Update(station);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{stationId}")]
        public async Task<IActionResult> DeleteExistingStation(Guid stationId)
        {
            Station currentStation = await _stationDb.Get(stationId);
            if (currentStation is null)
            {
                return NotFound();
            }
            await _stationDb.Remove(stationId);
            return Ok("Deleted Successfully");
        }

    }
}
