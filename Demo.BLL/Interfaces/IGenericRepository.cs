﻿using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetEntityById(int? id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
