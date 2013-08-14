// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionViewModel.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace BB.SmsQuiz.Web.Models
{
    /// <summary>
    /// The competition view model.
    /// </summary>
    public class CompetitionViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        public string CompetitionKey { get; set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the correct answer key.
        /// </summary>
        [Range(1, 4)]
        public int CorrectAnswerKey { get; set; }

        /// <summary>
        /// Gets or sets the answer 1.
        /// </summary>
        public string Answer1 { get; set; }

        /// <summary>
        /// Gets or sets the answer 2.
        /// </summary>
        public string Answer2 { get; set; }

        /// <summary>
        /// Gets or sets the answer 3.
        /// </summary>
        public string Answer3 { get; set; }

        /// <summary>
        /// Gets or sets the answer 4.
        /// </summary>
        public string Answer4 { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the answer a count.
        /// </summary>
        public int AnswerACount { get; set; }

        /// <summary>
        /// Gets or sets the answer b count.
        /// </summary>
        public int AnswerBCount { get; set; }

        /// <summary>
        /// Gets or sets the answer c count.
        /// </summary>
        public int AnswerCCount { get; set; }

        /// <summary>
        /// Gets or sets the answer d count. 
        /// </summary>
        public int AnswerDCount { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }
    }
}