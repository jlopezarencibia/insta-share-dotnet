using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using InstaShare.Authorization;
using InstaShare.Dtos.UserFile;
using InstaShare.FileService.Jobs;
using InstaShare.Models;
using InstaShare.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaShare.FileService;

/// <summary>
/// 
/// </summary>
public class UserFileAppService : AsyncCrudAppService<UserFile, UserFileDto>, IUserFileAppService
{
    private readonly IBackgroundJobManager _backgroundJobManager;
    /// <summary>
    /// Default repository constructor
    /// </summary>
    /// <param name="repository"></param>
    public UserFileAppService(
        IRepository<UserFile> repository,
        IBackgroundJobManager backgroundJobManager) : base(repository)
    {
        _backgroundJobManager = backgroundJobManager;
    }

    /// <summary>
    /// Get all the files for a giver user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [AbpAuthorize]
    [UnitOfWork]
    public async Task<List<BasicUserFileDto>> GetAllFilesByUserIdAsync(int userId)
    {
        var userFiles = await Repository.GetAllListAsync(f => f.UserId == userId);
        var result = new List<BasicUserFileDto>();
        foreach (var file in userFiles)
        {
            result.Add(new BasicUserFileDto()
            {
                Id = file.Id,
                FileName = file.FileName,
                FileSize = file.FileSize,
                FileStatus = file.FileStatus,
                UserId = file.UserId
            });
        }

        return result;
    }

    /// <summary>
    /// Upload the file for a user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="formFile"></param>
    /// <returns></returns>
    [AbpAuthorize]
    [UnitOfWork]
    [DisableRequestSizeLimit,
     RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, 
         ValueLengthLimit = int.MaxValue)]
    public async Task<BasicUserFileDto> UploadFileAsync(int userId, IFormFile formFile)
    {
        var newUserFile = await Repository.InsertAsync(new UserFile()
        {
            FileName = formFile.FileName,
            FileSize = formFile.Length,
            FileBytes = ReadToEnd(formFile.OpenReadStream()),
            FileStatus = FileStatus.Pending,
            UserId = userId
        });
        await CurrentUnitOfWork.SaveChangesAsync();
        
        await _backgroundJobManager.EnqueueAsync<ProcessFile, UserFile>(newUserFile);

        var result = new BasicUserFileDto()
        {
            Id = newUserFile.Id,
            FileName = newUserFile.FileName,
            FileSize = newUserFile.FileSize,
            FileStatus = newUserFile.FileStatus,
            UserId = newUserFile.UserId
        };

        return result;
    }

    public Task DownloadFileAsync(int fileId)
    {
        return null;
    }

    public async Task<BasicUserFileDto> RenameAsync(int fileId, string newName)
    {
        var file = await Repository.GetAsync(fileId);
        file.FileName = newName;
        var newFile = await UpdateAsync(ObjectMapper.Map<UserFileDto>(file));
        await CurrentUnitOfWork.SaveChangesAsync();
        return new BasicUserFileDto()
        {
            Id = newFile.Id,
            FileName = newFile.FileName,
            FileSize = newFile.FileSize,
            FileStatus = newFile.FileStatus,
        };
    }

    private byte[] ReadToEnd(Stream stream)
    {
        long originalPosition = 0;
        
        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];
            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;
                
                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }

            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }
}