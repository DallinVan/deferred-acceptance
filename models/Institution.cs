using System;
using System.Collections.Generic;

public class Institution
{
  public int capacity { get; set; }
  public string name { get; set; }
  public List<Applicant> rankedApplicants { get; set; }
  public List<Applicant> acceptedApplicants { get; set; }

  public Institution(string name, List<Applicant> rankedApplicants, int capacity)
  {
    this.name = name;
    this.rankedApplicants = rankedApplicants;
    this.capacity = capacity;
    this.acceptedApplicants = new List<Applicant>();
  }

  public bool AttemptToAcceptApplicant(Applicant applicant, out Applicant applicantRemoved)
  {
    applicantRemoved = null;
    if (capacity > acceptedApplicants.Count && ApplicantIsRanked(applicant))
    {
      acceptedApplicants.Add(applicant);
      return true;
    }
    else if (ApplicantIsPreferredOverCurrentlyAcceptedApplicants(applicant))
    {
      applicantRemoved = RemoveLowestRankedAcceptedApplicant();
      acceptedApplicants.Add(applicant);
      return true;
    }

    return false;
  }

  private Applicant RemoveLowestRankedAcceptedApplicant()
  {
    Applicant lowestRankedApplicant = acceptedApplicants.Count > 0 ? acceptedApplicants[0] : null;
    int lowestRank = -1;
    foreach (Applicant app in acceptedApplicants)
    {
      if (lowestRankedApplicant != null)
      {
        var appRank = ApplicantRank(app);
        if (appRank > lowestRank)
        {
          lowestRankedApplicant = app;
          lowestRank = appRank;
        }
      }
    }

    acceptedApplicants.Remove(lowestRankedApplicant);

    return lowestRankedApplicant;
  }

  private bool ApplicantIsRanked(Applicant applicant)
  {
    return rankedApplicants.Find(a => a == applicant) != null;
  }

  private bool ApplicantIsPreferredOverCurrentlyAcceptedApplicants(Applicant applicant)
  {
    if (!ApplicantIsRanked(applicant))
    {
      return false;
    }

    int applicantRank = ApplicantRank(applicant);
    foreach(Applicant app in acceptedApplicants)
    {
      int acceptedApplicantRank = ApplicantRank(app);
      if (applicantRank < acceptedApplicantRank) // The lower the rank/index, higher the preference (0 is best, 2 is better than 4, etc)
      {
        return true;
      }
    }

    return false;
  }

  private int ApplicantRank(Applicant applicant)
  {
    return rankedApplicants.FindIndex(a => a == applicant);
  }

  public override string ToString()
  {
    var output = $"{name}: ";
    foreach(var app in acceptedApplicants)
    {
      output += $"{app.name}, ";
    }

    return output;
  }
}