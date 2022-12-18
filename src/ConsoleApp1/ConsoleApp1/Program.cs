using SimpleJobs.Brazil.BrasilAPI;

Console.WriteLine("Hello, World!\n");

BrasilAPICore brasilAPI = new();
ResponseBase<IList<Bank>> response = (ResponseBase<IList<Bank>>)brasilAPI.GetBanks().Result;

if (response.Content != null)
    foreach (Bank bank in response.Content)
    {
        Console.WriteLine(bank.FullName);
    }

Console.ReadLine();
