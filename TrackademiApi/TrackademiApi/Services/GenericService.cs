using TrackademiApi.Mappers;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services.Interfaces;

namespace TrackademiApi.Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TModel>
    where TEntity : class
    where TModel : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly ModelMapper.MapperPair<TEntity, TModel> _mapper;


        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
            _mapper = ModelMapper.GetMapper<TEntity, TModel>();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => _mapper.ToViewModel.Map(e));
        }

        public async Task<TModel?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.ToViewModel.Map(entity);
        }

        public async Task AddAsync(TModel model)
        {
            var entity = _mapper.ToEntity.Map(model);
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(TModel model)
        {
            var entity = _mapper.ToEntity.Map(model);
            _repository.Update(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveAsync();
            }
        }
    }

}
