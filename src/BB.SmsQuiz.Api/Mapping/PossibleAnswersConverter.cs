// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PossibleAnswersConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The possible answers converter.
    /// </summary>
    public class PossibleAnswersConverter : ITypeConverter<PossibleAnswers, IEnumerable<PossibleAnswerItem>>
    {
        /// <summary>
        /// Converts from domain entity to api model entity.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<PossibleAnswerItem> Convert(ResolutionContext context)
        {
            var from = (PossibleAnswers)context.SourceValue;
            return Mapper.Map<IEnumerable<PossibleAnswer>, IEnumerable<PossibleAnswerItem>>(from.Answers);
        }
    }
}