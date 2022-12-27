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
    public Task<List<UserFileDto>> GetAllFilesByUserIdAsync(int userId);
    public Task<UserFileDto> UploadFileAsync(int userId, IFormFile formFile);
    
    
}