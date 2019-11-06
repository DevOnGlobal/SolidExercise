using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    public interface IStudentFactory
    {
        Student Create(string emailAddress, Guid universityId, Package package);
    }
}
