using System;
using System.Collections.Generic;

namespace RefactoringExercise
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUniversityRepository _universityRepository;

        public StudentService(IStudentRepository studentRepository, IUniversityRepository universityRepository)
        {
            _studentRepository = studentRepository;
            _universityRepository = universityRepository;
        }

        public bool Add(string emailAddress, Guid universityId)
        {
            Console.WriteLine(string.Format("Log: Start add student with email '{0}'", emailAddress));

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            if (_studentRepository.Exists(emailAddress))
            {
                return false;
            }

            var university = _universityRepository.GetById(universityId);

            var student = new Student(emailAddress, universityId);

            if (university.Package == Package.Standard)
            {
                student.MonthlyEbookAllowance = 10;
            }
            else if (university.Package == Package.Premium)
            {
                student.MonthlyEbookAllowance = 10 * 2;
            }

            _studentRepository.Add(student);
            
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
