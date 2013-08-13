// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutCompetitionItemConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The put competition item converter.
    /// </summary>
    public class PutCompetitionItemConverter : ITypeConverter<PutCompetition, Competition>
    {
        /// <summary>
        /// Converts request object to domain entity.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition Convert(ResolutionContext context)
        {
            return Mapper.Map<BaseCompetition, Competition>((BaseCompetition) context.SourceValue);
        }
    }
}