using System;
using System.Collections.Generic;

namespace RefactoringExercise
{
    public class StudentWriterService : IStudentWriterService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ILogger _logger;
        private readonly IStudentFactory _studentFactory;

        public StudentWriterService(IStudentRepository studentRepository, IUniversityRepository universityRepository, ILogger logger, IStudentFactory studentFactory)
        {
            _studentRepository = studentRepository;
            _universityRepository = universityRepository;
            _logger = logger;
            _studentFactory = studentFactory;
        }

        public void Add(string emailAddress, Guid universityId)
        {
            _logger.Log(string.Format("Log: Start add student with email '{0}'", emailAddress));

            if (_studentRepository.Exists(emailAddress))
            {
                throw new ArgumentException("A user with the same e-mail address already exists.", emailAddress);
            }

            var university = _universityRepository.GetById(universityId);

            var student = _studentFactory.Create(emailAddress, universityId, university.Package);

            _studentRepository.Add(student);

            _logger.Log(string.Format("Log: End add student with email '{0}'", emailAddress));
        }

        public void AddBonusAllowances()
        {
            //...get students (IEnumerable<LimitedStudent>) for bonus allowance
            IEnumerable<LimitedStudent> students = new List<LimitedStudent>();
            
            foreach (LimitedStudent s in students)
            {
                s.AddBonusAllowance();
            }
            //...
        }
    }
}
