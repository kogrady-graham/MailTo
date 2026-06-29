using System;
using System.IO;
using System.Net;

// Creates an email using a body, subject, and recpient arguments
// Based off of users default mail app instead of notes
// Usage: "Usage: mailto.exe 'bodyfile' 'subject' 'recipient' "

namespace MailTo
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Checks if the arguments were inputted correctly
                if (args.Length < 3)
                    throw new Exception("Usage: mailto.exe 'bodyfile' 'subject' 'recipient' ");

                string bodyFile = args[0];
                string subject = args[1];
                string recipient = args[2];

                // Checks if the body file exists
                if (!File.Exists(bodyFile))
                    throw new Exception($"Body file not found: {bodyFile}");

                // Reads the body from the file to the body variable
                string body = File.ReadAllText(bodyFile);

                //formats the mailto string with the recipient, subject, and body
                string mailto = string.Format(
                    "mailto:{0}?subject={1}&body={2}",
                    Uri.EscapeDataString(recipient),
                    Uri.EscapeDataString(subject),
                    Uri.EscapeDataString(body)
                    );

                // Opens the default mail client with the formatted mailto string
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(mailto) { 
                    FileName = mailto,
                    UseShellExecute = true
                });

                // Deletes the body file after the email is created
                File.Delete(bodyFile);

            }

            //prints the exception to the console if there is an error
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }
        }
    }
}