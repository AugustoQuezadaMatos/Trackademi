namespace TrackademiApi.Services.Interfaces
{
    public interface IGenericService<TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel?> GetByIdAsync(int id);
        Task AddAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(int id);
    }

}
