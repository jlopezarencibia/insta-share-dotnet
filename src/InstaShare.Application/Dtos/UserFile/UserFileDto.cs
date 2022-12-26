using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using InstaShare.Shared;

namespace InstaShare.Dtos.UserFile;

[AutoMap(typeof(Models.UserFile))]
public class UserFileDto : EntityDto<int>
{
    public byte[] FileBytes { get; set; }
    public long FileSize { get; set; }
    public string FileName { get; set; }
    public FileStatus FileStatus { get; set; }
    public int UserId { get; set; }
}