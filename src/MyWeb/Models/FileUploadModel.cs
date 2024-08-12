using System.ComponentModel.DataAnnotations;

namespace MyWeb.Models;

public class FileUploadModel
{
    [Required]
    public IFormFile File { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    [Required]
    public string Category { get; set; }

    public DateTime UploadDate { get; set; } = DateTime.UtcNow;

    public Dictionary<string, string> Metadata { get; set; }
}
