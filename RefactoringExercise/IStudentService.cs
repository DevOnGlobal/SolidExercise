using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    public interface IStudentService
    {
        bool Add(string emailAddress, Guid universityId);

        IEnumerable<Student> GetStudentsByUniversity();
        IEnumerable<Student> GetStudentsByCurrentlyBorrowedEbooks();
    }
}
