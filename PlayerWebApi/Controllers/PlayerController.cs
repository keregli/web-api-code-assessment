// /////////////////////////////////////////////////////////////////////////////
// YOU CAN FREELY MODIFY THE CODE BELOW IN ORDER TO COMPLETE THE TASK
// /////////////////////////////////////////////////////////////////////////////
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerWebApi.Data;
using PlayerWebApi.Data.Entities;

namespace PlayerWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerDbContext _dbContext;

    public PlayerController(PlayerDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        //Declaring both structures so PlayerSkills is not null when returning records.
        List<Player> ps = new List<Player>();
        List<PlayerSkill> ps2 = new List<PlayerSkill>();
        ps = _dbContext.Players.ToList();
        ps2 = _dbContext.PlayerSkills.ToList();
        int i = 0;

        //Fixing PlaySkills IDs bug
        foreach(var p in _dbContext.Players)
        {
            foreach(var pk in p.PlayerSkills)
            {
                i++;
                pk.Id = i;
            }
            i = 0;
        }


        return Ok(_dbContext.Players.ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        //Checking if Id exists
        var pa = _dbContext.Players.SingleOrDefault(p => p.Id == id);
        if (pa == null)
        {
            return NotFound("Player with Id:" + id + " does not exist");
        }

        //Declaring both structures so PlayerSkills is not null when returning records.
        List<Player> ps = new List<Player>();
        List<PlayerSkill> ps2 = new List<PlayerSkill>();
        ps = _dbContext.Players.ToList();
        ps2 = _dbContext.PlayerSkills.ToList();

        int i = 0;

        //Fixing PlaySkills IDs bug
        foreach (var p in _dbContext.Players)
        {
            foreach (var pk in p.PlayerSkills)
            {
                i++;
                pk.Id = i;
            }
            i = 0;
        }

        return Ok(_dbContext.Players.SingleOrDefault(p => p.Id == id));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Player p)
    {
        //Declaring Lists for Positions and Skills
        List<string> positions = new List<string>();
        positions.Add("defender");
        positions.Add("midfielder");
        positions.Add("forward");

        List<string> skills = new List<string>();
        skills.Add("defense");
        skills.Add("attack");
        skills.Add("speed");
        skills.Add("strength");
        skills.Add("stamina");

        //Player Name Validation 
        if (p.Name == "" || String.IsNullOrEmpty(p.Name))
        {
            return BadRequest("Error: Player Name can not be NULL or Empty");
        }

        //Position Validation
        if (!positions.Contains(p.Position))
        {
            return BadRequest("Error: Position must be 'defender', 'midfielder' or 'forward'");
        }

        //Player Skills Validation 
        if (p.PlayerSkills.Count() == 0)
        {
            return BadRequest("Error: Player Skills can not be Empty");
        }

        //Skills Validation
        foreach (var pk in p.PlayerSkills)
        {
            if (!skills.Contains(pk.Skill))
            {
                return BadRequest("Error: Skill must be 'defense', 'attack', 'speed', 'strength', or 'stamina'");
            }

            if(pk.Value < 1 || pk.Value > 99)
            {
                return BadRequest("Error: Value must be between [1 and 99]");
            }
        }

        _dbContext.Players.Add(p);
        _dbContext.SaveChanges();
        return Created("api/players/" + p.Id, p.Name);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Player>> PutAsync(int id, Player p)
    {
        //Declaring both structures so PlayerSkills is not null when returning records.
        List<Player> ps = new List<Player>();
        List<PlayerSkill> ps2 = new List<PlayerSkill>();
        ps = _dbContext.Players.ToList();
        ps2 = _dbContext.PlayerSkills.ToList();

        //Declaring Lists for Positions and Skills
        List<string> positions = new List<string>();
        positions.Add("defender");
        positions.Add("midfielder");
        positions.Add("forward");

        List<string> skills = new List<string>();
        skills.Add("defense");
        skills.Add("attack");
        skills.Add("speed");
        skills.Add("strength");
        skills.Add("stamina");

        //Checking to see if player exists
        var pa = _dbContext.Players.SingleOrDefault(p => p.Id == id);
        if (pa == null)
        {
            return NotFound("Player with Id:" + id + " does not exist");
        }

        //Player Name Validation 
        if (p.Name == "" || String.IsNullOrEmpty(p.Name))
        {
            return BadRequest("Error: Player Name can not be NULL or Empty");
        }

        //Position Validation
        if (!positions.Contains(p.Position))
        {
            return BadRequest("Error: Position must be 'defender', 'midfielder' or 'forward'");
        }

        //Player Skills Validation 
        if (p.PlayerSkills.Count() == 0)
        {
            return BadRequest("Error: Player Skills can not be Empty");
        }

        //Skills Validation
        foreach (var pk in p.PlayerSkills)
        {
            if (!skills.Contains(pk.Skill))
            {
                return BadRequest("Error: Skill must be 'defense', 'attack', 'speed', 'strength', or 'stamina'");
            }

            if (pk.Value < 1 || pk.Value > 99)
            {
                return BadRequest("Error: Value must be between [1 and 99]");
            }
        }

        pa.Name = p.Name;
        pa.Position = p.Position;
        pa.PlayerSkills = p.PlayerSkills;


        _dbContext.Update(pa);
        _dbContext.SaveChanges();
        return Ok("Player Updated Successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var pa = _dbContext.Players.SingleOrDefault(p => p.Id == id);
        if (pa == null)
        {
            return NotFound("Player with Id:" + id + " does not exist");
        }

        _dbContext.Players.Remove(pa);
        _dbContext.SaveChanges();

        return Ok("Player Deleted Successfully");
    }
}
