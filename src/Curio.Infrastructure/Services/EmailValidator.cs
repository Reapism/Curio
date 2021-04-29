using System;
using Curio.Core.Interfaces;

namespace Curio.Infrastructure.Services
{
    public class EmailValidator : IEmailValidator
    {
        public string GetEmailName(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var atPosition = email.IndexOf('@', StringComparison.Ordinal);

                if (atPosition > 0)
                {
                    return email.Substring(0, atPosition);
                }
                else if (atPosition == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return email;
                }
            }

            return string.Empty;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return IsValidEmailInternal(email);
        }

        private bool IsValidEmailInternal(string email)
        {
            throw new NotImplementedException();
        }
    }
}
