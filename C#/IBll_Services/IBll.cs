namespace IBll_Services
{
    public interface IBll<T>
    {
        public Task<List<T>> getAllAsync();

    }
}
