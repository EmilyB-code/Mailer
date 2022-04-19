appsettings.json needs account credentials, mail server URL and port filled in with what you're using for it.
the constant "receiver" in Program.cs should be changed to a valid email address

The command line interface has 3 commands
qt - sends a pre-defined email. Allows multiple messages to be sent quickly, since filling one out manually takes longer than the function call to send it.
send - steps through the message fields taking a line of input for each, then shows the message to verify before sending
exit - exits the application
