using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWeb.Models;
using System.Text.Json;

namespace MyWeb.ModelBinders
{
    public class FileUploadModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var model = new FileUploadModel();

            // Bind File
            var file = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                model.File = file;
            }

            // Bind simple properties
            model.Description = bindingContext.ValueProvider.GetValue("Description").FirstValue;
            model.Category = bindingContext.ValueProvider.GetValue("Category").FirstValue;

            if (DateTime.TryParse(bindingContext.ValueProvider.GetValue("UploadDate").FirstValue, out var uploadDate))
            {
                model.UploadDate = uploadDate;
            }

            // Bind Metadata
            var metadataJson = bindingContext.ValueProvider.GetValue("Metadata").FirstValue;
            if (!string.IsNullOrEmpty(metadataJson))
            {
                try
                {
                    model.Metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(metadataJson);
                }
                catch (JsonException)
                {
                    bindingContext.ModelState.AddModelError("Metadata", "Invalid JSON format for metadata");
                }
            }

            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
