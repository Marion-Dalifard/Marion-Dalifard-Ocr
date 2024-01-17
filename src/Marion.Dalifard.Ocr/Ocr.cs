using System.Reflection;
using Tesseract;

namespace Marion.Dalifard.Ocr;

public class Ocr
{
    public List<OcrResult> Read(IList<byte[]> images)
    {
        var tessDataPath = Path.Combine(GetExecutingPath(), @"tessdata");

        List<OcrResult> results = new List<OcrResult>();

        var tasks = images.Select(async image =>
        {
            using var engine = new TesseractEngine(tessDataPath, "fra", EngineMode.Default);

            using var pix = Pix.LoadFromMemory(image);
            var test = await Task.Run(() => engine.Process(pix));
            var Text = test.GetText();
            var Confidence = test.GetMeanConfidence();
            return new OcrResult { Text = Text, Confidence = Confidence };
        }).ToList();
        
        Task.WhenAll(tasks).Wait();
        
        foreach (var task in tasks)
        {
            results.Add(task.Result);
        }

        return results;
    }
    
    public async Task<List<OcrResult>> ReadAsync(IList<byte[]> images)
    {
        var tessDataPath = Path.Combine(GetExecutingPath(), @"tessdata");

        List<OcrResult> results = new List<OcrResult>();

        var tasks = images.Select(async image =>
        {
            using var engine = new TesseractEngine(tessDataPath, "fra", EngineMode.Default);

            using var pix = Pix.LoadFromMemory(image);
            var test = await Task.Run(() => engine.Process(pix));
            var Text = test.GetText();
            var Confidence = test.GetMeanConfidence();
            return new OcrResult { Text = Text, Confidence = Confidence };
        });

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            results.Add(await task);
        }

        return results;
    }


    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}