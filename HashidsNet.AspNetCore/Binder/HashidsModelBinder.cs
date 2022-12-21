using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HashidsNet.AspNetCore.Binder;

internal class HashidsModelBinder : IModelBinder
{
    private readonly IHashids hashids;

    public HashidsModelBinder(IHashids hashids)
    {
        this.hashids = hashids;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext is null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        if (bindingContext.ModelType == typeof(int))
        {
            var result = this.hashids.DecodeSingle(value);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else if (bindingContext.ModelType == typeof(long))
        {
            var result = this.hashids.DecodeSingleLong(value);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            throw new ArgumentException("Invalid hashids type", nameof(bindingContext.ModelType.Name));
        }

        return Task.CompletedTask;
    }
}