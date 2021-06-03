using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Phoenix.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly FirestoreDb firestoreDb;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            firestoreDb = FirestoreDb.Create("asp-mvc-b412a");
        }

        public DbSet<Product> Products { get; set; }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            if (entity is Product product)
            {
                var docRef = firestoreDb
                    .Collection("products")
                    .Document(product.Id);

                docRef.SetAsync(product).Wait();
            }

            return base.Add(entity);
        }

        public async Task<Product[]> GetProductsFromFirestoreAsync()
        {
            var productsRef = firestoreDb.Collection("products");
            var snapshot = await productsRef.GetSnapshotAsync();

            return snapshot
                .Documents
                .Select(d => d.ConvertTo<Product>())
                .ToArray();
        }
    }
}
