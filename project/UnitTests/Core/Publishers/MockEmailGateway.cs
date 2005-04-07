using System.Collections;
using System.Web.Mail;
using ThoughtWorks.CruiseControl.Core.Publishers;

namespace ThoughtWorks.CruiseControl.UnitTests.Core.Publishers
{
	public class MockEmailGateway : EmailGateway
	{
		private ArrayList _sentMessages = new ArrayList();
		private string _mailhost = "mock.gateway.org";

		public override string MailHost
		{
			get { return _mailhost; }
			set { _mailhost = value; }
		}

		public override void Send(string from, string to, string subject, string message)
		{
			MailMessage email = new MailMessage();
			email.From = from;
			email.To = to;
			email.Subject = subject;
			email.Body = message;

			_sentMessages.Add(email);
		}

		public IList SentMessages
		{
			get { return _sentMessages; }
		}

		public static MockEmailGateway Create()
		{
			return new MockEmailGateway();
		}
	}
}