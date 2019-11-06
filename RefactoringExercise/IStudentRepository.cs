using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringExercise
{
    public interface IStudentRepository
    {
        void Add(Student student);
        bool Exists(string emailAddress);
    }
}
