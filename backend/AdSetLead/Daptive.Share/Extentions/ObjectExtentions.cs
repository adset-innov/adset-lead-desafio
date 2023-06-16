
using System;

namespace Daptive.Share.Extentions
{
    public static class ObjectExtentions
    {
        /// <summary>
        /// Oposto de null
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object editValue)
        {
            bool ret;
            ret = editValue != null && editValue != DBNull.Value;
            return ret;
        }

        /// <summary>
        /// Valida se um tipo desconhecido contém um null
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        public static bool IsNull(this object editValue)
        {
            return editValue.IsNotNull() == false;
        }
    }
}
