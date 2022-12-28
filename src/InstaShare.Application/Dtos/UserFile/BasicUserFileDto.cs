using Abp.Application.Services.Dto;
using AutoMapper;
using InstaShare.Shared;

namespace InstaShare.Dtos.UserFile;
public class BasicUserFileDto
{
    public int Id { get; set; }
    public long FileSize { get; set; }
    public string FileName { get; set; }
    public FileStatus FileStatus { get; set; }
    public int UserId { get; set; }
}