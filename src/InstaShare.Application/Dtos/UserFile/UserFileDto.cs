using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using InstaShare.Shared;

namespace InstaShare.Dtos.UserFile;

[AutoMap(typeof(Models.UserFile))]
public class UserFileDto : EntityDto<int>
{
    public byte[] FileBytes;
    public long FileSize;
    public string FileName;
    public FileStatus FileStatus;
    public int UserId;
}