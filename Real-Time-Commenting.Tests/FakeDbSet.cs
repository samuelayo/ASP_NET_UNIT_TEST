﻿using Real_Time_Commenting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Real_Time_Commenting.Tests
{
    public class FakeDbSet<T> : IDbSet<T>
    where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }

    public class FakeBlogPostSet : FakeDbSet<BlogPost>
    {
        public override BlogPost Find(params object[] keyValues)
        {
            return this.SingleOrDefault(e => e.BlogPostID == (int)keyValues.Single());
        }

    }

    public class FakeCommentSet : FakeDbSet<Comment>
    {
        public override Comment Find(params object[] keyValues)
        {
            return this.SingleOrDefault(e => e.BlogPostID == (int)keyValues.Single());
        }
    }

    public class FakedbContext : IrealtimeContext
    {
        public FakedbContext()
        {
            this.BlogPost = new FakeBlogPostSet();
            this.Comment = new FakeCommentSet();
        }

        public IDbSet<BlogPost> BlogPost { get; private set; }

        public IDbSet<Comment> Comment { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}