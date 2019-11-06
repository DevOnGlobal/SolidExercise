using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    public abstract class LimitedStudent : Student
    {
        protected LimitedStudent(string emailAddress, Guid universityId) : base(emailAddress, universityId)
        {
        }

        public abstract void AddBonusAllowance();
    }
}
