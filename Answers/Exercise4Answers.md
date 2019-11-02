#Answers exercise 4
Adding the unlimited student will result in a class comparable to:
```
public class UnlimitedStudent: Student
{
    public UnlimitedStudent(string emailAddress, Guid universityId)
        : base(emailAddress, universityId)
    {
        this.MonthlyEbookAllowance = 0;
    }
     
    public override void AddBonusAllowance()
    {
        throw new NotImplementedException();
    }
}
```
As there is no monthly allowance limit for this type of student, AddBonusAllowance doesn’t have an implementation. And because of base Student class forces it, this class now breaks the LSP.

We can no longer replace UnlimitedStudent with Student because this will have an unexpected result. Sure, we can make sure this never happens during run-time by filtering the students upon retrieval, or checking the type, or even leaving the AddBonusAllowance blank. But the design will still be inappropriate and misleading.

One way to fix this is to create another abstract class LimitedStudent which inherits the Student class and has the abstract AddBonusAllowance method. Then the StandardStudent and PremiumStudent can inherit this new class, while the UnlimitedStudent will inherit the modified Student class without needing to implement the AddBonusAllowance method.

Another way would be to extract AddBonusAllowance abstract method into an interface, IBonusAllowable, which only StandardStudent and PremiumStudent will implement. The first option is shown below.

```
public abstract class Student
{
    protected const int STANDARD_ALLOWANCE = 10;
 
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
 
public abstract class LimitedStudent : Student
{
    public LimitedStudent(string emailAddress, Guid universityId)
        : base(emailAddress, universityId)
    {}
    public abstract void AddBonusAllowance();
}
 
public class StandardStudent : LimitedStudent
{
    public StandardStudent(string emailAddress, Guid universityId) 
        : base(emailAddress, universityId)
    {
        this.MonthlyEbookAllowance = Student.STANDARD_ALLOWANCE;
    }
 
    public override void AddBonusAllowance()
    {
        this.MonthlyEbookAllowance += 5;
    }
}
 
public class PremiumStudent : LimitedStudent
{
    public PremiumStudent(string emailAddress, Guid universityId)
        : base(emailAddress, universityId)
    {
        this.MonthlyEbookAllowance = Student.STANDARD_ALLOWANCE * 2;
    }
 
    public override void AddBonusAllowance()
    {
        this.MonthlyEbookAllowance += 10;
    }
}
 
public class UnlimitedStudent: Student
{
    public UnlimitedStudent(string emailAddress, Guid universityId)
        : base(emailAddress, universityId)
    {
        this.MonthlyEbookAllowance = 0;
    }       
}
```

Now we can update the AddBonusAllowances method in the StudentService to increase allowances just for students of type LimitedStudent. Of course, the method which returns the students must also be updated to return the correct student abstraction (LimitedStudent).
```
public void AddBonusAllowances()
{
    //...get students (IEnumerable<LimitedStudent>) for bonus allowance       
    foreach(LimitedStudent s in students)
    {           
        s.IncreaseAllowance();
    }
    //...
}
```
