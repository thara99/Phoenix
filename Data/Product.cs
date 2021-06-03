using Google.Cloud.Firestore;
using System;

namespace Phoenix.Data
{
    [FirestoreData]
    public class Product
    {
        [FirestoreProperty]
        public string Id { get; set; } = $"{Guid.NewGuid()}";

        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;

        [FirestoreProperty]
        public float Price { get; set; }
    }
}
