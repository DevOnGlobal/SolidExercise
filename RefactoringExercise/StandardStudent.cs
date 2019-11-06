using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    class StandardStudent : LimitedStudent
    {
        public StandardStudent(string emailAddress, Guid universityId) : base(emailAddress, universityId)
        {
            this.MonthlyEbookAllowance = Student.STANDARD_ALLOWANCE;
        }

        public override void AddBonusAllowance()
        {
            this.MonthlyEbookAllowance += 5;
        }
    }
}
