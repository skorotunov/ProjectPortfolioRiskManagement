using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPortfolioRiskManager.Domain.Entities
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public int LikertItemId { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
        public virtual Question Question { get; set; }
        public virtual LikertItem LikertItem { get; set; }
    }
}
