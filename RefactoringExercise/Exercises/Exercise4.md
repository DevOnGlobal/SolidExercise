# Introduction

This exercise will be focussed on the Liskov Substitution Principle:
- Subtypes must be substitutable for their base types.

# Exercise 4
A new requirement introduces an "Unlimited package". A student with this package does not have a montly allowance limit.

Add the following UnlimitedStudent to your project:

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

- Examine the design of the Student and its derived classes and try to identify how introducing this new "Unlimited" student type would break LSP.
- Find a way to fix this.