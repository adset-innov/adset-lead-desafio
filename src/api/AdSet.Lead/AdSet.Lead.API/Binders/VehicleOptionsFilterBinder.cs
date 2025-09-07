using AdSet.Lead.Domain.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdSet.Lead.API.Binders;

public class VehicleOptionsFilterBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var filter = new VehicleOptionsFilter();

        foreach (var kvp in bindingContext.ActionContext.HttpContext.Request.Query)
        {
            if (!kvp.Key.StartsWith("options.", StringComparison.OrdinalIgnoreCase))
                continue;

            var optionName = kvp.Key["options.".Length..];

            var rawValue = kvp.Value.FirstOrDefault();
            if (rawValue == null)
                continue;

            if (bool.TryParse(rawValue, out var parsed))
            {
                filter.AddOrUpdate(optionName, parsed);
            }
        }

        bindingContext.Result = ModelBindingResult.Success(filter);
        return Task.CompletedTask;
    }
}