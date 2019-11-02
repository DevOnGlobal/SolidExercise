#Answers exercise 5
This principle basically advocates for having small and focused interfaces that serve a specific set of clients rather than having big interfaces which all clients(callers) must use.

Let’s consider the IStudentService interface again.
```
public interface IStudentService
{
    //...
    bool Add(string emailAddress, Guid universityId);
    void AddBonusAllowances();
    IEnumerable<Student> GetUsersByUniversity();
    IEnumerable<Student> GetUsersByCurrentlyBorrowedEbooks();
    //...
}
```
We can logically group the operations of this interface and see that it performs both write operations and read operations. Although this is acceptable, there are areas of the application that only deal with reading data, like viewing reports. Likewise, there are areas of the application that are mainly concerned with data entry. Right now all these areas share the StudentService interface.
Although this example is kept simple on purpose, in real-life scenarios this interface will be bigger and will continue to grow. Therefore, according to the LSP, in this case a better design would be to split this interface according to the need of it’s clients.
```
public interface IStudentWriterService
{
    //...
    bool Add(string emailAddress, Guid universityId);
    void AddBonusAllowances();
    //...
}
 
public interface IStudentReaderService
{
    //...
    IEnumerable<Student> GetStudentsByUniversity();
    IEnumerable<Student> GetStudentsByCurrentlyBorrowedEbooks();
    //...
}
```

Bonus:

After we split the IStudentService interface, naturally the implementation split as well and now we have the following StudentWriterService:
```
public class StudentWriterService: IStudentWriterService
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
        foreach(LimitedStudent s in students)
        {               
            s.AddBonusAllowance();
        }           
    }       
}
```

Although now the code looks nice and tidy and follows the SOLID principles there’s still something not quite right about the Add method. It returns returns a value. This means it does more than one thing. This violates the Command-Query Separation principle because it both executes a command and also returns a value.

In this particular scenario returning a bool is not very helpful. If we add other business rules then we won’t be able to differentiate between the reasons why adding a student failed.

What about error codes instead of a bool? Going that route is still a violation of CQS and will force the caller to immediately deal with the error, usually with an if/switch statement, and implement a decision making mechanism which is very likely to break the OCP in the upper layer.

An alternative approach is to throw custom exceptions with meaningful descriptions. This can lead to more readable code in the caller as it makes it easier to separate the error handling functionality. For example, we could rewrite the Add method like this:

```
public void Add(string emailAddress, Guid universityId)
{           
    _logger.Log(string.Format("Log: Start add student with email '{0}'", emailAddress));            
 
    if (_studentRepository.Exists(emailAddress))
    {
        throw new DomainException("A user with the same e-mail address already exists.");
    }
 
    var university = _universityRepository.GetById(universityId);
     
    var student = _studentFactory.Create(emailAddress, universityId, university.Package);           
     
    _studentRepository.Add(student);
     
    _logger.Log(string.Format("Log: End add student with email '{0}'", emailAddress));          
}
```
