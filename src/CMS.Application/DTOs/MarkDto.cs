namespace CMS.Application.DTOs;

public class MarkDto
{
    public int MarkId { get; set; }
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CourseName { get; set; }
    public string? SemOrYear { get; set; }
    
    // Subject 1
    public string? Subject1 { get; set; }
    public string? Subject1MaxTheoryMarks { get; set; }
    public string? Subject1TheoryMarks { get; set; }
    public string? Subject1MaxPracticalMarks { get; set; }
    public string? Subject1PracticalMarks { get; set; }
    
    // Subject 2
    public string? Subject2 { get; set; }
    public string? Subject2MaxTheoryMarks { get; set; }
    public string? Subject2TheoryMarks { get; set; }
    public string? Subject2MaxPracticalMarks { get; set; }
    public string? Subject2PracticalMarks { get; set; }
    
    // Subject 3
    public string? Subject3 { get; set; }
    public string? Subject3MaxTheoryMarks { get; set; }
    public string? Subject3TheoryMarks { get; set; }
    public string? Subject3MaxPracticalMarks { get; set; }
    public string? Subject3PracticalMarks { get; set; }
    
    // Subject 4
    public string? Subject4 { get; set; }
    public string? Subject4MaxTheoryMarks { get; set; }
    public string? Subject4TheoryMarks { get; set; }
    public string? Subject4MaxPracticalMarks { get; set; }
    public string? Subject4PracticalMarks { get; set; }
    
    // Subject 5
    public string? Subject5 { get; set; }
    public string? Subject5MaxTheoryMarks { get; set; }
    public string? Subject5TheoryMarks { get; set; }
    public string? Subject5MaxPracticalMarks { get; set; }
    public string? Subject5PracticalMarks { get; set; }
    
    // Subject 6
    public string? Subject6 { get; set; }
    public string? Subject6MaxTheoryMarks { get; set; }
    public string? Subject6TheoryMarks { get; set; }
    public string? Subject6MaxPracticalMarks { get; set; }
    public string? Subject6PracticalMarks { get; set; }
    
    // Totals
    public string? TotalMaxTheoryMarks { get; set; }
    public string? TotalTheoryMarks { get; set; }
    public string? TotalMaxPracticalMarks { get; set; }
    public string? TotalPracticalMarks { get; set; }
}