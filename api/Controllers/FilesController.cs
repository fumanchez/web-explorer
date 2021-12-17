using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static System.IO.File;

namespace WebExplorer.Api.Controllers {
    using Data.Transfer;

    [ApiController]
    [Route("[controller]")]
    public class FilesController: ControllerBase {
        public static readonly string FallbackContentType = "application/x-binary";

        private readonly IContentTypeProvider _typeProvider;

        public FilesController(IContentTypeProvider typeProvider) {
            _typeProvider = typeProvider;
        }

        [HttpGet("{path}")]
        public FileStreamResult FileContent(string path) {
            var stream = Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(stream, contentType: GetFileContentType(path));
        }

        [HttpGet("{path}/*")]
        public ActionResult<FileDto[]> Files(string path) =>
            Directory.Exists(path) ?
                Directory.GetFiles(path)
                    .Select(p => new FileDto(Path: p, ContentType: GetFileContentType(path)))
                    .ToArray() :
                NotFound();

        private string GetFileContentType(string path) {
            _typeProvider.TryGetContentType(path, out var contentType);
            return contentType ?? FallbackContentType;
        }
    }
}
