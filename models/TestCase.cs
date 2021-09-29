using System.Collections.Generic;

public class TestCase
{
  public List<Applicant> applicants { get; set; }
  public List<Institution> institutions { get; set; }

  public TestCase(List<Applicant> applicants, List<Institution> institutions)
  {
    this.applicants = applicants;
    this.institutions = institutions;
  }
}