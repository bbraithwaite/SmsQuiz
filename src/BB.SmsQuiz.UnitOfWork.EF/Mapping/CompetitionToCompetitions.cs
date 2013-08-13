using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BB.SmsQuiz.UnitOfWork.EF.Mapping
{
    public class CompetitionToCompetitions : ITypeConverter<Model.Competitions.Competition, EF.Competition>
    {
        public Competition Convert(ResolutionContext context)
        {
            if (context.SourceValue == null) return null;

            var source = (Model.Competitions.Competition)context.SourceValue;

            var competition = new Competition();

            return competition;
        }
    }
}
