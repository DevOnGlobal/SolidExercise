#Exercise 1 answers
The first rule is easier to understand. The high level modules are the ones that contain the core (use-cases and the the business logic) of the application. Low level modules are modules that perform very specific operations (often infrastructure related) which are orchestrated by the higher level modules.

Looking at our example, it’s obvious that the StudentService is part of a high level module and it depends on the StudentRepository which persists students to the database and belongs to a low level module. It’s also clear that neither of them depend on abstractions.

In order to satisfy the first rule it’s enough to do something a lot of .NET developers are already familiar with. We put both classes behind interfaces (abstractions) and inject the lower level abstraction, the new IStudentRepository, into the StudentService constructor. This is Dependency Injection but it’s enough to satisfy the DIP.

But what about the second rule?

The second rule is not as easy to understand because it doesn’t have a direct reflection in code. It’s more about perspective and who triggers the change in the abstraction-details relationship. In our example, the IStudentRepository is the abstraction of the StudentRepository detail. In order to satisfy this rule any change in the StudentRepository should be triggered by a change in the IStudentRepository.

To achieve this it helps to think that the IStudentRepository abstraction is in the same higher level module as StudentService. Practically, this would translate to placing IStudentRepository in the same project (higher level module) as the StudentService, while StudentRepository will be placed in a separate project (lower level module).

```
public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUniversityRepository _universityRepository;
 
    public StudentService(IStudentRepository studentRepository, IUniversityRepository universityRepository)
    {
        _studentRepository = studentRepository;
        _universityRepository = universityRepository;
    }
 
    public bool Add(string emailAddress, Guid universityId)
    {       
        Console.WriteLine(string.Format("Log: Start add student with email '{0}'", emailAddress));
 
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            return false;
        }           
 
        if (_studentRepository.Exists(emailAddress))
        {
            return false;
        }           
 
        var university = _universityRepository.GetById(universityId);
 
        var student = new Student(emailAddress, universityId);
                 
        if (university.Package == Package.Standard)
        {
            student.MonthlyEbookAllowance = 10;
        }
        else if (university.Package == Package.Premium)
        {
            student.MonthlyEbookAllowance = 10 * 2;
        }                           
         
        _studentRepository.Add(student);
         
        Console.WriteLine(string.Format("Log: End add student with email '{0}'", emailAddress));
 
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
```

