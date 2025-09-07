using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdSet.Lead.API.Binders;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public class FromQueryBinderAttribute<TBinder>() : ModelBinderAttribute(typeof(TBinder))
    where TBinder : IModelBinder;