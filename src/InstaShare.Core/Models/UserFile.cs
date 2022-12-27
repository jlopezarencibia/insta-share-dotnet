using Abp.Domain.Entities;
using InstaShare.Shared;

namespace InstaShare.Models;

public class UserFile: Entity<int>
{
    public byte[] FileBytes { get; set; }
    public long FileSize { get; set; }
    public string FileName { get; set; }
    public FileStatus FileStatus { get; set; }
    public int UserId { get; set; }
}