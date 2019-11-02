using System;

namespace RefactoringExercise
{
    public class Student
    {
        public string EmailAddress { get; private set; }
        public Guid UniversityId { get; private set; }
        public int MonthlyEbookAllowance { get; set; }
        public int CurrentlyBorrowedEbooks { get; private set; }

        public Student(string emailAddress, Guid universityId)
        {
            this.EmailAddress = emailAddress;
            this.UniversityId = universityId;
        }
    }
}
