using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bit.EPi.Property.BitForm
{
    public abstract class BitFormDynamicData<T> : IDynamicData where T : BitFormDynamicData<T>
    {

        public Identity Id { get; set; }

        protected static DynamicDataStore GetStore()
        {
            return DynamicDataStoreFactory.Instance.CreateStore(typeof(T));
        }

        public void Initialize()
        {
            this.Id = Identity.NewIdentity(Guid.NewGuid());
        }

        public BitFormDynamicData()
        {
            Initialize();
        }

        public void Save()
        {
            GetStore().Save(this);
        }

        public void Delete()
        {
            GetStore().Delete(this);
        }

        public static void Delete(string id)
        {
            Get(id).Delete();
        }

        public static T Get(Identity id)
        {
            var d = GetStore().Items<T>().Where(x => x.Id.StoreId == id.StoreId).FirstOrDefault();

            return d;
        }

        public static T Get(string id)
        {
            return Get(Identity.Parse(id));
        }

        public static IEnumerable<T> GetAll()
        {
            return GetStore().Items<T>();
        }

        public static IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return GetStore().Items<T>().Where(predicate);
        }
    }
}
