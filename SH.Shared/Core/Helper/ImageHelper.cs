namespace SH.Shared.Core.Helper;

public static class ImageHelper
{
    public static string BlobPath = "https://drmammstorage.blob.core.windows.net";

    public enum PhotoType
    {
        Gallery = 1,
    }

    public static string GetDefaultPhoto => "images/no-image.webp";

    public static string GetPhotoContainer(PhotoType type)
    {
        return type switch
        {
            PhotoType.Gallery => "photo-gallery",
            _ => throw new InvalidOperationException(nameof(PhotoType))
        };
    }
}