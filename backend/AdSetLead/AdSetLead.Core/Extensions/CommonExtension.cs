

using Daptive.Share.Extentions;
using Daptive.Share.Response;

namespace AdSetLead.Core.Extensions
{
    public static class CommonExtension
    {
        public static bool InError(this BaseResponse response)
        {
            bool inError = false;
            bool hasErrorMessages = response.HasErrorMessages 
                                    || response.HasExceptionMessages 
                                    || response.HasValidationMessages;

            if (response.IsNull() || hasErrorMessages) 
            {
                inError = true;
            }

            return inError;
        }
    }
}
