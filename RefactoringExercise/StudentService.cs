using System;
using System.Collections.Generic;

namespace RefactoringExercise
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ILogger _logger;

        public StudentService(IStudentRepository studentRepository, IUniversityRepository universityRepository, ILogger logger)
        {
            _studentRepository = studentRepository;
            _universityRepository = universityRepository;
            _logger = logger;
        }

        public bool Add(string emailAddress, Guid universityId)
        {
            _logger.Log(string.Format("Log: Start add student with email '{0}'", emailAddress));

            if (_studentRepository.Exists(emailAddress))
            {
                return false;
            }

            var university = _universityRepository.GetById(universityId);

            var student = new Student(emailAddress, universityId, university.Package);

            _studentRepository.Add(student);

            _logger.Log(string.Format("Log: End add student with email '{0}'", emailAddress));

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
