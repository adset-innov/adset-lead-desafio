namespace AdSet.Core
{
    public interface IViewModelMapper
    {
        TTo Map<TTo>(object from);
        TTo Map<TFrom, TTo>(TFrom from);
        TTo Map<TFrom, TTo>(TFrom from, TTo to);
    }
}
