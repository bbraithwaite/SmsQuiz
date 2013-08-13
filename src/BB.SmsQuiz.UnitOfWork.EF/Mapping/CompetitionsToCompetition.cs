using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BB.SmsQuiz.UnitOfWork.EF.Mapping
{
    public class CompetitionsToCompetition : ITypeConverter<EF.Competition, Model.Competitions.Competition>
    {
        public Model.Competitions.Competition Convert(ResolutionContext context)
        {
            if (context.SourceValue == null) return null;

            var source = (EF.Competition)context.SourceValue;

            var competition = new Model.Competitions.Competition();

            return competition;
        }
    }
}
