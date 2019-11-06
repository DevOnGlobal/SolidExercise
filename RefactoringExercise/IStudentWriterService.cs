using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    public interface IStudentWriterService
    {
        bool Add(string emailAddress, Guid universityId);
        void AddBonusAllowances();
    }
}
