using System;

namespace RefactoringExercise
{
    public class Student
    {
        private const int STANDARD_ALLOWANCE = 10;

        public string EmailAddress { get; private set; }
        public Guid UniversityId { get; private set; }
        public int MonthlyEbookAllowance { get; set; }
        public int CurrentlyBorrowedEbooks { get; private set; }

        public Student(string emailAddress, Guid universityId, Package package)
        {
            this.EmailAddress = emailAddress;
            this.UniversityId = universityId;

            if (package == Package.Standard)
            {
                MonthlyEbookAllowance = STANDARD_ALLOWANCE;
            }
            else if (package == Package.Premium)
            {
                MonthlyEbookAllowance = STANDARD_ALLOWANCE * 2;
            }
        }
    }
}
