// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostCompetitionItemConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The post competition item converter.
    /// </summary>
    public class PostCompetitionItemConverter : ITypeConverter<PostCompetition, Competition>
    {
        /// <summary>
        /// Convers post competition request to domain entity.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition Convert(ResolutionContext context)
        {
            return Mapper.Map<BaseCompetition, Competition>((BaseCompetition)context.SourceValue);
        }
    }
}