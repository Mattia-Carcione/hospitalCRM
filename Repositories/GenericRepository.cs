using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;

namespace Repositories
{
    /// <summary>
    /// An instance of <see cref="GenericRepository{T, TContext}"/> to perform the CRUD operation on the context.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    public class GenericRepository<T, TContext> : IRepository<T> where T : class where TContext : DbContext
    {
        /// <summary>
        /// Provides access to database related information and operations for <typeparamref name="TContext"/>.
        /// </summary>
        protected readonly TContext _context;

        /// <summary>
        /// An <see cref="ILogger"/> object to perform logging.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="GenericRepository{T, TContext}"/> using the specified <paramref name="context"/> of type <typeparamref name="TContext"/>.
        /// </summary>
        /// <param name="context">The injection of the current context.</param>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> injection to perform logging.</param>
        public GenericRepository(TContext context, ILogger<GenericRepository<T, TContext>> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds the entity <typeparamref name="T"/> in the current context.
        /// </summary>
        /// 
        /// <param name="entity">The specified entity to add.</param>
        /// 
        /// <returns>
        /// An asynchronous task that represents the adding operation in the current context.
        /// </returns>
        /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
        public async Task AddAsync(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding the entity: {ex.Message}", ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

        /// <summary>
        /// Remove the entity <typeparamref name="T"/> from the current context.
        /// </summary>
        /// 
        /// <param name="entity">The entity to remove from the current context.</param>
        /// 
        /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
        public void Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the entity: {ex.Message}", ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

        /// <summary>
        /// Gets a list of entities of type <typeparamref name="T"/> wheter or not passing a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> that takes a LINQ operation. 
        /// </param>
        /// 
        /// <returns>
        /// An asynchronous task operation returning
        /// <description><see cref="IEnumerable{T}"/> represents the entities of the current context.</description>
        /// </returns>
        /// 
        /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? queryLinq = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if(queryLinq != null)
                    query = queryLinq(query);

                var collection = await query.ToListAsync();

                return collection;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting the entities: {ex.Message}", ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return Enumerable.Empty<T>();
            }
        }

        /// <summary>
        /// Gets the entity <typeparamref name="T"/> with the <paramref name="id"/>, whether or not passing a lambda expression <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> that takes a LINQ expression, such as sorting.
        /// <para>
        /// Defaults to <see langword="null"/>.
        /// </para>
        /// </param>
        /// <returns>A task representing asynchronous operation. The task result is the entity <typeparamref name="T"/> with the specified <paramref name="id"/>.</returns>
        /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
        public async Task<T?> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>>? queryLinq = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (queryLinq != null)
                    query = queryLinq(query);

                return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting the entity n. {id}: {ex.Message}", ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// 
        /// <returns>
        /// A task operation that represents the asynchronous save operation.
        /// </returns>
        /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while saving the entity changes: {ex.Message}", ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

        /// <summary>
        /// Updates the entity <typeparamref name="T"/> in the current context.
        /// </summary>
        /// 
        /// <param name="entity">The entity to update in the current context.</param>
        /// 
        /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
        public void Update(T entity)
        {
            try
            {
                _context.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating the entity: {ex.Message}", ex);
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }
    }
}
