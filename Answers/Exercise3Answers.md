#Answers exercise 3
If we look at the Student class we can easily see that the constructor needs to change each time we add a new Package.

Another problem is that Package is a concept belonging to University. The Student should only care about the monthly allowance. AddBonusAllowance also has to check the current Package in order to increase the allowance accordingly.
As it stands, if there is a need to add another type of Package then we’ll have to modify both the constructor and the AddBonusAllowance method, and any other method we may have that depends on the Package.

To make the Student class adhere to the OCP we will create an abstract Student and two derived classes StandardStudent and PremiumStudent.
With this design each Student class handles its own allowance and does not directly depend on Package. Now that we have these classes, someone still has to create the appropriate object based on the Package. For this purpose we can create a Simple Factory which returns the appropriate type of Student. This factory will then be used by the StudentService to create a Student.

Although the Student class is now adhering to the OCP it seems that the original problem was passed to the StudentFactory. That is true but now the code that has to change is localized in a class specifically built for creating Student objects. Additionally, now I only have to change one method in case a new Package is added. It’s a good trade-off.

And of course, there are techniques to completely remove the switch statement. We could store <Package, class> pairs in a dictionary and then use reflection to instantiate the proper Student. Or, without using reflection, we can have <Package, SpecialisedFactory> pairs to retrieve a specialised factory that knows to create its own Student object. We're not going to implement these techniques here because in this context the added complexity outweighs the benefits but it’s worth knowing there are further options.

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
 
    public abstract void AddBonusAllowance();   
}
```
```
public class StandardStudent : Student
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
```
```
public class PremiumStudent : Student
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
```
```
public class StudentFactory : IStudentFactory
{
    public Student Create(string emailAddress, Guid universityId, Package package)
    {
        switch(package)
        {
            case Package.Standard: return new StandardStudent(emailAddress, universityId); break;
            case Package.Premium: return new PremiumStudent(emailAddress, universityId); break;
            default: throw new NotImplementedException("There is no associated student for this package");
        }
    }
}
```
```
public class StudentService : IStudentService
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
        //...get students (IEnumerable<Student>) for bonus allowance
        foreach (Student s in students)
        {
            s.AddBonusAllowance();
        }
        //...
    }
     
    public IEnumerable<Student> GetStudentsByUniversity()
    {
        //...       
    }
 
    public IEnumerable<Student> GetStudentsByCurrentlyBorrowedEbooks()
    {
        //...       
    }
}