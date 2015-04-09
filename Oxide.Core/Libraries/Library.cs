﻿using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Oxide.Core.Libraries
{
    /// <summary>
    /// Indicates that the specified function is a library function with a name
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LibraryFunction : Attribute
    {
        /// <summary>
        /// Gets the name for the library function
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates a library function using the methods name
        /// </summary>
        public LibraryFunction()
        {
        }

        /// <summary>
        /// Creates a library function using the given name
        /// </summary>
        /// <param name="name"></param>
        public LibraryFunction(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Represents a library containing a set of functions for script languages to use
    /// </summary>
    public abstract class Library
    {
        // Functions stored in this library
        private IDictionary<string, MethodInfo> functions;

        /// <summary>
        /// Returns if this library should be loaded into the global namespace
        /// </summary>
        public abstract bool IsGlobal { get; }

        /// <summary>
        /// Initializes a new instance of the Library class
        /// </summary>
        public Library()
        {
            functions = new Dictionary<string, MethodInfo>();
            foreach (var method in GetType().GetMethods())
            {
                try
                {
                    var attribute = method.GetCustomAttributes(typeof(LibraryFunction), true).SingleOrDefault() as LibraryFunction;
                    if (attribute != null) functions.Add(attribute.Name ?? method.Name, method);
                }
                catch (TypeLoadException)
                {
                    // Ignore rare exceptions caused by type information being loaded for all methods
                }
            }
        }

        /// <summary>
        /// Gets all function names in this library
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFunctionNames()
        {
            return functions.Keys;
        }

        /// <summary>
        /// Gets a function by the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MethodInfo GetFunction(string name)
        {
            MethodInfo info;
            if (functions.TryGetValue(name, out info)) return info;
            return null;
        }
    }
}
