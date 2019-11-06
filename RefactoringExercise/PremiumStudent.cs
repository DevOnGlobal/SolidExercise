using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    class PremiumStudent : Student
    {
        public PremiumStudent(string emailAddress, Guid universityId) : base(emailAddress, universityId)
        {
            this.MonthlyEbookAllowance = STANDARD_ALLOWANCE * 2;
        }

        public override void AddBonusAllowance()
        {
            this.MonthlyEbookAllowance += 10;
        }
    }
}
