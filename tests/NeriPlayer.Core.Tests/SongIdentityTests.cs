using NeriPlayer.Core.Player.Model;

namespace NeriPlayer.Core.Tests;

public class SongIdentityTests
{
    [Fact]
    public void LocalSong_StableKey_IsNormalizedPath()
    {
        var item = new SongItem
        {
            Name = "test",
            Artist = "a",
            Album = "b",
            Platform = PlaybackSource.Local,
            FilePath = @"D:\Music\A\B\C.flac"
        };

        Assert.Equal("local|d:/music/a/b/c.flac", item.StableKey());
    }

    [Theory]
    [InlineData("https://www.youtube.com/watch?v=dQw4w9WgXcQ", "dQw4w9WgXcQ")]
    [InlineData("https://youtu.be/dQw4w9WgXcQ?si=abc", "dQw4w9WgXcQ")]
    public void YouTube_ExtractVideoId_Works(string url, string expected)
    {
        var item = new SongItem
        {
            Name = "x",
            Artist = "y",
            Album = "z",
            Platform = PlaybackSource.YouTubeMusic,
            AudioUrl = url   // 不设 VideoId，让 StableKey 走 URL 解析
        };

        Assert.Contains(expected, item.StableKey());
    }

    [Fact]
    public void Netease_StableKey_UsesAudioId()
    {
        var item = new SongItem
        {
            Name = "晴天",
            Artist = "周杰伦",
            Album = "叶惠美",
            Platform = PlaybackSource.Netease,
            AudioId = "3456789"
        };

        Assert.Equal("netease|3456789", item.StableKey());
    }
}
