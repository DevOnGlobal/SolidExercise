using System;

namespace RefactoringExercise
{
    public abstract class Student
    {
        protected const int STANDARD_ALLOWANCE = 10;

        public string EmailAddress { get; private set; }
        public Guid UniversityId { get; private set; }
        public int MonthlyEbookAllowance { get; set; }
        public int CurrentlyBorrowedEbooks { get; private set; }

        protected Student(string emailAddress, Guid universityId)
        {
            this.EmailAddress = emailAddress;
            this.UniversityId = universityId;
        }

        public abstract void AddBonusAllowance();

    }
}
