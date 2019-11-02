using System;

namespace RefactoringExercise
{
    public class University
    {
        public Guid Id { get; private set; }
        public String Name { get; private set; }
        public Package Package { get; private set; }
    }
}