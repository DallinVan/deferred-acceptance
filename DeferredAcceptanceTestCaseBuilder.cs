using System;
using System.Collections.Generic;

public class DeferredAcceptanceTestCaseBuilder
{
  public static TestCase TrivialTestCase()
  {
    var applicants = new List<Applicant>
    {
      new Applicant("Dallin", null)
    };

    var institutions = new List<Institution>
    {
      new Institution("Utah", null, 1)
    };

    applicants[0].rankedInstitutions = new List<Institution> { InstitutionWithName(institutions, "Utah") };
    institutions[0].rankedApplicants = new List<Applicant> { ApplicantWithName(applicants, "Dallin") };

    return new TestCase(applicants, institutions);
  }

  public static TestCase SimpleTestCase()
  {
    var applicants = new List<Applicant>
    {
      new Applicant("Dallin", null),
      new Applicant("Andrew", null)
    };

    var institutions = new List<Institution>
    {
      new Institution("Carolinas", null, 1),
      new Institution("Utah", null, 1),
    };

    ApplicantWithName(applicants, "Andrew").rankedInstitutions = new List<Institution> { InstitutionWithName(institutions, "Utah"), InstitutionWithName(institutions, "Carolinas") };
    ApplicantWithName(applicants, "Dallin").rankedInstitutions = new List<Institution> { InstitutionWithName(institutions, "Utah") };

    InstitutionWithName(institutions, "Utah").rankedApplicants = new List<Applicant> { ApplicantWithName(applicants, "Dallin"), ApplicantWithName(applicants, "Andrew") };
    InstitutionWithName(institutions, "Carolinas").rankedApplicants = new List<Applicant> { ApplicantWithName(applicants, "Andrew") };

    return new TestCase(applicants, institutions);
  }

  public static TestCase TestCase()
  {
    var applicants = new List<Applicant>
    {
      new Applicant("Arthur", null),
      new Applicant("Sunny", null),
      new Applicant("Joseph", null),
      new Applicant("Latha", null),
      new Applicant("Darrius", null)
    };

    var institutions = new List<Institution>
    {
      new Institution("Mercy", null, 2),
      new Institution("City", null, 2),
      new Institution("General", null, 2),
    };

    ApplicantWithName(applicants, "Arthur").rankedInstitutions = new List<Institution>
    {
      InstitutionWithName(institutions, "City")
    };
    ApplicantWithName(applicants, "Sunny").rankedInstitutions = new List<Institution>
    {
      InstitutionWithName(institutions, "City"),
      InstitutionWithName(institutions, "Mercy")
    };
    ApplicantWithName(applicants, "Joseph").rankedInstitutions = new List<Institution>
    {
      InstitutionWithName(institutions, "City"),
      InstitutionWithName(institutions, "General"),
      InstitutionWithName(institutions, "Mercy")
    };
    ApplicantWithName(applicants, "Latha").rankedInstitutions = new List<Institution>
    {
      InstitutionWithName(institutions, "Mercy"),
      InstitutionWithName(institutions, "City"),
      InstitutionWithName(institutions, "General")
    };
    ApplicantWithName(applicants, "Darrius").rankedInstitutions = new List<Institution>
    {
      InstitutionWithName(institutions, "City"),
      InstitutionWithName(institutions, "Mercy"),
      InstitutionWithName(institutions, "General")
    };

    InstitutionWithName(institutions, "Mercy").rankedApplicants = new List<Applicant>
    {
      ApplicantWithName(applicants, "Darrius"),
      ApplicantWithName(applicants, "Joseph")
    };
    InstitutionWithName(institutions, "City").rankedApplicants = new List<Applicant>
    {
      ApplicantWithName(applicants, "Darrius"),
      ApplicantWithName(applicants, "Arthur"),
      ApplicantWithName(applicants, "Sunny"),
      ApplicantWithName(applicants, "Latha"),
      ApplicantWithName(applicants, "Joseph")
    };
    InstitutionWithName(institutions, "General").rankedApplicants = new List<Applicant>
    {
      ApplicantWithName(applicants, "Darrius"),
      ApplicantWithName(applicants, "Arthur"),
      ApplicantWithName(applicants, "Joseph"),
      ApplicantWithName(applicants, "Latha"),
    };
    

    return new TestCase(applicants, institutions);
  }

  public static Institution InstitutionWithName(List<Institution> institutions, string name)
  {
    var institution = institutions.Find(i => i.name == name);
    if (institution == null)
    {
      throw new Exception($"InstitutionWithName: Institution not found for name '{name}'");
    }
    return institution;
  }

  public static Applicant ApplicantWithName(List<Applicant> applicants, string name)
  {
    var applicant = applicants.Find(i => i.name == name);
    if (applicant == null)
    {
      throw new Exception($"ApplicationWithName: Applicant not found for name '{name}'");
    }
    return applicant;
  }
}