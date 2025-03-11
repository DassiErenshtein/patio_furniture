namespace IDal_Repository
{
    public interface IDalClient:IDal<DTO_Command.Client>
    {
        public Task<DTO_Command.Client> getById(string id);
        public Task<bool> addClient(DTO_Command.Client client);


    }
}
