// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PossibleAnswerItemsConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The possible answer items converter.
    /// </summary>
    public class PossibleAnswerItemsConverter : ITypeConverter<IEnumerable<PossibleAnswerItem>, PossibleAnswers>
    {
        /// <summary>
        /// Converts possible answers from request object to domain entity.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="PossibleAnswers"/>.
        /// </returns>
        public PossibleAnswers Convert(ResolutionContext context)
        {
            var from = (IEnumerable<PossibleAnswerItem>)context.SourceValue;
            var pa = new PossibleAnswers();

            for (int i = 0; i < from.Count(); i++)
            {
                var item = from.ElementAt(i);
                pa.Add(new PossibleAnswer(item.IsCorrectAnswer, (CompetitionAnswer) item.AnswerKey, item.AnswerText));
            }

            return pa;
        }
    }
}