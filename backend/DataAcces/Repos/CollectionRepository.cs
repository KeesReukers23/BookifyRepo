using DataAcces;
using Interfaces;
using Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly BookifyContext _context;

        public CollectionRepository(BookifyContext context)
        {
            _context = context;
        }
        public async Task AddAsync(CollectionDto dto)
        {
            await _context.Collections.AddAsync(dto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CollectionDto>> GetAllAsync()
        {
            var collections = await _context.Collections.ToListAsync();
            return collections;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllByUserIdAsync(Guid userId)
        {
            var collections = await _context.Collections
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

            return collections;
        }

        public async Task<CollectionDto?> GetByIdAsync(Guid collectionId)
        {
            var collections = await _context.Collections.FindAsync(collectionId);
            if (collections == null)
            {
                return null;
            }

            return collections;
        }

        public async Task RemoveAsync(Guid collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);

            if (collection != null)
            {
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(CollectionDto dto)
        {
            var collection = await _context.Collections.FindAsync(dto.CollectionId);

            if (collection != null)
            {
                collection.Name = dto.Name;
                collection.UserId = dto.UserId;

                await _context.SaveChangesAsync();
            }
        }
    }
}
