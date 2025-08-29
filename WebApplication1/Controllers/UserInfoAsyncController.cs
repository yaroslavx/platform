using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserInfoAsyncController : ControllerBase
{
    private readonly string USERS_FILE_PATH = "data/users.json";
    private readonly string LOCATIONS_FILE_PATH = "data/locations.json";
    private readonly string GAMES_FILE_PATH = "data/games.json";

    [HttpGet("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
        var userId = await GetRandomUserId();

        var location = await GetUserLocation(userId);
        var favoriteGame = await GetUserFavoriteGame(userId);
        
        return Ok(new { userId, location, favoriteGame });
    }

    private async Task<int> GetRandomUserId()
    {
        var userJson = await System.IO.File.ReadAllTextAsync(USERS_FILE_PATH);

        var usersData = JsonSerializer.Deserialize<UserData>(userJson) ?? throw new NullReferenceException();

        return usersData.Users.First().Id;
    }

    private async Task<string> GetUserLocation(int userId)
    {
        var locationsJson = await System.IO.File.ReadAllTextAsync(LOCATIONS_FILE_PATH);

        var locationsData = JsonSerializer.Deserialize<LocationData>(locationsJson) ?? throw new NullReferenceException();

        return locationsData.Locations.First(l => l.UserId == userId).LocationName; 
    }

    private async Task<string> GetUserFavoriteGame(int userId)
    {
        var gamesJson = await System.IO.File.ReadAllTextAsync(GAMES_FILE_PATH);

        var gamesData = JsonSerializer.Deserialize<GameData>(gamesJson) ?? throw new NullReferenceException();

        return gamesData.Games.First(l => l.UserId == userId).FavoriteGame; 
    }
}