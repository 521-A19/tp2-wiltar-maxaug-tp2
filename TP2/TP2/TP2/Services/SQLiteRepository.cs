﻿using TP2.Models.Entities;
using SQLite;
using System.Collections.Generic;

namespace TP2.Services
{

    public class SqLiteRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly SQLiteConnection _database;

        public SqLiteRepository(SQLiteConnection sqLiteConnection)
        {
            _database = sqLiteConnection;
            _database.CreateTable<T>(); // Si la table existe déjà ne fait rien. 
        }
        public IEnumerable<T> GetAll()
        {
            return _database.Table<T>().ToList();
        }

        public T GetById(int id)
        {
            return _database.Find<T>(id);
        }

        public void Delete(T entity)
        {
            _database.Delete(entity);
        }

        public void DeleteAll()
        {
            _database.DeleteAll<T>();
        }

        public void Add(T entity)
        {
            _database.Insert(entity);
        }

        public void Update(T entity)
        {
            _database.Update(entity);
        }
    }
}