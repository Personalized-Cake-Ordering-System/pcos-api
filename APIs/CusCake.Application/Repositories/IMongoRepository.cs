namespace CusCake.Application.Repositories;
public interface IMongoRepository
{
    Task InsertAsync<T>(string table, T record) where T : class;
    Task<List<T>> GetAllAsync<T>(string table);
    Task<T> GetByIdAsync<T>(string table, Guid id);
    Task UpsertAsync<T>(string table, Guid id, T record) where T : class;
    Task<bool> DeleteAsync<T>(string table, Guid id);
}