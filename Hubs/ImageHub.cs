using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BlazorApp.Models;
using BlazorApp.Repositories;

namespace BlazorApp.Hubs{
    public class ImageHub : Hub
    {
        private readonly IImageUploadRepository _repository;

        public ImageHub(IImageUploadRepository repository)
        {
            _repository = repository;
        }

        public async Task BroadcastImageUpdate(string imageId)
        {
            await Clients.All.SendAsync("ReceiveImageUpdate", imageId);
        }

        public async Task BroadcastImageDelete(int imageId)
        {
            await Clients.All.SendAsync("ReceiveImageDelete", imageId);
        }

        public async Task UploadImageToDb(ImageFile image, string userId)
        {
            await _repository.UploadImageToDb(image, userId);
            await BroadcastImageUpdate(image.Id.ToString()); // Assuming ImageName is the URL
        }

        public async Task DeleteImageFromDb(int imageId, string userId)
        {
            await _repository.DeleteImageFromDb(imageId, userId);
            await BroadcastImageDelete(imageId);
        }
    }
}
