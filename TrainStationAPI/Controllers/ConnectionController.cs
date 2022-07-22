using Microsoft.AspNetCore.Mvc;
using TrainStationAPI.Services;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO.Connection;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrainStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly ITrainStation<Connection> _connectionDb;
        private readonly ITrainStation<Station> _stationDb;
        private readonly ITrainStation<Train> _trainDb;
        public ConnectionController(ITrainStation<Connection> connectionDb, ITrainStation<Station> stationDb, ITrainStation<Train> trainDb)
        {
            _connectionDb = connectionDb;
            _stationDb = stationDb;
            _trainDb = trainDb;
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public ActionResult<IEnumerable<ConnectionDTO>> GetAllConnections()
        {
            IEnumerable<Connection> connections = _connectionDb.GetAll();
            IEnumerable<ConnectionDTO> mappedConnections = connections.Adapt<IEnumerable<ConnectionDTO>>();
            return Ok(mappedConnections);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectionDTO>> GetConnection(Guid id)
        {
            Connection connection = await _connectionDb.Get(id);
            ConnectionDTO mappedConnection = connection.Adapt<ConnectionDTO>();
            return Ok(mappedConnection);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddConnection(ConnectionCreate newConnection)
        {
            Train currentTrain = await _trainDb.Get(newConnection.TrainId);
            Station currentStation = await _stationDb.Get(newConnection.CurrentStationId);
            Station nextStation = await _stationDb.Get(newConnection.NextStationId);
            if (currentTrain is null || currentStation is null || nextStation is null)
            {
                return NotFound();
            }

            Connection connection = newConnection.Adapt<Connection>();
            
            connection.Train = currentTrain;
            connection.CurrentStation = currentStation;
            connection.NextStation = nextStation;
            
            await _connectionDb.Add(connection);
            return Ok("Added Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConnection(Guid id, ConnectionUpdate updatedConnection)
        {
            Connection connection = await _connectionDb.Get(id);
            if (connection is null)
            {
                return NotFound();
            }
            connection.Adapt(updatedConnection);
            await _connectionDb.Update(connection);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnection(Guid id)
        {
            Connection connection = await _connectionDb.Get(id);
            if (connection is null)
            {
                return NotFound();
            }
            await _connectionDb.Remove(id);
            return Ok("Deleted Successfully");
        }
    }
}
