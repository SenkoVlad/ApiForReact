using ApiForReact.Models;
using System;

namespace ApiForReact.Helpers
{
    public class Validator
    {
        public static bool IsSiteValid(string site)
        {
            Uri uriResult;
            bool tryCreateResult = Uri.TryCreate(site, UriKind.Absolute, out uriResult);
            if (tryCreateResult == true && uriResult != null)
                return true;
            else
                return false;
        }

        public static string ValidateUserContacts(UserContacts userContacts)
        {
            foreach (var prop in userContacts.GetType().GetProperties())
            {
                var value = (string)prop.GetValue(userContacts, null);
                if (!IsSiteValid(value))
                    return string.Concat("Field ", prop.Name, " isn't valid");
            }
            return "";
        }
    }
}
