using Marion.Dalifard.Ocr;

var imagesBytes = new List<byte[]>();

foreach (var arg  in args)
{
    imagesBytes.Add(await File.ReadAllBytesAsync(arg));
}

var ocrResults = new Ocr().Read(imagesBytes);

foreach (var ocrResult in ocrResults) 
{ 
    System.Console.WriteLine($"Confidence :{ocrResult.Confidence}"); 
    System.Console.WriteLine($"Text :{ocrResult.Text}"); 
} 
