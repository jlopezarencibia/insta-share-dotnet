using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using InstaShare.Models;
using InstaShare.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;

namespace InstaShare.FileService.Jobs;

public class ProcessFile : AsyncBackgroundJob<UserFile>, ITransientDependency
{
    private readonly IRepository<UserFile> _userFileRepository;

    public ProcessFile(IRepository<UserFile> userFileRepository)
    {
        _userFileRepository = userFileRepository;
    }

    [UnitOfWork]
    public override async Task ExecuteAsync(UserFile userFile)
    {
        userFile.FileStatus = FileStatus.Processing;
        await _userFileRepository.UpdateAsync(userFile);
        using (var compressedFileStream = new MemoryStream())
        {
            // Create an archive and store the stream in memory.
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
            {
                //Create a zip entry for each attachment
                var zipEntry = zipArchive.CreateEntry(userFile.FileName);

                //Get the stream of the attachment
                using (var originalFileStream = new MemoryStream(userFile.FileBytes))
                await using (var zipEntryStream = zipEntry.Open())
                {
                    //Copy the attachment stream to the zip entry stream
                    originalFileStream.CopyTo(zipEntryStream);
                }
            }
            userFile.FileBytes = compressedFileStream.ToArray();
            // userFile.FileName = $"{removeExtension(userFile.FileName)}.zip";
            userFile.FileStatus = FileStatus.Ready;
            await _userFileRepository.UpdateAsync(userFile);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }

    private string removeExtension(string filename)
    {
        var index = filename.Length - 1;
        for (int i = filename.Length - 1; i < 0; i--)
        {
            if (filename[i] != '.') continue;
            index = i;
            break;
        }

        return filename.Remove(index);
    }
}