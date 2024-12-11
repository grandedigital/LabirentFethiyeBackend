
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Persistence.Helpers
{
    public static class GeneralFunctions
    {
        public static string UrlReplace(string value)
        {
            if (value != null)
            {
                value = value.Trim();
                string gvCopy = value.ToLowerInvariant().Trim();
                string[,] arr = new string[,]
                {{ ",", "_" },{ "'", "_" },{ ":", "" },{ "%27", "" },{ "?", "" },{ "*", "" },{ "&#199;", "o" },{ "&#246;", "o" },{ "&#214;", "o" },{ "&#252;", "u" },{ "&#220;", "u" },{ "&#231;", "c" },{ "&#174;", "®" },{ "&amp;", "-" },{ "&nbsp;", "-" },{ " ", "-" },{ ";", "-" },{ "%20", "-" },{ "/", "-" },{ ".", "" },{ "ç", "c" },{ "Ç", "c" },{ "ğ", "g" },{ "Ğ", "g" },{ "İ", "i" },{ "I", "i" },{ "ı", "i" },{ "ö", "o" },{ "Ö", "o" },{ "ş", "s" },{ "Ş", "s" },{ "ü", "u" },{ "Ü", "u" },{ ".", "" },{ "’", "" },{ "'", "" },{ "(", "_" },{ ")", "_" },{ "/", "_" },{ "<", "_" },{ ">", "_" },{ "\"", "_" },{ "\\", "_" },{ "{", "_" },{ "}", "_" },{ "%", "_" },{ "&", "_" },{ "+", "_" },{ "//", "_" },{ "__", "_" },{ "³", "_" },{ "²", "2" },{ "“", null },{ "”", null },{ "’", null },{ "”", null },{ "&", "-" },{ "[^\\w]", "-" },{ "----", "-" },{ "---", "-" },{ "--", "-" },{ "[", "-" },{ "]", "-" },{ "½", "-" },{ "^", "-" },{ "~", "-" },{ "|", "-" },{ "*", "-" },{ "#", "-" },{ "%", "-" },{ "union", "" },{ "select", "" },{ "update", "" },{ "insert", "" },{ "delete", "" },{ "drop", "" },{ "into", "" },{ "where", "" },{ "order", "" },{ "chr", "" },{ "isnull", "" },{ "xtype", "" },{ "sysobject", "" },{ "syscolumns", "" },{ "convert", "" },{ "db_name", "" },{ "@@", "-" },{ "@var", "-" },{ "declare", "" },{ "execute", "" },{ "having", "" },{ "1=1", "-" },{ "exec", "" },{ "cmdshell", "" },{ "master", "" },{ "cmd", "-" },{ "xp_", "-" },};
                int abc = -1;
                for (int i = 0; i < arr.Length / 2; i++)
                {
                    abc = gvCopy.IndexOf(arr[i, 0]);
                    if (abc > -1)
                    {
                    bastan:
                        value = value.Substring(0, abc) + arr[i, 1] + value.Substring(abc + arr[i, 0].Length, value.Length - abc - arr[i, 0].Length);
                        gvCopy = gvCopy.Substring(0, abc) + arr[i, 1] + gvCopy.Substring(abc + arr[i, 0].Length, gvCopy.Length - abc - arr[i, 0].Length);
                        abc = gvCopy.IndexOf(arr[i, 0]);
                        if (abc > -1) goto bastan;
                    }
                }

            }
            return value.ToLowerInvariant().Trim();
        }

        public static string UniqueNumber()
        {
            return DateTime.Now.ToString("yyMMddHHmmssffff");
        }

        public static PageInfo PageInfoHelper(int Page, int Size, int TotalCount)
        {
            int TotalPage = (int)Math.Ceiling((double)TotalCount / (double)Size);
            return new()
            {
                Page = Page,
                Size = Size,
                NextPage = Page + 1 < TotalPage ? true : false,
                PrevPage = Page > 0 ? true : false,
                TotalRow = TotalCount,
                TotalPage = TotalPage
            };
        }

    }
}
