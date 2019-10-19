namespace MvcMarket.FrontEnd.Models
{
    using System;
    using System.Net.Mail;
    using System.Text;

    public class EmailOrderSubmitter
    {
        private const string MailSubject = "New order submitted!";
        private readonly string _mailFrom;
        private readonly string _mailTo;
        private readonly string _smtpServer;

        public EmailOrderSubmitter(string smtpServer, string mailFrom, string mailTo)
        {
            _smtpServer = smtpServer;
            _mailFrom = mailFrom;
            _mailTo = mailTo;
        }

        public bool SubmitOrder(Cart cart,Guid userId)
        {
            var body = new StringBuilder();
            body.AppendLine("A new order has been submitted");
            body.AppendLine("---");
            body.AppendLine("Items:");
            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                                  line.Product.Name,
                                  subtotal);
            }
            body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue());
            body.AppendLine("---");
            body.AppendLine("Ship to:");
            var sd = cart.ShippingDetails(userId);
            body.AppendLine(sd.Name);
            body.AppendLine(sd.Address);
            body.AppendLine(sd.City);
            body.AppendLine(sd.State);
            body.AppendLine(sd.Country);
            body.AppendLine(sd.Zip);

            var smtpClient = new SmtpClient(_smtpServer);
            try
            {
                smtpClient.Send(new MailMessage(_mailFrom, _mailTo, MailSubject,
                                                            body.ToString()));
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}