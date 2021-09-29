using System;
using System.Collections.Generic;

public class Applicant
{
  public string id { get; set; }
  public string name { get; set; }
  public List<Institution> rankedInstitutions { get; set; }

  public Applicant(string name, List<Institution> rankedInstitutions)
  {
    this.name = name;
    this.rankedInstitutions = rankedInstitutions;
    this.id = Guid.NewGuid().ToString();
  }
}