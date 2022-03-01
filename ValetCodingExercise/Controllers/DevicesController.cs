#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValetCodingExercise.Models;
using ValetCodingExercise.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ValetCodingExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ValetDbExampleContext _context;

        public DevicesController(ValetDbExampleContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        // GET: api/Devices/User
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices(int id)
        {
            var devices = await _context.Devices.Where(x => x.UserId == id).ToListAsync();

            return devices;
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound($"No devices with id {id}");
            }

            return device;
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, [FromBody] Device device)
        {
            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Device {device.Name} was updated");
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT user_manager.devices ON;");

                _context.Devices.Add(device);
                await _context.SaveChangesAsync();

                transaction.Commit();

                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT user_manager.devices OFF;");
            }

            return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok($"Device {device.Name} Deleted");
        }

        [HttpPatch("{id}/Mode")]
        public async Task<IActionResult> UpdateDeviceMode(int id, [FromBody] string mode)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            if (mode != device.Mode)
            {
                if (!Enum.TryParse(typeof(DeviceMode), mode, true, out var newMode))
                {
                    return BadRequest($"{mode} is not a supported device mode.");
                }

                device.Mode = newMode.ToString();
                await _context.SaveChangesAsync();
            }

            return Ok($"Mode was set {mode}");
        }

        [HttpPatch("{id}/Status")]
        public async Task<IActionResult> UpdateDeviceStatus(int id, [FromBody] string status)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            if (status != device.Status)
            {
                if (!Enum.TryParse(typeof(DeviceStatus), status, true, out var newStatus))
                {
                    return BadRequest($"{status} is not a supported device mode.");
                }

                device.Status = newStatus.ToString();
                await _context.SaveChangesAsync();
            }

            return Ok($"Status was set {status}");
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.DeviceId == id);
        }
    }
}
