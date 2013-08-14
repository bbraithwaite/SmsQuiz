// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostCompetitionConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Mapping
{
    /// <summary>
    /// The post competition converter.
    /// </summary>
    public class PostCompetitionConverter : ITypeConverter<CompetitionViewModel, PostCompetition>
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="PostCompetition"/>.
        /// </returns>
        public PostCompetition Convert(ResolutionContext context)
        {
            var viewModel = (CompetitionViewModel) context.SourceValue;
            var item = new PostCompetition()
                {
                    ClosingDate = viewModel.ClosingDate, 
                    Answers = new List<string>
                        {
                            viewModel.Answer1, 
                            viewModel.Answer2, 
                            viewModel.Answer3, 
                            viewModel.Answer4
                        }.ToArray(), 
                    CompetitionKey = viewModel.CompetitionKey, 
                    CorrectAnswerKey = viewModel.CorrectAnswerKey, 
                    Question = viewModel.Question
                };

            return item;
        }
    }
}