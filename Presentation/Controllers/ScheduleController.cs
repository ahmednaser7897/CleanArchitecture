using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
//https://localhost:7156/swagger/index.html
//https://localhost:7156/api/schedule
[ApiController]
[Route("api/[controller]")]
public class ScheduleController(IUnitOfWork UnitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var schedules = await UnitOfWork.Schedules.GetAll();
        return Ok(schedules);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var schedule = await UnitOfWork.Schedules.GetById(id);
        return Ok(schedule);
    }
    [HttpPost]
    public async Task<IActionResult> Post(Schedule schedule)
    {
        await UnitOfWork.Schedules.Add(schedule);
        await UnitOfWork.Complete();
        return Ok(schedule);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Schedule schedule)
    {
        await UnitOfWork.Schedules.Update(schedule);
        await UnitOfWork.Complete();
        return Ok(schedule);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        await UnitOfWork.Schedules.Remove(id);
        await UnitOfWork.Complete();
        return NoContent();
    }
}
