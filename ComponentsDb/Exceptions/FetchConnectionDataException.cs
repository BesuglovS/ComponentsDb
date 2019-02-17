using System;

namespace ComponentsDb.Exceptions
{
    class FetchConnectionDataException: Exception
    {
        public FetchConnectionDataException(): base("Не удалось прочитать реквизиты для подключения к базе данных.")
        {            
        }

        public FetchConnectionDataException(string message)
            : base(message)
        {
        }

        public FetchConnectionDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
