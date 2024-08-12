using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;

namespace MyWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly ILogger<UploadController> _logger;

    public UploadController(ILogger<UploadController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] FileUploadModel model)
    {
        if (model.File == null || model.File.Length == 0)
            return BadRequest("File is empty");

        var fileName = Path.GetFileName(model.File.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", model.Category, fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        _logger.LogInformation($"File {fileName} has been uploaded successfully. " +
                               $"Description: {model.Description}, " +
                               $"Category: {model.Category}, " +
                               $"Upload Date: {model.UploadDate}, " +
                               $"Metadata Count: {model.Metadata.Count}");

        return Ok(new
        {
            FileName = fileName,
            Description = model.Description,
            Category = model.Category,
            UploadDate = model.UploadDate,
            MetadataCount = model.Metadata.Count
        });
    }
}
