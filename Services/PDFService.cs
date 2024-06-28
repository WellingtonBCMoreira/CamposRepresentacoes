using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System.Runtime.InteropServices;

public class PDFService
{
    private readonly IConverter _converter;

    public PDFService(IConverter converter)
    {
        _converter = converter;
        LoadLibwkhtmltox();
    }

    private void LoadLibwkhtmltox()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var libraryPath = Path.Combine(currentDirectory, "References", "wkhtmltopdf", "libwkhtmltox.dll");

        Console.WriteLine($"Attempting to load wkhtmltox.dll from: {libraryPath}");

        if (!File.Exists(libraryPath))
        {
            throw new FileNotFoundException("libwkhtmltox.dll not found.", libraryPath);
        }

        var handle = LoadLibrary(libraryPath);
        if (handle == IntPtr.Zero)
        {
            throw new Exception($"Could not load libwkhtmltox: {libraryPath}");
        }
    }

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr LoadLibrary(string lpFileName);

    public byte[] ConvertToPdf(string htmlContent)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
            Objects = {
                new ObjectSettings() {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                }
            }
        };

        return _converter.Convert(doc);
    }
}
