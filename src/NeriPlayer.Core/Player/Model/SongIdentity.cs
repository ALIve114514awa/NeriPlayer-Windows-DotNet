using System.Text.RegularExpressions;

namespace NeriPlayer.Core.Player.Model;

/// <summary>
/// 对标 StableKeyUtil / StableKeyByChannelId：生成不依赖具体平台 ID 的统一稳定键。
/// </summary>
public static partial class SongIdentity
{
    public static string StableKey(this SongItem item)
    {
        if (item.IsLocalSong())
            return "local|" + NormalizePath(item.FilePath ?? string.Empty);

        switch (item.Platform)
        {
            case PlaybackSource.Netease:
                return "netease|" + item.AudioId;
            case PlaybackSource.Bilibili:
                return "bilibili|" + item.AudioId + "|" + item.SubAudioId;
            case PlaybackSource.YouTubeMusic:
                var vid = !string.IsNullOrEmpty(item.VideoId)
                    ? item.VideoId
                    : ExtractYouTubeVideoId(item.AudioUrl ?? string.Empty);
                return "ytm|" + (vid ?? item.AudioUrl ?? string.Empty);
            default:
                return (item.AudioUrl ?? string.Empty);
        }
    }

    private static string NormalizePath(string path)
    {
        return path
            .Replace('\\', '/')
            .TrimEnd('/')
            .ToLowerInvariant();
    }

    [GeneratedRegex(@"(?:[?&]v=|youtu\.be/|/shorts/)([A-Za-z0-9_-]{11})")]
    private static partial Regex YouTubeVideoIdRegex();

    private static string? ExtractYouTubeVideoId(string url)
    {
        var m = YouTubeVideoIdRegex().Match(url);
        return m.Success ? m.Groups[1].Value : null;
    }


}
