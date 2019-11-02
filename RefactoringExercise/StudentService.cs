using System;
using System.Collections.Generic;

namespace RefactoringExercise
{
    public class StudentService
    {
        public bool Add(string emailAddress, Guid universityId)
        {
            Console.WriteLine(string.Format("Log: Start add student with email '{0}'", emailAddress));

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            var studentRepository = new StudentRepository();

            if (studentRepository.Exists(emailAddress))
            {
                return false;
            }

            var universityRepository = new UniversityRepository();

            var university = universityRepository.GetById(universityId);

            var student = new Student(emailAddress, universityId);

            if (university.Package == Package.Standard)
            {
                student.MonthlyEbookAllowance = 10;
            }
            else if (university.Package == Package.Premium)
            {
                student.MonthlyEbookAllowance = 10 * 2;
            }

            studentRepository.Add(student);
            
            Console.WriteLine(string.Format("Log: End add student with email '{0}'", emailAddress));

            return true;
        }

        public IEnumerable<Student> GetStudentsByUniversity()
        {
            //...
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsByCurrentlyBorrowedEbooks()
        {
            //...
            throw new NotImplementedException();
        }
    }
}
