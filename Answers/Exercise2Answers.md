#Answers exercise 2
Looking at the Add method we can see a couple of reasons to change that may or may not be part of the actual purpose of this method:

1. The method starts and ends by logging the student email address. Even if we can assume that this method is expected to log each call we can definitely say it’s not its responsibility to know the internals of logging. In other words, the method is expected to log but shouldn’t care or be affected by how the logging is made.
We can fix this by extracting the logging details into a separate Logger class, putting that class behind an ILogger interface and injecting the interface into the class constructor. In this case we’re essentially applying DIP to fix SRP.

2. Next we can see two types validations.
The first one checks if the student’s email is not empty. This is input validation. It’s similar to the logging scenario above but in this case I could argue that this type of validation can be performed in a different class from an upper layer (think Data Annotations in ASP.NET MVC or Fluent Validation). Therefore, we can remove this from the Add method.
The next type of validation checks if the there is an existing user with the same email address before adding a new user. This is business validation and is based on the business rule that ‘Each student must have an unique email address’. Therefore this is a true responsibility because changing this should definitely change the Add method.

3. Finally, the method creates a Student object and then sets the monthly allowance based on the Package purchased by the University. While it seems reasonable for the method to create a new Student, at the same time it shouldn’t be concerned or affected by the criteria on which the monthly allowance is set. This seems more like the responsibility of the Student so we’ll update its constructor by adding the Package to its signature and moving the allowance logic inside.

```
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
    }
 
    public IEnumerable<Student> GetStudentsByCurrentlyBorrowedEbooks()
    {
        //...
    }
}
```
```
public class Student
{
    private const int STANDARD_ALLOWANCE = 10;
 
    public string EmailAddress { get; private set; }
    public Guid UniversityId { get; private set; }
    public int MonthlyEbookAllowance { get; private set; }
    public int CurrentlyBorrowedEbooks { get; private set; }
 
    public Student(string emailAddress, Guid universityId, Package package)
    {
        this.EmailAddress = emailAddress;
        this.UniversityId = universityId;
 
        if (package == Package.Standard)
        {
            this.MonthlyEbookAllowance = STANDARD_ALLOWANCE;
        }
        else if (package == Package.Premium)
        {
            this.MonthlyEbookAllowance = STANDARD_ALLOWANCE * 2;
        }                           
    }   

    public void AddBonusAllowance(Package package)
    {
        if (package == Package.Standard)
        {
            this.MonthlyEbookAllowance += 5; ;
        }
        else if (package == Package.Premium)
        {
            this.MonthlyEbookAllowance += 10; ;
        }
    }    
}