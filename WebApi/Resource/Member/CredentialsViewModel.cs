using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using WebApi.Validations;

namespace WebApi.Resource.Member
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
