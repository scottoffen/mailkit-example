# Using MailKit with Gmail in .netcore 2.x #

- [MailKit Officially Replaces .NET’s SmtpClient](https://www.infoq.com/news/2017/04/MailKit-MimeKit-Official)

- [MailKit on GitHub](https://github.com/jstedfast/MailKit)

- [MailKit and Gmail](https://github.com/jstedfast/MailKit/blob/master/FAQ.md#GMailAccess)

- [Cerberus (HTML Email)](https://tedgoas.github.io/Cerberus/)

## Usage ##

While logged into your Google account, [enable less secure apps](https://www.google.com/settings/security/lesssecureapps).

Go into the [`Constants`](http://github.extendhealth.com/scooffe/MailkitExample/blob/master/MailkitExample/Constants.cs) class and change the variables there to match your Gmail credentails.

Add the appropriate people to the [`Recipents` list](http://github.extendhealth.com/scooffe/MailkitExample/blob/master/MailkitExample/Program.cs#L25).

Edit the [`template.html`](http://github.extendhealth.com/scooffe/MailkitExample/blob/master/MailkitExample/template.html) file to change the email output. You can preview the output by viewing the `output.html` and `output.eml` files that are generated when the program is run.

When you are ready to actually send the email, update the [`SendEmail`](http://github.extendhealth.com/scooffe/MailkitExample/blob/master/MailkitExample/Program.cs#L19) variable and run the program again.