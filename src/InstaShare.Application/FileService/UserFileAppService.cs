using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using InstaShare.Authorization;
using InstaShare.Dtos.UserFile;
using InstaShare.Models;
using InstaShare.Shared;
using Microsoft.AspNetCore.Http;

namespace InstaShare.FileService;

/// <summary>
/// 
/// </summary>
public class UserFileAppService : AsyncCrudAppService<UserFile, UserFileDto>, IUserFileAppService
{
    /// <summary>
    /// Default repository constructor
    /// </summary>
    /// <param name="repository"></param>
    public UserFileAppService(IRepository<UserFile, int> repository) : base(repository)
    {
    }

    /// <summary>
    /// Get all the files for a giver user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [AbpAuthorize]
    [UnitOfWork]
    public async Task<List<BasicUaserFileDto>> GetAllFilesByUserIdAsync(int userId)
    {
        var userFiles = await Repository.GetAllListAsync(f => f.UserId == userId);
        var result = new List<BasicUaserFileDto>();
        foreach (var file in userFiles)
        {
            result.Add(new BasicUaserFileDto()
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
    public async Task<BasicUaserFileDto> UploadFileAsync(int userId, IFormFile formFile)
    {
        var a = userId;
        var b = formFile;

        // if (!formData.TryGetValue("UserId", out var userId))
        // {
        //     return null;
        // }

        var newUserFile = await Repository.InsertAsync(new UserFile()
        {
            FileName = formFile.FileName,
            FileSize = formFile.Length,
            FileBytes = ReadToEnd(formFile.OpenReadStream()),
            FileStatus = FileStatus.Pending,
            UserId = userId
        });

        return ObjectMapper.Map<BasicUaserFileDto>(newUserFile);
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