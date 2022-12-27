using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using InstaShare.Authorization.Users;
using InstaShare.Dtos.UserFile;
using InstaShare.Models;
using Microsoft.AspNetCore.Http;

namespace InstaShare.FileService;

public interface IUserFileAppService: IAsyncCrudAppService<UserFileDto>
{
    public Task<List<BasicUaserFileDto>> GetAllFilesByUserIdAsync(int userId);
    public Task<BasicUaserFileDto> UploadFileAsync(int userId, IFormFile formFile);
    public Task DownloadFileAsync(int fileId);

}