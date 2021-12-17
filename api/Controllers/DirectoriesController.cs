using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using static System.IO.Directory;

namespace WebExplorer.Api.Controllers {
    using Data.Transfer;

    [ApiController]
    [Route("dirs")]
    public class DirectoriesController: ControllerBase {
        public static readonly DirectoryDto UnixRootDirectory = new DirectoryDto("/");

        [HttpGet]
        public DirectoryDto[] Roots() =>
            Environment.OSVersion.Platform is PlatformID.Win32NT ?
                GetLogicalDrives()
                    .Select(name => new DirectoryDto(name))
                    .ToArray() :
                GetDirectories(UnixRootDirectory.Path)
                    .Select(name => new DirectoryDto(name, Parent: UnixRootDirectory))
                    .ToArray();

        [HttpGet("{path}")]
        public ActionResult<DirectoryDto[]> Directories(string path) =>
            Exists(path) ?
                GetDirectories(path)
                    .Select(p => DirectoryDto.FromPath(p))
                    .ToArray() :
                NotFound();
    }
}
