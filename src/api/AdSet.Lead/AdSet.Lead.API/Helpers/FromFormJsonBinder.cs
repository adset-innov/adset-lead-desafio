using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace AdSet.Lead.API.Helpers
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class FromFormJsonAttribute() : ModelBinderAttribute(typeof(FromFormJsonBinder));

    public class FromFormJsonBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var json = valueProviderResult.FirstValue;

            if (string.IsNullOrWhiteSpace(json))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            try
            {
                var result = JsonSerializer.Deserialize(json, bindingContext.ModelType, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (JsonException ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName,
                    $"Error JSON deserializing: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}