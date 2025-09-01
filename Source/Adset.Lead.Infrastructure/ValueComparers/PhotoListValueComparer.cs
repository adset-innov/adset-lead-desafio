using Adset.Lead.Domain.Automobiles;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Adset.Lead.Infrastructure.ValueComparers;

/// <summary>
/// ValueComparer para detectar mudanças em listas de fotos
/// </summary>
internal static class PhotoListValueComparer
{
    public static ValueComparer<List<Photo>> Create()
    {
        return new ValueComparer<List<Photo>>(
            (currentList, newList) => currentList != null && newList != null && currentList.SequenceEqual(newList),
            photoList => photoList.Aggregate(0, (acc, photo) => HashCode.Combine(acc, photo.GetHashCode())),
            photoList => photoList.ToList()
        );
    }
}
