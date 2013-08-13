using AutoMapper;
using BB.SmsQuiz.Infrastructure.Mapping;

namespace BB.SmsQuiz.Web.Mapping
{
    /// <summary>
    /// The mapper service using AutoMapper
    /// </summary>
    public class AutoMapperService : IMapper
    {
        /// <summary>
        /// Maps the specified domain.
        /// </summary>
        /// <typeparam name="TDomain">The type of the domain.</typeparam>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="domain">The domain.</param>
        /// <returns>Returns a mapped entity.</returns>
        public TItem Map<TDomain, TItem>(TDomain domain) where TItem : class
        {
            return Mapper.Map<TDomain, TItem>(domain);
        }
    }
}