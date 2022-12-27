using Abp.Application.Services.Dto;
using AutoMapper;
using InstaShare.Shared;

namespace InstaShare.Dtos.UserFile;
public class BasicUaserFileDto: EntityDto<int>
{
    public long FileSize { get; set; }
    public string FileName { get; set; }
    public FileStatus FileStatus { get; set; }
    public int UserId { get; set; }
}