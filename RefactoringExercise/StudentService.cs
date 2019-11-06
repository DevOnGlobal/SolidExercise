﻿using System;
using System.Collections.Generic;

namespace RefactoringExercise
{
    public class StudentService : IStudentReaderService, IStudentWriterService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ILogger _logger;
        private readonly IStudentFactory _studentFactory;

        public StudentService(IStudentRepository studentRepository, IUniversityRepository universityRepository, ILogger logger, IStudentFactory studentFactory)
        {
            _studentRepository = studentRepository;
            _universityRepository = universityRepository;
            _logger = logger;
            _studentFactory = studentFactory;
        }

        public bool Add(string emailAddress, Guid universityId)
        {
            _logger.Log(string.Format("Log: Start add student with email '{0}'", emailAddress));

            if (_studentRepository.Exists(emailAddress))
            {
                return false;
            }

            var university = _universityRepository.GetById(universityId);

            var student = _studentFactory.Create(emailAddress, universityId, university.Package);

            _studentRepository.Add(student);

            _logger.Log(string.Format("Log: End add student with email '{0}'", emailAddress));

            return true;
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
