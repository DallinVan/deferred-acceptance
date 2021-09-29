using System;
using System.Collections.Generic;

namespace deferred_acceptance
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // RunTrivialMatch();
                // RunSimpleMatch();
                RunTestMatch();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.StackTrace}");
            }
        }

        static void RunTrivialMatch()
        {
            TestCase testCase = DeferredAcceptanceTestCaseBuilder.TrivialTestCase();
            var unmatchedApplicants = new List<Applicant>();
            DeferredAcceptance.ComputeMatches(testCase.applicants, testCase.institutions, out unmatchedApplicants);
            DeferredAcceptance.PrintResults(testCase.institutions, unmatchedApplicants);
        }

        static void RunSimpleMatch()
        {
            TestCase testCase = DeferredAcceptanceTestCaseBuilder.SimpleTestCase();
            var unmatchedApplicants = new List<Applicant>();
            DeferredAcceptance.ComputeMatches(testCase.applicants, testCase.institutions, out unmatchedApplicants);
            DeferredAcceptance.PrintResults(testCase.institutions, unmatchedApplicants);
        }

        static void RunTestMatch()
        {
            TestCase testCase = DeferredAcceptanceTestCaseBuilder.TestCase();
            var unmatchedApplicants = new List<Applicant>();
            DeferredAcceptance.ComputeMatches(testCase.applicants, testCase.institutions, out unmatchedApplicants);
            DeferredAcceptance.PrintResults(testCase.institutions, unmatchedApplicants);
        }
    }
}
