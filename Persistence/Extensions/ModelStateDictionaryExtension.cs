using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NoticeBoard.Persistence.Extensions
{
    public static class ModelStateDictionaryExtension
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary modelState)
            => modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => $"{key} : [{x.ErrorMessage}]")).ToList();
    }
}
