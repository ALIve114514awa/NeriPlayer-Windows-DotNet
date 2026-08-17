using System.Text.Json.Serialization;

namespace NeriPlayer.Core.Player.Model;

public enum PlaybackSource
{
    Local,
    Netease,
    Bilibili,
    YouTubeMusic
}

public sealed record SongItem
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("coverUrl")]
    public string? CoverUrl { get; init; }

    [JsonPropertyName("duration")]
    public long Duration { get; init; }

    [JsonPropertyName("platform")]
    public PlaybackSource Platform { get; init; }

    [JsonPropertyName("audioUrl")]
    public string? AudioUrl { get; init; }

    [JsonPropertyName("audioUrlExpiry")]
    public DateTime? AudioUrlExpiry { get; init; }

    [JsonPropertyName("videoId")]
    public string? VideoId { get; init; }

    [JsonPropertyName("audioId")]
    public string? AudioId { get; init; }

    [JsonPropertyName("subAudioId")]
    public string? SubAudioId { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("artist")]
    public required string Artist { get; init; }

    [JsonPropertyName("album")]
    public required string Album { get; init; }

    [JsonPropertyName("albumId")]
    public string? AlbumId { get; init; }

    [JsonPropertyName("artistId")]
    public string? ArtistId { get; init; }

    [JsonPropertyName("extraInfo")]
    public string? ExtraInfo { get; init; }

    [JsonPropertyName("filePath")]
    public string? FilePath { get; init; }

    [JsonPropertyName("artworkFilePath")]
    public string? ArtworkFilePath { get; init; }

    [JsonPropertyName("hierarchicalArtist")]
    public string? HierarchicalArtist { get; init; }

    [JsonPropertyName("isFromLocalMediaStore")]
    public bool IsFromLocalMediaStore { get; init; }

    [JsonPropertyName("lyricUrl")]
    public string? LyricUrl { get; init; }

    [JsonPropertyName("neteaseMusicId")]
    public long? NeteaseMusicId { get; init; }

    [JsonPropertyName("bvid")]
    public string? Bvid { get; init; }

    [JsonPropertyName("cid")]
    public long? Cid { get; init; }

    [JsonPropertyName("lyricLrc")]
    public string? LyricLrc { get; init; }

    [JsonPropertyName("ttmlLyric")]
    public string? TtmlLyric { get; init; }

    [JsonPropertyName("translatedLrc")]
    public string? TranslatedLrc { get; init; }

    [JsonPropertyName("romanianLrc")]
    public string? RomanianLrc { get; init; }

    [JsonPropertyName("qqMusicId")]
    public long? QQMusicId { get; init; }

    [JsonPropertyName("isLocalMediaStoreSong")]
    public bool IsLocalMediaStoreSong { get; init; }

    [JsonPropertyName("isFmFallback")]
    public bool IsFmFallback { get; init; }

    [JsonPropertyName("orderInQueue")]
    public int OrderInQueue { get; init; }

    [JsonPropertyName("ttl")]
    public long Ttl { get; init; }

    /// <summary>显示名称：本地歌曲优先用文件名</summary>
    public string DisplayName =>
        IsLocalMediaStoreSong && !string.IsNullOrEmpty(FilePath)
            ? System.IO.Path.GetFileNameWithoutExtension(FilePath)
            : Name;

    /// <summary>显示艺术家</summary>
    public string DisplayArtist =>
        IsLocalMediaStoreSong
            ? (HierarchicalArtist ?? Artist)
            : Artist;

    public bool IsLocalSong() =>
        Platform == PlaybackSource.Local ||
        IsFromLocalMediaStore ||
        IsLocalMediaStoreSong;
}
