using BB.SmsQuiz.Model.Competitions;
using System;
using System.Configuration;

namespace BB.SmsQuiz.Repository.Tests
{
    /// <summary>
    /// Repository factory for our tests.
    /// </summary>
    internal class RepositoryFactory
    {
        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// An instance of the repository under test.
        /// </returns>
        internal static object Instance(string instance)
        {
            string targetAssembly = ConfigurationManager.AppSettings["targetAssembly"];
            return Activator.CreateInstance(targetAssembly, targetAssembly + "." + instance).Unwrap();
        }
    }
}