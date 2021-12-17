using System.IO;

namespace WebExplorer.Api.Data.Transfer {
    public record DirectoryDto(string Path, DirectoryDto? Parent = null) {
        public static DirectoryDto FromPath(string path) {
            var parent = Directory.GetParent(path);
            return parent is null ?
                new DirectoryDto(path) :
                new DirectoryDto(path, Parent: FromPath(parent.FullName));
        }
    }
}
