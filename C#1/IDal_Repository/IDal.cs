namespace IDal_Repository
{
    public interface IDal<T>
    {
        public Task<List<T>> selectAllAsync();
    }
}
