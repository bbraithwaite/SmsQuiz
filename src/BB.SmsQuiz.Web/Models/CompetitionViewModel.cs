using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BB.SmsQuiz.Web.Models
{
    public class CompetitionViewModel
    {
        public string ID { get; set; }

        public string Question { get; set; }

        public string CompetitionKey { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ClosingDate { get; set; }

        public List<string> Answers { get; set; }

        public IEnumerable<SelectListItem> PossibleAnswerSelectList { get; set; }

        public int CorrectAnswer { get; set; }

        public CompetitionViewModel()
        {
            var selectList = new List<SelectListItem>();
            var answers = new List<string>();

            for (int i = 0; i < 4; i++)
            {
                selectList.Add(new SelectListItem()
                {
                    Selected = (i == 0),
                    Text = (i + 1).ToString(),
                    Value = i.ToString()
                });
                answers.Add("");
            }

            this.PossibleAnswerSelectList = selectList;
            this.Answers = answers;
        }
    }
}