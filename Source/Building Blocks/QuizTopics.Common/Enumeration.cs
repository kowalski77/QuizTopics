﻿#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuizTopics.Common
{
    public class Enumeration<T>
    {
        protected Enumeration() { }

        protected Enumeration(int id, string name)
        {
            (this.Id, this.Name) = (id, name);
        }

        public int Id { get; }

        public string Name { get; }

        protected static IEnumerable<T> All => LazyEnumeration.Value;

        private static readonly Lazy<IEnumerable<T>> LazyEnumeration = new(() =>
        {
            return typeof(T).GetFields(
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.DeclaredOnly)
                .Select(x => x.GetValue(null))
                .Cast<T>();
        });
    }
}