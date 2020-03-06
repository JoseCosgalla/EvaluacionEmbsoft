namespace AeropuertoApp.Infrastructure.Queries
{
    public static class FieldFactory
    {
        public static string DateFieldConverter(this string field, int dataType)
        {
            field = " CONVERT(varchar(11), "+ field + ", "+ dataType +")";
            return field;
        }

        public static string DateAndHourFieldConverter(this string field, int dataTypeDate, int dataTypeHour)
        {
            field = " CONVERT(varchar(11), " + field + ", " + dataTypeDate + ")" + " + ' ' + " + " CONVERT(varchar(11), " + field + ", " + dataTypeHour + ")";
            return field;
        }
    }
}