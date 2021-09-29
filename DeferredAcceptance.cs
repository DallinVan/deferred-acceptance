using System;
using System.Collections.Generic;

public class DeferredAcceptance {
  /// <summary>
  /// This is a brute force implementation of the deferred acceptance algorithm without any optimizations
  /// </summary>
  /// <param name="applicants"></param>
  /// <param name="institutions"></param>
  /// <param name="unmatchedApplicants"></param>
  public static void ComputeMatches(List<Applicant> applicants, List<Institution> institutions, out List<Applicant> unmatchedApplicants)
  {
    var applicantsToPlace = new List<Applicant>(applicants);

    while (true)
    {
      var applicantsNotPlaced = new List<Applicant>();
      var applicantsPlaced = new List<Applicant>();

      // Attempt to place each applicant
      foreach (var applicant in applicantsToPlace)
      {
        Applicant displacedApplicant = null;
        var applicantPlaced = AttemptToPlaceApplicant(applicant, out displacedApplicant);
        if (applicantPlaced)
        {
          applicantsPlaced.Add(applicant);
        }
        else 
        {
          applicantsNotPlaced.Add(applicant);
        }
        if (displacedApplicant != null)
        {
          applicantsNotPlaced.Add(displacedApplicant);
        }
      }

      applicantsPlaced.ForEach(appPlaced => applicantsToPlace.Remove(appPlaced));

      unmatchedApplicants = applicantsNotPlaced;

      // Check if matching is complete/stable
      if (applicantsNotPlaced.Count == 0)
      {
        break;
      }
      else if (AreEqual(applicantsToPlace, applicantsNotPlaced))
      {
        break;
      }

      // Add any displaced applicants back into the mix to see if they match elsewhere on the next iteration
      applicantsNotPlaced.ForEach(a => {
        if (!applicantsToPlace.Contains(a))
        {
          applicantsToPlace.Add(a);
        }
      });
    }
  }

  public static bool AttemptToPlaceApplicant(Applicant applicant, out Applicant displacedApplicant)
  {
    displacedApplicant = null;

    foreach (Institution institution in applicant.rankedInstitutions)
    {
      bool applicantPlaced = institution.AttemptToAcceptApplicant(applicant, out displacedApplicant);
      if (applicantPlaced) {
        return true;
      }
    }

    return false;
  }

  private static bool AreEqual(List<Applicant> list1, List<Applicant> list2)
  {
    if (list1.Count != list2.Count)
    {
      return false;
    }

    list1 = new List<Applicant>(list1);
    list2 = new List<Applicant>(list2);

    var comparer = new ApplicantComparer();
    list1.Sort(comparer);
    list2.Sort(comparer);

    for (var i = 0; i < list1.Count; i++)
    {
      if (list1[i]?.id != list2[i]?.id)
      {
        return false;
      }
    }

    return true;
  }

  public static void PrintResults(List<Institution> institutions, List<Applicant> unmatchedApplicants)
  {
    Console.WriteLine();

    Console.WriteLine("=== Institutions ===");
    institutions.ForEach(i => Console.WriteLine(i.ToString()));

    Console.WriteLine();

    Console.WriteLine("=== Unmatched Applicants ===");
    var unmatchedApplicantNames = unmatchedApplicants.Count == 0 ? "None" : "";
    unmatchedApplicants.ForEach(a => unmatchedApplicantNames += $"{a.name}, ");
    Console.WriteLine(unmatchedApplicantNames);

    Console.WriteLine();
  }

}

class ApplicantComparer : IComparer<Applicant>
{
  public int Compare(Applicant x, Applicant y)
  {
    return string.Compare(x.id, y.id);
  }
}