using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserInfoSyncController : ControllerBase
{
    private readonly string USERS_FILE_PATH = "data/users.json";
    private readonly string LOCATIONS_FILE_PATH = "data/locations.json";
    private readonly string GAMES_FILE_PATH = "data/games.json";

    [HttpGet("user-info")]
    public ActionResult GetUserInfo()
    {
        var userId = GetRandomUserId();

        var location = GetUserLocation(userId);
        var favoriteGame = GetUserFavoriteGame(userId);
        
        return Ok(new { userId, location, favoriteGame });
    }

    private int GetRandomUserId()
    {
        var userJson = System.IO.File.ReadAllText(USERS_FILE_PATH);
        Task.Delay(1000).Wait();

        var usersData = JsonSerializer.Deserialize<UserData>(userJson) ?? throw new NullReferenceException();

        return usersData.Users.First().Id;
    }

    private string GetUserLocation(int userId)
    {
        var locationsJson = System.IO.File.ReadAllText(LOCATIONS_FILE_PATH);
        Task.Delay(3000).Wait();

        var locationsData = JsonSerializer.Deserialize<LocationData>(locationsJson) ?? throw new NullReferenceException();

        return locationsData.Locations.First(l => l.UserId == userId).LocationName; 
    }

    private string GetUserFavoriteGame(int userId)
    {
        var gamesJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
        Task.Delay(3000).Wait();

        var gamesData = JsonSerializer.Deserialize<GameData>(gamesJson) ?? throw new NullReferenceException();

        return gamesData.Games.First(l => l.UserId == userId).FavoriteGame; 
    }
}