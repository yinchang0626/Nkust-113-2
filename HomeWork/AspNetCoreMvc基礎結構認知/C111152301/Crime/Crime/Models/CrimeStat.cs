public class CrimeStat
{
    public int Id { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int TotalCases { get; set; }
    public int RapeCases { get; set; }
    public int RobberyCases { get; set; }
    public int OtherCases { get; set; }
    public double IncidentRate { get; set; }
    public int Suspects { get; set; }
    public double CrimeRate { get; set; }
    public int SolvedTotal { get; set; }
    public int SolvedCurrent { get; set; }
    public int SolvedBacklog { get; set; }
    public int SolvedOther { get; set; }
    public double SolveRate { get; set; }
}