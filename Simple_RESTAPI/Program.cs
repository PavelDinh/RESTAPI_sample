using System;
using System.Data.SqlClient;
using System.Configuration;

/*
* Hlavní úkol
Popis problému
Imaginární obchod “Nejlepší oděvy” má ve své kamenné prodejně nainstalovánu monitorovací kameru,
která snímá jednotlivé zákazníky a dokáže k nim přiřadit věk, pohlaví a dle výrazu v obličeji určit, zdali byl
zákazník spokojen či nikoliv. Data z kamery by chtěl mít management uložena v cloudu MS Azure v
tabulce [recruit].[Customers], přičemž kompletní struktura tabulky je následující:
• [Id] - umělý klíč (autoinkrement)
• [VisitDateTime] - datum pořízení záznamu
• [Age] - věk zákazníka
• [WasSatisfied] - příznak, zdali byl spokojený či nikoliv (TRUE / FALSE)
• [Sex] - pohlaví (‘M’ - male, ‘F’ - female)
Zadání úkolu
Úkolem je naprogramovat jednoduché REST API, které bude umět komunikovat s výše zmiňovanou
tabulkou. API by mělo obsahovat:
1. GET metodu pro čtení (např. Read), která bude vracet obsah tabulky Customers.
a. Ideálně by mělo být možné, aby si uživatel mohl zvolit, za jaký časový interval chce, aby
se mu záznamy vrátily

2. POST metodu pro zápis (např. Insert), která bude do tabulky Customers zapisovat nová data.
a. Ideálně umožnit i batchový zápis více záznamů najednou

[BONUS - Nepovinný úkol 1]
• API by mělo obsahovat i Swagger pro dokumentaci a testování metod
[BONUS - Nepovinný úkol 2]
• Management obchodu by si přál vytvořit jednoduchý PowerBI report, který bude vizualizoval
obsah tabulky recruit.Customers. Report by měl přinést odpovědi na otázky:
o “Kolik procent zákazníků bylo celkově spokojeno?”
o “Je spokojenost zákazníka závislá na věku nebo pohlaví?”
• Designu a struktuře reportu se meze nekladou.

!! TEST ODEVZDAT ZDE: zamestnani@softim.cz !!
*/

namespace Simple_RESTAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Zadejte příkaz: ");
                    Console.WriteLine("'GET' - Informace z tabulky Customers.");
                    Console.WriteLine("'POST' - Zapsat nová data do tabulky Customers");
                    Console.Write("Příkaz: ");

                    string input = Console.ReadLine();

                    if (input.ToLower() == "get")
                    {
                        Console.WriteLine("Za jak dlouho v sekundách chcete načíst data ?");
                        Console.Write("Příkaz: ");
                        int timeDelay = int.Parse(Console.ReadLine());
                        System.Threading.Thread.Sleep(timeDelay * 1000);

                        using (SqlCommand Com = new SqlCommand("select Id, VisitDateTime, Age, WasSatisfied, Sex from recruit.Customers", con))
                        {
                            using (SqlDataReader reader = Com.ExecuteReader())
                            {
                                Console.WriteLine("\nCUSTOMERS");
                                Console.WriteLine("ID" + " | " + "VisitDateTime" + " | " + "Age" + " | " + "WasSatisfied" + " | " + "Sex" + " | ");

                                while (reader.Read())
                                {
                                    Console.WriteLine(reader["Id"].ToString() +
                                        " | " + reader["VisitDateTime"].ToString() +
                                        " | " + reader["Age"].ToString() +
                                        " | " + reader["WasSatisfied"].ToString() +
                                        " | " + reader["Sex"].ToString() +
                                        " | ");
                                }
                            }
                        }
                    }
                    else if (input.ToLower() == "post")
                    {
                        bool done = false;
                        Console.WriteLine("ZAPISOVANI DO TABULKY CUSTOMERS");

                        while (!done)
                        {
                            Console.Write("Věk: ");
                            int age = int.Parse(Console.ReadLine());

                            Console.Write("Spokojenost: ");
                            bool satisfied = bool.Parse(Console.ReadLine());

                            Console.Write("Pohlaví: ");
                            char Sex = char.Parse(Console.ReadLine());

                            using (SqlCommand Com = new SqlCommand("insert into recruit.Customers values (@VisitDateTime, @Age, @WasSatisfied, @Sex) ", con))
                            {
                                DateTime date = DateTime.Now;
                                Com.Parameters.AddWithValue("@VisitDateTime", date);
                                Com.Parameters.AddWithValue("@Age", age);
                                Com.Parameters.AddWithValue("@WasSatisfied", satisfied);
                                Com.Parameters.AddWithValue("@Sex", Sex);

                                Com.ExecuteNonQuery();
                            }
                            bool rep = false;
                            while (!rep)
                            {
                                Console.WriteLine("Přidat další záznam ?(ano/ne)");
                                string inp = Console.ReadLine();

                                if (inp.ToLower() == "ne")
                                {
                                    done = true;
                                    rep = true;
                                }else if(inp.ToLower() == "ano")
                                {
                                    rep = true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
