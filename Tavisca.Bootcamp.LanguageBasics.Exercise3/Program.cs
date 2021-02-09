using System;
using System.Linq;
using System.Collections.Generic;


namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int[] result = new int[dietPlans.Length];

            int[] calorie = new int[protein.Length];
            List<int> MaxCalorie = new List<int>();
            List<int> MaxProtein = new List<int>();
            List<int> MaxCarbs = new List<int>();
            List<int> MaxFat = new List<int>();

            List<int> MinCalorie = new List<int>();
            List<int> MinProtein = new List<int>();
            List<int> MinCarbs = new List<int>();
            List<int> MinFat = new List<int>();

            int MaxIndP = 0;
            int MaxIndC = 0;
            int MaxIndF = 0;
            int MaxIndCal = 0;

            int MinIndP = 0;
            int MinIndC = 0;
            int MinIndF = 0;
            int MinIndCal = 0;

            for (int i = 0; i < protein.Length; ++i)
            {
                calorie[i] = (9 * fat[i] + 5 * (protein[i] + carbs[i]));
            }
            for (int i = 0; i < protein.Length; ++i)
            {
                if (protein[MaxIndP] < protein[i])
                {
                    MaxIndP = i;
                }
                if (carbs[MaxIndC] < carbs[i])
                {
                    MaxIndC = i;
                }
                if (fat[MaxIndF] < fat[i])
                {
                    MaxIndF = i;
                }
                if (calorie[MaxIndCal] < calorie[i])
                {
                    MaxIndCal = i;
                }
            }
            for (int i = 0; i < protein.Length; ++i)
            {
                if (protein[MinIndP] > protein[i])
                {
                    MinIndP = i;
                }
                if (carbs[MinIndC] > carbs[i])
                {
                    MinIndC = i;
                }
                if (fat[MinIndF] > fat[i])
                {
                    MinIndF = i;
                }
                if (calorie[MinIndCal] > calorie[i])
                {
                    MinIndCal = i;
                }
            }

            for (int i = 0; i < protein.Length; ++i)
            {
                if (protein[MaxIndP] == protein[i])
                {
                    MaxProtein.Add(i);
                }
                if (carbs[MaxIndC] == carbs[i])
                {
                    MaxCarbs.Add(i);
                }
                if (fat[MaxIndF] == fat[i])
                {
                    MaxFat.Add(i);
                }
                if (calorie[MaxIndCal] == calorie[i])
                {
                    MaxCalorie.Add(i);
                }
            }
            for (int i = 0; i < protein.Length; ++i)
            {
                if (protein[MinIndP] == protein[i])
                {
                    MinProtein.Add(i);
                }
                if (carbs[MinIndC] == carbs[i])
                {
                    MinCarbs.Add(i);
                }
                if (fat[MinIndF] == fat[i])
                {
                    MinFat.Add(i);
                }
                if (calorie[MinIndCal] == calorie[i])
                {
                    MinCalorie.Add(i);
                }
            }

            int sind = 0;
            foreach (string dietPlan in dietPlans)
            {
                int[] AnsArray = new int[50];
                for (int i = 0; i < 50; ++i)
                    AnsArray[i] = 0;
                for (int i = 0; i < dietPlan.Length; ++i)
                {
                    if (dietPlan[i] == 'p')
                    {
                        int cnt = 0;
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MinProtein.Count; ++k)
                            {
                                AnsArray[MinProtein[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMinElements(TempList, protein);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                    if (dietPlan[i] == 'P')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MaxProtein.Count; ++k)
                            {
                                AnsArray[MaxProtein[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMaxElements(TempList, protein);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                        
                    }
                    if (dietPlan[i] == 'c')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MinCarbs.Count; ++k)
                            {
                                AnsArray[MinCarbs[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMinElements(TempList, carbs);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                    if (dietPlan[i] == 'C')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MaxCarbs.Count; ++k)
                            {
                                AnsArray[MaxCarbs[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMaxElements(TempList, carbs);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                    if (dietPlan[i] == 'f')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MinFat.Count; ++k)
                            {
                                AnsArray[MinFat[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMinElements(TempList, fat);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                    if (dietPlan[i] == 'F')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MaxFat.Count; ++k)
                            {
                                AnsArray[MaxFat[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMaxElements(TempList, fat);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                    if (dietPlan[i] == 't')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MinCalorie.Count; ++k)
                            {
                                AnsArray[MinCalorie[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMinElements(TempList, calorie);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                    if (dietPlan[i] == 'T')
                    {
                        int cnt = 0;  
                        for(int h = 0; h < 50; ++h)
                        {
                            if(AnsArray[h] == 1)
                                cnt++;
                        }
                        if(cnt == 1)
                            break;
                        else if(cnt == 0)
                        {
                            for (int k = 0; k < MaxCalorie.Count; ++k)
                            {
                                AnsArray[MaxCalorie[k]] = 1;
                            }
                        }
                        else
                        {
                            List<int> TempList = new List<int>();
                            for(int l = 0; l < 50; ++l)
                            {
                                if(AnsArray[l] == 1)
                                    TempList.Add(l);
                            }
                            TempList = Program.FindMaxElements(TempList, calorie);
                            for(int h = 0, j = 0; h < 50 && j < TempList.Count; h++)
                            {
                                if(TempList[j] == h)
                                {
                                    AnsArray[h] = 1;
                                    ++j;
                                }
                                else
                                {
                                    AnsArray[h] = 0;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < 50; ++i)
                {
                    if (AnsArray[i] == 1)
                    {
                        result[sind] = i;
                        break;
                    }
                }
                sind++;

            }

            System.Console.Write("[ ");
            for (int i = 0; i < result.Length; ++i)
            {
                System.Console.Write(result[i] + ", ");
            }
            System.Console.Write("] \n");
            return result;
            throw new NotImplementedException();
        }


        static List<int> FindMaxElements(List<int> GivenList, int[] ActualList)
        {
            int maxInd = GivenList[0];
            for(int i = 1; i < GivenList.Count; ++i )
            {
                if(ActualList[maxInd] < ActualList[GivenList[i]])
                {
                    maxInd = GivenList[i];
                }
            }
            List<int> newList = new List<int>();
            for(int i = 0; i < GivenList.Count; ++i)
            {
                if(ActualList[maxInd] == ActualList[GivenList[i]])
                {
                    newList.Add(GivenList[i]);
                }
            }
            return newList;
        }
        static List<int> FindMinElements(List<int> GivenList, int[] ActualList)
        {
            int minInd = GivenList[0];
            for(int i = 1; i < GivenList.Count; ++i )
            {
                if(ActualList[minInd] > ActualList[GivenList[i]])
                {
                    minInd = GivenList[i];
                }
            }
            List<int> newList = new List<int>();
            for(int i = 0; i < GivenList.Count; ++i)
            {
                if(ActualList[minInd] == ActualList[GivenList[i]])
                {
                    newList.Add(GivenList[i]);
                }
            }
            return newList;
        }
        public static void PrintArray(List<int> arry)
        {
            System.Console.Write("[ ");
            foreach (var cal in arry)
            {
                System.Console.Write(cal + ", ");
            }
            System.Console.Write("] \n");
        }
        public static void PrintArray(int[] arry)
        {
            System.Console.Write("[ ");
            foreach (var cal in arry)
            {
                System.Console.Write(cal + ", ");
            }
            System.Console.Write("] \n");
        }
    }
}
