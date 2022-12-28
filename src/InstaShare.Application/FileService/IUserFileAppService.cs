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
    public Task<List<BasicUserFileDto>> GetAllFilesByUserIdAsync(int userId);
    public Task<BasicUserFileDto> UploadFileAsync(int userId, IFormFile formFile);
    public Task DownloadFileAsync(int fileId);
    public Task<BasicUserFileDto> RenameAsync(int fileId, string newName);

}