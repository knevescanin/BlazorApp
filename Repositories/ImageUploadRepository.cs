using BlazorApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorApp.Repositories;
public interface IImageUploadRepository
{
    Task UploadImageToDb(ImageFile image, string userId);
    Task<IEnumerable<ImageFile>> GetImages(string userId);
}

public class ImageUploadRepository : IImageUploadRepository
{
    private readonly IConfiguration _config;

    const string CONN_KEY = "BlazorAppIdentityDbContextConnection";

    public ImageUploadRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task UploadImageToDb(ImageFile image, string userId)
    {
        var connectionString = _config.GetConnectionString(CONN_KEY); 
        using IDbConnection connection = new SqlConnection(connectionString);
        string sql = "INSERT INTO ImageFile (ImageName, UserId, DateUploaded) VALUES (@ImageName, @UserId, @DateUploaded)";
        await connection.ExecuteAsync(sql, new { ImageName = image.ImageName, UserId = userId, DateUploaded = image.DateUploaded });
    }

    public async Task<IEnumerable<ImageFile>> GetImages(string userId)
    {
        var connectionString = _config.GetConnectionString(CONN_KEY);
        using IDbConnection connection = new SqlConnection(connectionString);
        string sql = "SELECT * FROM ImageFile WHERE UserId = @UserId ORDER BY DateUploaded DESC";
        var images = await connection.QueryAsync<ImageFile>(sql, new { UserId = userId });

        return images;
    }
}