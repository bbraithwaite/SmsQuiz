using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Mapping
{
    /// <summary>
    /// The post competition converter.
    /// </summary>
    public class PutCompetitionConverter : ITypeConverter<CompetitionViewModel, PutCompetition>
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="PutCompetition"/>.
        /// </returns>
        public PutCompetition Convert(ResolutionContext context)
        {
            var viewModel = (CompetitionViewModel)context.SourceValue;
            var item = new PutCompetition()
            {
                ClosingDate = viewModel.ClosingDate,
                Answers = new List<string>
                        {
                            viewModel.Answer1, 
                            viewModel.Answer2, 
                            viewModel.Answer3, 
                            viewModel.Answer4
                        }.ToArray(),
                CorrectAnswerKey = viewModel.CorrectAnswerKey,
                Question = viewModel.Question
            };

            return item;
        }
    }
}