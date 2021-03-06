﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using Greewf.BaseLibrary.Repositories;
using Greewf.BaseLibrary;
using Greewf.BaseLibrary.Linq;
using System.Transactions;

namespace Greewf.BaseLibrary.Repositories
{
    /// <summary>
    /// The main EntityFrmaework DbContext
    /// </summary>
    /// <typeparam name="T">EF Context</typeparam>
    /// <typeparam name="Y">UnitOfRepository</typeparam>
    public class RepositoryBase<T, Y>
        where T : DbContext, ISavingTracker, ITransactionScopeAwareness, IQueryHintContext, new()
        where Y : class, new()
    {
        protected T context = null;
        protected ContextManager<T> ContextManager { get; private set; }
        protected Y UoR { get; private set; }
        protected IValidationDictionary ValidationDictionary { get; private set; }//we cannot return it directly from ContextManager becuase sometimes it may be null

        protected RepositoryBase(ContextManager<T> contextManager, Y unitOfRepository)
        {
            ContextManager = contextManager;
            if (contextManager == null)
            {
                context = new T();// throw new Exception("ContextManager cannot be empty for Repository creation");
                ValidationDictionary = new DefaultValidationDictionary();//when contextmanager is null.                
            }
            else
            {
                context = contextManager.Context;
                ValidationDictionary = ContextManager.ValidationDictionary;
            }

            //handling events
            context.OnChangesSaving += OnChangesSaving;
            context.OnChangesSaved += OnChangesSaved;
            context.OnChangesCommitted += OnChangesCommitted;
            context.OnBeforeTransactionStart += OnBeforeTransactionStart;

            //if (contextManager == null)//because committing transaction is handled with context manager. when there is no any context manager, so we don't have any outer transaction(transactionscope indeed). so when the changes saved, it means commission too.
            //{
            //    context.OnChangesSaved += (o) =>
            //    {
            //        context.OnChangesCommittedEventInvoker();
            //    };
            //}

            UoR = unitOfRepository;

        }

        protected virtual void OnChangesSaving(DbContext context)
        {
        }

        protected virtual void OnChangesSaved(DbContext context)
        {
        }

        protected virtual void OnChangesCommitted()
        {

        }

        protected virtual void OnBeforeTransactionStart()
        {

        }

        protected IQueryable<X> AllIncluding<X>(DbSet<X> dbset, params Expression<Func<X, object>>[] includeProperties) where X : class, new()
        {
            IQueryable<X> query = dbset;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Detach<E>(E entity) where E : class
        {
            context.Entry<E>(entity).State = EntityState.Detached;
        }

        public string[] Errors
        {
            get
            {
                return ValidationDictionary.Errors;
            }
        }

    }

    public class RepositoryBase<T, Y, M> : RepositoryBase<T, Y>
        where T : DbContext, ISavingTracker, ITransactionScopeAwareness, IQueryHintContext, new()
        where Y : class, new()
        where M : class, new()
    {
        //NOTE! this class is just for generic ValidationDictionart<M>. please put your extensibility codes in base class.

        public RepositoryBase(ContextManager<T> contextManager, Y unitOfRepository)
            : base(contextManager, unitOfRepository)
        {
        }

        protected new IValidationDictionary<M> ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
        }

    }
}
