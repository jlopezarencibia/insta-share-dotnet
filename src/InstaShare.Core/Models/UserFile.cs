using Abp.Domain.Entities;
using InstaShare.Authorization.Users;
using InstaShare.Shared;

namespace InstaShare.Models;

public class UserFile: Entity<int>
{
    public byte[] FileBytes;
    public long FileSize;
    public string FileName;
    public FileStatus FileStatus;
    public int UserId;
}