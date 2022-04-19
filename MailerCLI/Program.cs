using Mailer;

const string receiver = "replace.me@bademail.com";

Task<int> mailStatus = null;



//Main Loop
while (true)
{
    string send, rec, sub, bod, cmd, to;
    Console.WriteLine("Awaiting instructions...");
    cmd = Console.ReadLine();
    if (cmd.Equals("exit"))
    {
        break;
    } else if (cmd.Equals("send")){
        Console.WriteLine("Enter sender name:");
        send = Console.ReadLine();
        Console.WriteLine("Enter recipient name:");
        to = Console.ReadLine();
        Console.WriteLine("Enter recipient address:");
        rec = Console.ReadLine();
        Console.WriteLine("Enter subject:");
        sub = Console.ReadLine();
        Console.WriteLine("Enter message body:");
        bod = Console.ReadLine();
        Console.WriteLine("To: "+ to + ", " + rec + "\nFrom: " + send + "\nSubject: " + sub + "\nBody: " + bod + "\nConfirm Y/N?");
        cmd = Console.ReadLine();
        if (cmd.ToLower().Equals("y")){
            mailStatus = Task.Run(() => Mailer.Mailer.SendEmail(send, to, rec, sub, bod));
            //Console.WriteLine("request sent");
        } else continue;
    } else if (cmd.Equals("qt"))
    {
        mailStatus = Task.Run(() => Mailer.Mailer.SendEmail("Rob Ott", "Emily", receiver, "Rapid Test Email", "The version of the test for sending a quick message without manual entry."));
        //Console.WriteLine("request sent");
    }
}
if(mailStatus != null) await mailStatus;
