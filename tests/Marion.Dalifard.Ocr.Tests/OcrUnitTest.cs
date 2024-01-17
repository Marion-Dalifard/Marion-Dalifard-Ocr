using System.Reflection;
using Xunit;

namespace Marion.Dalifard.Ocr.Tests;

public class OcrUnitTest
{
    [Fact]
    public async Task ImagesShouldBeReadCorrectly()
    {
        var executingPath = GetExecutingPath();
        var images = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "images")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            images.Add(imageBytes);
        }
        var ocrResults = new Ocr().Read(images);
        Assert.Equal(ocrResults[0].Text, @"développeur C# au sens large. Car si
vous savez coder en C#, potentiellemer
vous savez coder avec toutes les
".Replace("\r\n", "\n"));;
        Assert.Equal(Math.Round(ocrResults[0].Confidence,2), 0.94);
        Assert.Equal(ocrResults[1].Text, @"Malheureusement les cabinets de
recrutement et les annonces, ainsi que les
études sur les salaires, gardent cette
terminologie obsolète.
".Replace("\r\n", "\n"));;
        Assert.Equal(Math.Round(ocrResults[1].Confidence,2), 0.95);
        Assert.Equal(ocrResults[2].Text, @"Quel salaire peut-on espérer ? Comme
toujours, les écarts moyens sont
importants. La base serait :
".Replace("\r\n", "\n"));;
        Assert.Equal(Math.Round(ocrResults[2].Confidence,2), 0.95);
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath =
            Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}